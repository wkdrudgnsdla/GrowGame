using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InfraClick : MonoBehaviour
{
    public GameObject playerView;
    public InfraManager iManager;

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

    private Vector3 preClickPosition;
    private bool hasPreClickPosition = false;

    private Vector3 moveVelocity = Vector3.zero;

    private bool forceZToSixActive = false;

    public bool isReturning = false;
    public bool returnFinished = true;

    private void Awake()
    {
        if (playerView == null)
            playerView = GameObject.Find("PlayerView");
        iManager = gameObject.GetComponent<InfraManager>();
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
        if (uiPanel.activeSelf)
        {
            LevelTxt();
        }

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        HandleMovement();

        if (isReturning)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                hitObj = hit.collider.gameObject;

                if (hitObj.CompareTag("Infra"))
                {
                    InfraInfo info = hitObj.GetComponent<InfraInfo>();
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
                        Debug.LogWarning($"[{hitObj.name}]에 InfraInfo 컴포넌트가 없습니다.");
                        HideUI();
                    }

                    StartMoveTo(hitObj.transform.position, forceZToSix: true, preserveZ: false);
                    return;
                }
            }

            HideUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            HideUI();
    }

    void StartMoveTo(Vector3 target, bool forceZToSix = false, bool preserveZ = false)
    {
        if (playerView == null) return;

        target.y = Mathf.Max(target.y, minY);

        if (forceZToSix)
        {
            target.z = 10f;
            target.x += uiOffsetX;
            forceZToSixActive = true;
            isReturning = false;
        }
        else
        {
            if (!preserveZ)
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
        string status = string.IsNullOrEmpty(info.status) ? "-" : info.status;

        if (titleTMP != null) titleTMP.text = title;

        if (statusTMP != null) statusTMP.text = status;

        string titleForCheck = title;

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

            isReturning = true;
            returnFinished = false;

            StartMoveTo(returnTarget, forceZToSix: false, preserveZ: true);

            hasPreClickPosition = false;
        }
    }

    void LevelTxt()
    {
        if (iManager == null || hitObj == null) return;

        if (hitObj.name == "Silo")
        {
            if (levelTMP != null) levelTMP.text = "Level." + iManager.siloLevel;
            if (countTMP != null) countTMP.text = "Infra Count :  " + iManager.siloCont;
        }
        else
        {
            if (levelTMP != null) levelTMP.text = "Level." + "???";
            if (countTMP != null) countTMP.text = "Infra Count :  " + "???";
        }
    }
}
