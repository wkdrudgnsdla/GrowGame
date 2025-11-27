using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InfraClick : MonoBehaviour
{
    public GameObject playerView;
    private InfraManager iManager;
    public InfraInfo info;

    public Camera cam;
    public float maxDistance = 500f;
    public GameObject hitObj;

    [Header("UI")]
    public GameObject uiPanel;

    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI levelTMP;
    public TextMeshProUGUI countTMP;
    public TextMeshProUGUI statusTMP;
    public TextMeshProUGUI upgradeType;
    public TextMeshProUGUI UpgradeStatusText;

    public Image uiImage;

    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

    [Header("Movement")]
    public float maxMoveSpeed = 20f;
    public float minMoveSpeed = 2f;
    public float stopDistance = 0.15f;

    public float minY = 12f;
    public float minZ = 6f;

    public float returnY = 30f;

    public float uiOffsetX = 5f;

    public float smoothTimeMin = 0.04f;
    public float smoothTimeMax = 0.12f;

    private bool isMoving = false;
    private Vector3 moveTarget;
    private float initialDistance = 0f;

    [SerializeField] private Vector3 preClickPosition;
    [SerializeField] private bool hasPreClickPosition = false;

    private Vector3 moveVelocity = Vector3.zero;

    private bool forceZToSixActive = false;

    public bool isReturning = false;
    public bool returnFinished = true;

    [Header("infraData")]
    public GameObject Silo;
    public Transform SiloViewTarget;
    public bool SiloUseForceZ = true;

    public GameObject Storages;
    public Transform StoragesViewTarget;
    public bool StoragesUseForceZ = true;

    public GameObject GreenHouses;
    public Transform GreenHousesViewTarget;
    public bool GreenHousesUseForceZ = true;

    public GameObject Animal_Farms;
    public Transform AnimalFarmsViewTarget;
    public bool AnimalFarmsUseForceZ = true;

    private void Awake()
    {
        iManager = gameObject.GetComponent<InfraManager>();
        if (playerView == null)
            playerView = GameObject.Find("PlayerView");
    }

    void Start()
    {
        upgradeType = GameObject.Find("UpgradeStatusTitleText").GetComponent<TextMeshProUGUI>();

        returnFinished = true;
        if (cam == null) cam = Camera.main;
        if (uiPanel != null) uiPanel.SetActive(false);

        if (playerView == null)
        {
            Debug.LogWarning("playerView가 세팅되지 않았습니다. PlayerView를 찾아보세요.");
        }
        else
        {
            Vector3 pos = playerView.transform.position;
            bool corrected = false;
            if (pos.y < minY) { pos.y = minY; corrected = true; }
            if (pos.z < minZ) { pos.z = minZ; corrected = true; }
            if (corrected)
            {
                playerView.transform.position = pos;
            }
        }
    }

    void Update()
    {
        if (uiPanel != null && uiPanel.activeSelf)
        {
            LevelTxt(info);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            HideUI();

        HandleMovement();
    }

    void StartMoveTo(Vector3 target, bool forceZToSix = false)
    {
        if (playerView == null) return;

        returnFinished = false;

        target.y = Mathf.Max(target.y, minY);

        if (forceZToSix)
        {
            target.z = 6f;
            target.x += uiOffsetX;
            forceZToSixActive = true;
            isReturning = false;
        }
        else
        {
            target.z = Mathf.Max(target.z, minZ);
            forceZToSixActive = false;
        }

        moveTarget = target;
        initialDistance = Vector3.Distance(playerView.transform.position, moveTarget);

        moveVelocity = Vector3.zero;

        isMoving = initialDistance > stopDistance;
    }

    void HandleMovement()
    {
        if (!isMoving || playerView == null) return;

        Vector3 currentPos = playerView.transform.position;
        float currentDistance = Vector3.Distance(currentPos, moveTarget);

        if (currentDistance <= stopDistance)
        {
            playerView.transform.position = moveTarget;
            isMoving = false;
            moveVelocity = Vector3.zero;
            forceZToSixActive = false;

            if (isReturning)
            {
                returnFinished = true;
                isReturning = false;
            }

            return;
        }

        float ratio = (initialDistance > 0f) ? (currentDistance / initialDistance) : 0f;
        ratio = Mathf.Clamp01(ratio);

        float desiredMaxSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, ratio);

        float smoothTime = Mathf.Lerp(smoothTimeMax, smoothTimeMin, ratio);

        Vector3 newPos = Vector3.SmoothDamp(
            currentPos,
            moveTarget,
            ref moveVelocity,
            smoothTime,
            desiredMaxSpeed,
            Time.deltaTime
        );

        newPos.y = Mathf.Max(newPos.y, minY);

        if (!forceZToSixActive)
        {
            newPos.z = Mathf.Max(newPos.z, minZ);
        }

        playerView.transform.position = newPos;
    }

    void ShowUI(InfraInfo info)
    {
        if (uiPanel == null) return;
        uiPanel.SetActive(true);

        string title = info.title ?? "";

        if (titleTMP != null) titleTMP.text = title;

        string titleForCheck = !string.IsNullOrEmpty(title)
            ? title
            : (titleTMP != null && !string.IsNullOrEmpty(titleTMP.text) ? titleTMP.text : "-");

        if (upgradeType != null)
        {
            if (titleForCheck == "Silo" || titleForCheck == "Storage")
            {
                upgradeType.text = "Storage Capacity ↑";
            }
            else if (titleForCheck == "GreenHouse")
            {
                upgradeType.text = "Extra Production ↑";
            }
            else if (titleForCheck == "Barn")
            {
                upgradeType.text = "Sell Price ↑";
            }
            else
            {
                upgradeType.text = "-";
            }
        }

        if (uiImage != null)
        {
            string objectName = info.gameObject.name;
            Sprite loaded = null;

            if (!string.IsNullOrEmpty(objectName) && spriteCache.TryGetValue(objectName, out Sprite cached))
            {
                loaded = cached;
            }
            else
            {
                if (!string.IsNullOrEmpty(objectName))
                {
                    loaded = Resources.Load<Sprite>($"Image/{objectName}");
                    if (loaded != null)
                    {
                        spriteCache[objectName] = loaded;
                    }
                }
            }

            if (loaded != null)
            {
                uiImage.sprite = loaded;
                uiImage.gameObject.SetActive(true);
            }
            else
            {
                if (info.infraImage != null && info.infraImage.sprite != null)
                {
                    uiImage.sprite = info.infraImage.sprite;
                    uiImage.gameObject.SetActive(true);
                }
                else
                {
                    uiImage.gameObject.SetActive(false);
                }
            }
        }
    }

    void HideUI()
    {
        if (uiPanel == null) return;

        uiPanel.SetActive(false);

        if (playerView != null && hasPreClickPosition)
        {
            Vector3 returnTarget = preClickPosition;
            returnTarget.y = returnY;
            returnTarget.z = Mathf.Max(returnTarget.z, minZ);

            isReturning = true;
            returnFinished = false;

            StartMoveTo(returnTarget, forceZToSix: false);

            hasPreClickPosition = false;
        }
    }

    private void HandleButtonClick(GameObject infraObj, Transform viewTarget, bool useForceZ)
    {
        if (infraObj == null)
        {
            Debug.LogWarning("Infra object가 할당되어 있지 않습니다.");
            return;
        }

        hitObj = infraObj;

        if (infraObj == Silo) hitObj.name = "Silo";
        else if (infraObj == Storages) hitObj.name = "Storages";
        else if (infraObj == GreenHouses) hitObj.name = "GreenHouses";
        else if (infraObj == Animal_Farms) hitObj.name = "Animal_Farms";

        info = infraObj.GetComponent<InfraInfo>();
        if (info != null)
        {
            if (playerView != null)
            {
                preClickPosition = playerView.transform.position;
                preClickPosition.y = Mathf.Max(preClickPosition.y, minY);
                preClickPosition.z = Mathf.Max(preClickPosition.z, minZ);
                hasPreClickPosition = true;
            }

            ShowUI(info);
        }
        else
        {
            Debug.LogWarning($"[{infraObj.name}]에 InfraInfo 컴포넌트가 없습니다.");
            HideUI();
        }

        // viewTarget이 있으면 그 위치로, 없으면 오브젝트 월드 포지션으로 이동
        if (viewTarget != null)
        {
            StartMoveTo(viewTarget.position, forceZToSix: useForceZ);
        }
        else
        {
            StartMoveTo(infraObj.transform.position, forceZToSix: useForceZ);
        }
    }

    public void OnClickSilo()
    {
        HandleButtonClick(Silo, SiloViewTarget, SiloUseForceZ);
    }

    public void OnClickStorages()
    {
        HandleButtonClick(Storages, StoragesViewTarget, StoragesUseForceZ);
    }

    public void OnClickGreenHouses()
    {
        HandleButtonClick(GreenHouses, GreenHousesViewTarget, GreenHousesUseForceZ);
    }

    public void OnClickAnimalFarms()
    {
        HandleButtonClick(Animal_Farms, AnimalFarmsViewTarget, AnimalFarmsUseForceZ);
    }

    void LevelTxt(InfraInfo info)
    {
        if (iManager == null || hitObj == null) return;

        if (UpgradeStatusText != null) UpgradeStatusText.text = iManager.UpgradeStatus;

        if (hitObj.name == "Silo")
        {
            if (levelTMP != null) levelTMP.text = "Level." + iManager.siloLevel;
            info.level = iManager.siloLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.siloCont;
            info.infraCount = iManager.siloCont;
            if (statusTMP != null) statusTMP.text = "Silo Capacity  + " + iManager.siloCapacity;
            info.status = "Silo Capacity  + " + iManager.siloCapacity;
        }
        else if (hitObj.name == "Storages")
        {
            if (levelTMP != null) levelTMP.text = "Level." + iManager.storageLevel;
            info.level = iManager.storageLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.storageCount;
            info.infraCount = iManager.storageCount;
            if (statusTMP != null) statusTMP.text = "Silo Capacity  + " + iManager.storageCapacity;
            info.status = "Silo Capacity  + " + iManager.storageCapacity;
        }
        else if (hitObj.name == "GreenHouses")
        {
            if (levelTMP != null) levelTMP.text = "Level." + iManager.greenHouseLevel;
            info.level = iManager.greenHouseLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.greenHouseCount;
            info.infraCount = iManager.greenHouseCount;
            if (statusTMP != null) statusTMP.text = " + " + iManager.storageCapacity;
            info.status = "Silo Capacity  + " + iManager.storageCapacity;
        }
        else if (hitObj.name == "Animal_Farms")
        {
            if (levelTMP != null) levelTMP.text = "Level." + iManager.animalFarmLevel;
            info.level = iManager.animalFarmLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.animalFarmCount;
            info.infraCount = iManager.animalFarmCount;
            if (statusTMP != null) statusTMP.text = "increase in profits + " + (20 * iManager.animalFarmCount).ToString() + "%";
            info.status = "increase in profits + " + (20 * iManager.animalFarmCount).ToString() + "%";
        }
        else
        {
            if (levelTMP != null) levelTMP.text = "Level." + "???";
            if (countTMP != null) countTMP.text = "Infra Count :  " + "???";
        }
    }
}
