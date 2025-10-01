using UnityEngine;
using UnityEngine.UI;

public class InfraInfo : MonoBehaviour
{
    [Header("기본 정보")]
    public string title = "인프라 이름";
    public int level = 0;
    public int infraCount = 0;
    public string status = "";

    public Image infraImage;
}
