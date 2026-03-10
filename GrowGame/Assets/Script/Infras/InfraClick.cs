using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfraClick : MonoBehaviour
{
    public GameObject playerView;
    private InfraManager iManager;
    [SerializeField] private FarmUpgrade farmUpgrade;
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

    private bool ignoreMinZForThisMove = false;

    public bool isReturning = false;
    public bool returnFinished = true;

    [Header("infraData")]
    public GameObject Silo;
    public float SiloViewZ = 6f;

    public GameObject Storages;
    public float StoragesViewZ = 6f;

    public GameObject GreenHouses;
    public float GreenHousesViewZ = 6f;

    public GameObject Animal_Farms;
    public float AnimalFarmsViewZ = 6f;

    public GameObject Village;
    public float HousesViewZ = 6f;

    public GameObject Reservoir;
    public float ReservoirViewZ = 6f;

    // farms
    public GameObject WheatField;
    public float WheatFieldViewZ = 6f;

    public GameObject CarrotField;
    public float CarrotFieldViewZ = 6f;

    public GameObject CucumberField;
    public float CucumberFieldViewZ = 6f;

    public GameObject PotatoField;
    public float PotatoFieldViewZ = 6f;

    public GameObject OnionField;
    public float OnionFieldViewZ = 6f;

    [SerializeField] private GameObject waterButton;

    private void Awake()
    {
        iManager = gameObject.GetComponent<InfraManager>();
        if (playerView == null)
            playerView = GameObject.Find("PlayerView");
    }

    void Start()
    {
        if (upgradeType == null)
        {
            var tmp = GameObject.Find("UpgradeStatusTitleText");
            if (tmp != null) upgradeType = tmp.GetComponent<TextMeshProUGUI>();
        }

        returnFinished = true;
        if (cam == null) cam = Camera.main;
        if (uiPanel != null) uiPanel.SetActive(false);

        if (playerView != null)
        {
            Vector3 pos = playerView.transform.position;
            bool corrected = false;
            if (pos.y < minY) { pos.y = minY; corrected = true; }
            if (pos.z < minZ) { pos.z = minZ; corrected = true; }
            if (corrected) playerView.transform.position = pos;
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

    void StartMoveTo(Vector3 target, float? zOverride = null)
    {
        if (playerView == null) return;

        returnFinished = false;

        target.y = Mathf.Max(target.y, minY);

        target.x += uiOffsetX;

        if (zOverride.HasValue)
        {
            target.z = zOverride.Value;   
            ignoreMinZForThisMove = true; 
        }
        else
        {
            target.z = Mathf.Max(target.z, minZ);
            ignoreMinZForThisMove = false;
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
            ignoreMinZForThisMove = false;

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

        if (!ignoreMinZForThisMove)
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
                upgradeType.text = "Storage Capacity ↑";
            else if (titleForCheck == "GreenHouse")
                upgradeType.text = "Extra Production ↑";
            else if (titleForCheck == "Barn" || titleForCheck == "Village")
                upgradeType.text = "Sell Price ↑";
            else
                upgradeType.text = "-";
        }

        if (uiImage != null)
        {
            string objectName = info.gameObject.name;
            Sprite loaded = null;

            if (!string.IsNullOrEmpty(objectName) && spriteCache.TryGetValue(objectName, out Sprite cached))
                loaded = cached;
            else
            {
                if (!string.IsNullOrEmpty(objectName))
                {
                    loaded = Resources.Load<Sprite>($"Image/{objectName}");
                    if (loaded != null) spriteCache[objectName] = loaded;
                }
            }

            if (loaded != null)
            {
                uiImage.sprite = loaded;
                uiImage.gameObject.SetActive(true);
            }
            else if (info.infraImage != null)
            {
                uiImage.sprite = info.infraImage;
                uiImage.gameObject.SetActive(true);
            }
            else
            {
                uiImage.gameObject.SetActive(false);
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

            StartMoveTo(returnTarget);

            hasPreClickPosition = false;
        }
    }

    private void HandleButtonClick(GameObject infraObj, float viewZ)
    {
        hitObj = infraObj;

        info = infraObj.GetComponent<InfraInfo>();
        if (info != null)
        {
            if (playerView != null)
            {
                preClickPosition = playerView.transform.position;
                hasPreClickPosition = true;
            }

            ShowUI(info);
        }
        else
        {
            Debug.LogWarning($"[{infraObj.name}]에 InfraInfo 컴포넌트가 없습니다.");
            HideUI();
        }

        Vector3 targetPos = infraObj.transform.position;
        targetPos.y = Mathf.Max(targetPos.y, minY);
        StartMoveTo(targetPos, viewZ);
    }

    public void OnClickSilo()
    {
        HandleButtonClick(Silo, SiloViewZ);
    }

    public void OnClickStorages()
    {
        HandleButtonClick(Storages, StoragesViewZ);
    }

    public void OnClickGreenHouses()
    {
        HandleButtonClick(GreenHouses, GreenHousesViewZ);
    }

    public void OnClickAnimalFarms()
    {
        HandleButtonClick(Animal_Farms, AnimalFarmsViewZ);
    }

    public void OnClickVillage()
    {
        HandleButtonClick(Village, HousesViewZ);
    }
    
    public void OnClickReservoir()
    {
        HandleButtonClick(Reservoir, ReservoirViewZ);
    }

    public void OnClickWheatField()
    {
        HandleButtonClick(WheatField, WheatFieldViewZ);
    }

    public void OnClickCarrotField()
    {
        HandleButtonClick(CarrotField, CarrotFieldViewZ);
    }

    public void OnClickCucumberField()
    {
        HandleButtonClick(CucumberField, CucumberFieldViewZ);
    }

    public void OnClickPotatoField()
    {
        HandleButtonClick(PotatoField, PotatoFieldViewZ);
    }

    public void OnClickOnionField()
    {
        HandleButtonClick(OnionField, OnionFieldViewZ);
    }

    void LevelTxt(InfraInfo info)
    {
        if (iManager == null || hitObj == null) return;

        if (UpgradeStatusText != null) UpgradeStatusText.text = iManager.UpgradeStatus;

        if (hitObj.name == "Silo")
        {
            if (waterButton.activeSelf) waterButton.SetActive(false);
            if (!countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(true);
            if (levelTMP != null) levelTMP.text = "Level." + iManager.siloLevel;
            info.level = iManager.siloLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.siloCont;
            info.infraCount = iManager.siloCont;
            if (statusTMP != null) statusTMP.text = "Silo Capacity  + " + iManager.siloCapacity;
            info.status = "Silo Capacity  + " + iManager.siloCapacity;
        }
        else if (hitObj.name == "Storages")
        {
            if (waterButton.activeSelf) waterButton.SetActive(false);
            if (!countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(true);
            if (levelTMP != null) levelTMP.text = "Level." + iManager.storageLevel;
            info.level = iManager.storageLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.storageCount;
            info.infraCount = iManager.storageCount;
            if (statusTMP != null) statusTMP.text = "Silo Capacity  + " + iManager.storageCapacity;
            info.status = "Silo Capacity  + " + iManager.storageCapacity;
        }
        else if (hitObj.name == "GreenHouses")
        {
            if (waterButton.activeSelf) waterButton.SetActive(false);
            if (!countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(true);
            if (levelTMP != null) levelTMP.text = "Level." + iManager.greenHouseLevel;
            info.level = iManager.greenHouseLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.greenHouseCount;
            info.infraCount = iManager.greenHouseCount;
            if (statusTMP != null) statusTMP.text = "ExtraProduction + " + (50 * iManager.greenHouseLevel).ToString() + "%";
            info.status = "ExtraProduction + " + (50 * iManager.greenHouseLevel).ToString() + "%";
        }
        else if (hitObj.name == "Animal_Farms")
        {
            if (waterButton.activeSelf) waterButton.SetActive(false);
            if (!countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(true);
            if (levelTMP != null) levelTMP.text = "Level." + iManager.animalFarmLevel;
            info.level = iManager.animalFarmLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.animalFarmCount;
            info.infraCount = iManager.animalFarmCount;
            if (statusTMP != null) statusTMP.text = "increase in profits + " + (20 * iManager.animalFarmCount).ToString() + "%";
            info.status = "increase in profits + " + (20 * iManager.animalFarmCount).ToString() + "%";
        }
        else if (hitObj.name == "Village")
        {
            if (waterButton.activeSelf) waterButton.SetActive(false);
            if (!countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(true);
            if (levelTMP != null) levelTMP.text = "Level." + iManager.VillageLevel;
            info.level = iManager.VillageLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.VillageCount;
            info.infraCount = iManager.VillageCount;
            if (statusTMP != null) statusTMP.text = "increase in profits + " + (5 * iManager.VillageCount).ToString() + "%";
            info.status = "increase in profits + " + (5 * iManager.VillageCount).ToString() + "%";
        }
        else if (hitObj.name == "Reservoir")
        {
            if (waterButton.activeSelf) waterButton.SetActive(false);
            if (!countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(true);
            if (levelTMP != null) levelTMP.text = "Level." + iManager.ReservoirLevel;
            info.level = iManager.ReservoirLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.ReservoirCount;
            info.infraCount = iManager.ReservoirCount;
            if (statusTMP != null) statusTMP.text = "water cooltime - " + (200 * iManager.ReservoirCount).ToString() + "%";
            info.status = "water cooltime - " + (200 * iManager.ReservoirCount).ToString() + "%";
        }
        // farms
        else if (hitObj.name == "Wheat")
        {
            if (!waterButton.activeSelf) waterButton.SetActive(true);
            if (countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(false);
            if (levelTMP != null) levelTMP.text = "Level." + farmUpgrade.wheatFarmLevel;
            info.level = farmUpgrade.wheatFarmLevel;
            if (statusTMP != null) statusTMP.text = "+" + (25 * farmUpgrade.wheatFarmLevel) + "/min";
            info.status = "+" + (25 * farmUpgrade.wheatFarmLevel) + "/min";
        }
        else if (hitObj.name == "Carrot")
        {
            if (!waterButton.activeSelf) waterButton.SetActive(true);
            if (countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(false);
            if (levelTMP != null) levelTMP.text = "Level." + farmUpgrade.carrotFarmLevel;
            info.level = farmUpgrade.carrotFarmLevel;
            if (statusTMP != null) statusTMP.text = "+" + (25 * farmUpgrade.carrotFarmLevel) + "/min";
            info.status = "+" + (25 * farmUpgrade.carrotFarmLevel) + "/min";
        }
        else if (hitObj.name == "Cucumber")
        {
            if (!waterButton.activeSelf) waterButton.SetActive(true);
            if (countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(false);
            if (levelTMP != null) levelTMP.text = "Level." + farmUpgrade.cucumberFarmLevel;
            info.level = farmUpgrade.cucumberFarmLevel;
            if (statusTMP != null) statusTMP.text = "+" + (25 * farmUpgrade.cucumberFarmLevel) + "/min";
            info.status = "+" + (25 * farmUpgrade.cucumberFarmLevel) + "/min";
        }
        else if (hitObj.name == "Potato")
        {
            if (!waterButton.activeSelf) waterButton.SetActive(true);
            if (countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(false);
            if (levelTMP != null) levelTMP.text = "Level." + farmUpgrade.potatoFarmLevel;
            info.level = farmUpgrade.potatoFarmLevel;
            if (statusTMP != null) statusTMP.text = "+" + (25 * farmUpgrade.potatoFarmLevel) + "/min";
            info.status = "+" + (25 * farmUpgrade.potatoFarmLevel) + "/min";
        }
        else if (hitObj.name == "Onion")
        {
            if (!waterButton.activeSelf) waterButton.SetActive(true);
            if (countTMP.gameObject.activeSelf) countTMP.gameObject.SetActive(false);
            if (levelTMP != null) levelTMP.text = "Level." + farmUpgrade.onionFarmLevel;
            info.level = farmUpgrade.onionFarmLevel;
            if (statusTMP != null) statusTMP.text = "+" + (25 * farmUpgrade.onionFarmLevel) + "/min";
            info.status = "+" + (25 * farmUpgrade.onionFarmLevel) + "/min";
        }
        else
        {
            if (levelTMP != null) levelTMP.text = "Level." + "???";
            if (countTMP != null) countTMP.text = "Infra Count :  " + "???";
        }
    }
}

