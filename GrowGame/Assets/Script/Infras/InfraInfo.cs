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

    private InfraManager iManager;

    private int siloCapacity;

    private void Start()
    {
        iManager = GameObject.Find("GameManager").GetComponent<InfraManager>();
    }

    private void Update()
    {
        SiloCapacity();

        if (title == "Silo")
        {
            status = "Silo Capacity + " + siloCapacity;
        }
        else if (title == "Storage")
        {
            status = "Storage Capacity + " + 1500 * iManager.storageCount;
        }
        else if(title == "Barn")
        {
            status = "Sell Price +" + 20 * iManager.animalFarmCount + "%";
        }
        else if (title == "GreenHouse")
        {
            status = "??? Production Bonus" + 100 * iManager.greenHouseCount;
        }
    }


    private void SiloCapacity()
    {
        if (iManager.siloCont <= 4 && iManager.silo1Lev == 1 && iManager.silo2Lev == 1 && iManager.silo3Lev == 1 && iManager.silo4Lev == 1)
        {
            siloCapacity = 300 * iManager.siloCont;
        }
        else if (iManager.silo1Lev == 2 && iManager.silo2Lev == 1 && iManager.silo3Lev == 1 && iManager.silo4Lev == 1)
        {
            siloCapacity = 1400;
        }
        else if (iManager.silo1Lev == 2 && iManager.silo2Lev == 2 && iManager.silo3Lev == 1 && iManager.silo4Lev == 1)
        {
            siloCapacity = 1600;
        }
        else if (iManager.silo1Lev == 2 && iManager.silo2Lev == 2 && iManager.silo3Lev == 2 && iManager.silo4Lev == 1)
        {
            siloCapacity = 1800;
        }
        else if (iManager.silo1Lev == 2 && iManager.silo2Lev == 2 && iManager.silo3Lev == 2 && iManager.silo4Lev == 2)
        {
            siloCapacity = 2000;
        }
    }
}
