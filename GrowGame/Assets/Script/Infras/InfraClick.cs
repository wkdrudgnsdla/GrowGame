using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InfraClick : MonoBehaviour
{
    public GameObject playerView;

    public Camera cam;
    public float maxDistance = 500f;

    [Header("UI (Screen Space - Panel)")]
    public GameObject uiPanel;

    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI levelTMP;
    public TextMeshProUGUI countTMP;
    public TextMeshProUGUI statusTMP;

    public Text titleText;
    public Text levelText;
    public Text countText;
    public Text statusText;

    public Image uiImage;

    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

    [Header("Movement")]
    public float maxMoveSpeed = 20f;
    public float minMoveSpeed = 2f;
    public float stopDistance = 0.15f;

    public float minY = 12f;
    public float minZ = 6f;

    public float returnY = 30f;

    [Tooltip("인프라 클릭 시 목표 X에 더해질 오프셋 (예: UI와 겹치지 않게 오른쪽으로 이동)")]
    public float uiOffsetX = 5f;

    [Header("Smooth Movement Tuning")]
    [Tooltip("가장 즉각적으로 이동할 때(멀리 있을 때) 적용될 최소 smoothTime")]
    public float smoothTimeMin = 0.04f;
    [Tooltip("가까울 때 더 부드럽게 정지시키기 위한 최대 smoothTime")]
    public float smoothTimeMax = 0.12f;

    private bool isMoving = false;
    private Vector3 moveTarget;
    private float initialDistance = 0f;

    private Vector3 preClickPosition;
    private bool hasPreClickPosition = false;

    private Vector3 moveVelocity = Vector3.zero;

    private bool forceZToSixActive = false;

    private void Awake()
    {
        if (playerView == null)
            playerView = GameObject.Find("PlayerView");
    }

    void Start()
    {
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
                Debug.LogWarning("playerView의 Y/Z가 최소값보다 낮아 보정했습니다.");
            }
        }
    }

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                GameObject hitObj = hit.collider.gameObject;

                if (hitObj.CompareTag("Infra"))
                {
                    InfraInfo info = hitObj.GetComponent<InfraInfo>();
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
                        Debug.LogWarning($"[{hitObj.name}]에 InfraInfo 컴포넌트가 없습니다.");
                        HideUI();
                    }

                    StartMoveTo(hitObj.transform.position, forceZToSix: true);
                    return;
                }
            }

            HideUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            HideUI();

        HandleMovement();
    }

    void StartMoveTo(Vector3 target, bool forceZToSix = false)
    {
        if (playerView == null) return;

        target.y = Mathf.Max(target.y, minY);

        if (forceZToSix)
        {
            target.z = 6f;
            target.x += uiOffsetX;
            forceZToSixActive = true;
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
        string level = "Level. " + info.level.ToString();
        string count = "Infra Count :  " + info.infraCount.ToString();
        string status = string.IsNullOrEmpty(info.status) ? "-" : info.status;

        if (titleTMP != null) titleTMP.text = title;
        else if (titleText != null) titleText.text = title;

        if (levelTMP != null) levelTMP.text = level;
        else if (levelText != null) levelText.text = level;

        if (countTMP != null) countTMP.text = count;
        else if (countText != null) countText.text = count;

        if (statusTMP != null) statusTMP.text = status;
        else if (statusText != null) statusText.text = status;

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
                    Debug.Log($"이미지 없음: Resources/Image/{objectName} 또는 InfraInfo.infraImage에 스프라이트 없음.");
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

            StartMoveTo(returnTarget, forceZToSix: false);

            hasPreClickPosition = false;
        }
    }
}
