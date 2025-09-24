using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InfraClick : MonoBehaviour
{
    public Camera cam;                     // 비워두면 Camera.main 사용
    public float maxDistance = 500f;

    [Header("UI (Screen Space - Panel)")]
    public GameObject uiPanel;             // 패널 (초기 비활성화)

    // TMP 우선
    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI levelTMP;
    public TextMeshProUGUI countTMP;
    public TextMeshProUGUI statusTMP;

    // legacy Text (TMP 미사용시)
    public Text titleText;
    public Text levelText;
    public Text countText;
    public Text statusText;

    // 이미지 슬롯
    public Image uiImage;

    // 간단 캐시: Resources에서 불러온 스프라이트를 저장
    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (uiPanel != null) uiPanel.SetActive(false);
    }

    void Update()
    {
        // UI 위에서 클릭하면 월드 클릭 무시
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                GameObject hitObj = hit.collider.gameObject;

                // 태그 검사: Tag가 "Infra"인 경우에만 동작
                if (hitObj.CompareTag("Infra"))
                {
                    InfraInfo info = hitObj.GetComponent<InfraInfo>();
                    if (info != null)
                    {
                        ShowUI(info);
                    }
                    else
                    {
                        Debug.LogWarning($"[{hitObj.name}]에 InfraInfo 컴포넌트가 없습니다.");
                        HideUI();
                    }
                    return;
                }
            }

            // 다른 곳 클릭 시 UI 닫기
            HideUI();
        }

        // ESC로 닫기 (선택)
        if (Input.GetKeyDown(KeyCode.Escape))
            HideUI();
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

            // 캐시 확인
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
    }
}
