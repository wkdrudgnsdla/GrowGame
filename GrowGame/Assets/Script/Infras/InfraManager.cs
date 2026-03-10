using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfraManager : MonoBehaviour
{
    [SerializeField] private InfraClick infraclick;
    [SerializeField] private FarmUpgrade farmUpgrade;
    [SerializeField] private MoneyManager mManager;

    public string UpgradeStatus;

    [Header("Slio")]
    public int siloLevel = 0;

    public int siloCont = 0;
    public int silo1Lev = 1;
    public int silo2Lev = 1;
    public int silo3Lev = 1;
    public int silo4Lev = 1;
    public int siloCapacity;

    [SerializeField] private TextMeshProUGUI UpgradePriceText;

    [SerializeField] private GameObject silo1Lev1;
    [SerializeField] private GameObject silo2Lev1;
    [SerializeField] private GameObject silo3Lev1;
    [SerializeField] private GameObject silo4Lev1;
    [SerializeField] private GameObject silo1Lev2;
    [SerializeField] private GameObject silo2Lev2;
    [SerializeField] private GameObject silo3Lev2;
    [SerializeField] private GameObject silo4Lev2;

    private double[] siloUpgreadeCost = { 100000f, 100000f, 100000f, 100000f, 100000f, 100000f, 100000f, 100000f };

    [Header("Storage")]
    public int storageLevel;
    public int storageCapacity;
    public int storageCount = 0;
    [SerializeField] private GameObject storage1;
    [SerializeField] private GameObject storage2;
    [SerializeField] private GameObject storage3;

    private double[] storageUpgreadeCost = { 1000000f, 1000000f, 1000000f};

    [Header("Animal_Farms")]
    public int animalFarmLevel;
    public int animalFarmCount = 0;
    [SerializeField] private GameObject animalFarm1;
    [SerializeField] private GameObject animalFarm2;

    private double[] animalFarmUpgreadeCost = { 200000000f, 200000000f };

    [Header("GreenHouse")]
    public int greenHouseLevel;
    public int greenHouseCount = 0;
    public float ExtraProduction = 0;
    public bool ActiveGreenHouse = false;
    [SerializeField] private GameObject GreenHouse1;
    [SerializeField] private GameObject GreenHouse2;

    private double[] greenHouseUpgreadeCost = { 1500000f, 1500000f };

    [Header("Village")]
    public int VillageLevel;
    public int VillageCount;
    [SerializeField] private GameObject[] Houses;
    [SerializeField] private bool VillageActive = false;

    private double[] villageUpgreadCost = { 10000000f, 10000000f, 10000000f, 10000000f, 10000000f, 10000000f, 10000000f, 10000000f };

    [Header("Reservoir")]
    public int ReservoirLevel;
    public int ReservoirCount;
    private bool reservoirActive = false;

    private double[] reservoirUpgreadCost = { 5000000f, 10f };
    [SerializeField] private GameObject Ground;
    [SerializeField] private GameObject Water;

    [Header("Farm")]
    public float WheatWaterCol;
    public float CarrotWaterCol;
    public float CucumberWaterCol;
    public float PotatoWaterCol;
    public float OnionWaterCol;

    private bool wheatFramActive = false;
    private bool carrotFramActive = false;
    private bool cucumberFramActive = false;
    private bool potatoFramActive = false;
    private bool onionFramActive = false;

    [SerializeField] private float wheatWaterTime;
    [SerializeField] private float carrotWaterTime;
    [SerializeField] private float cucumberWaterTime;
    [SerializeField] private float potatoWaterTime;
    [SerializeField] private float onionWaterTime;

    [SerializeField] private Image WaterUIImage;

    private float[] wheatUpgreadeCost = { 0f, 5000f,10000f };
    private float[] carrotUpgreadeCost = { 20000f, 30000f, 50000f };
    private float[] cucumberUpgreadeCost = { 75000f, 100000f, 200000f };
    private float[] potatoUpgreadeCost = { 500000f, 1000000f, 1200000f };
    private float[] onionUpgreadeCost = { 1500000f, 2000000f , 2500000f };


    private void Start()
    {
        WheatWaterCol = 300;
        CarrotWaterCol = 300;
        CucumberWaterCol = 300;
        PotatoWaterCol = 300;
        OnionWaterCol = 300;

        wheatWaterTime = 180;
        carrotWaterTime = 180;
        cucumberWaterTime = 180;
        potatoWaterTime = 180;
        onionWaterTime = 180;
    }

    private void Update()
    {
        if (infraclick.hitObj != null)
        {
            switch (infraclick.hitObj.name)
            {
                case "Wheat":
                    WaterUIImage.fillAmount = 1 - WheatWaterCol / 300;
                    break;
                case "Carrot":
                    WaterUIImage.fillAmount = 1 - CarrotWaterCol / 300;
                    break;
                case "Cucumber":
                    WaterUIImage.fillAmount = 1 - CucumberWaterCol / 300;
                    break;
                case "Potato":
                    WaterUIImage.fillAmount = 1 - PotatoWaterCol / 300;
                    break;
                case "Onion":
                    WaterUIImage.fillAmount = 1- OnionWaterCol / 300;
                    break;
            }
        }

        WaterManage();

        SiloUpgrade();
        SiloManage();

        StorageManage();

        AnimalFarmManage();

        GreenHouseManage();

        VillageManage();

        ReservoirManage();

        UpgradeStatusManage();
    }

    private void WaterManage()
    {
        if (!reservoirActive)
        {
            if (WheatWaterCol > 0 && wheatFramActive)
            {
                WheatWaterCol -= Time.deltaTime;
            }
            if (CarrotWaterCol > 0 && carrotFramActive)
            {
                CarrotWaterCol -= Time.deltaTime;
            }
            if (CucumberWaterCol > 0 && cucumberFramActive)
            {
                CucumberWaterCol -= Time.deltaTime;
            }
            if (PotatoWaterCol > 0 && potatoFramActive)
            {
                PotatoWaterCol -= Time.deltaTime;
            }
            if (OnionWaterCol > 0 && onionFramActive)
            {
                OnionWaterCol -= Time.deltaTime;
            }
        }
        else if (reservoirActive)
        {
            if (WheatWaterCol > 0 && wheatFramActive)
            {
                WheatWaterCol -= Time.deltaTime * 2f;
            }
            if (CarrotWaterCol > 0 && carrotFramActive)
            {
                CarrotWaterCol -= Time.deltaTime * 2f;
            }
            if (CucumberWaterCol > 0 && cucumberFramActive)
            {
                CucumberWaterCol -= Time.deltaTime * 2f;
            }
            if (PotatoWaterCol > 0 && potatoFramActive)
            {
                PotatoWaterCol -= Time.deltaTime * 2f;
            }
            if (OnionWaterCol > 0 && onionFramActive)
            {
                OnionWaterCol -= Time.deltaTime * 2f;
            }
        }
        

        if (farmUpgrade.wheatFarmWater)
        {
            wheatWaterTime -= Time.deltaTime;
        }
        else if (!farmUpgrade.wheatFarmWater)
        {
            wheatWaterTime = 180;
        }

        if (farmUpgrade.carrotFarmWater)
        {
            carrotWaterTime -= Time.deltaTime;
        }
        else if (!farmUpgrade.carrotFarmWater)
        {
            carrotWaterTime = 180;
        }

        if (farmUpgrade.cucumberFarmWater)
        {
            cucumberWaterTime -= Time.deltaTime;
        }
        else if (!farmUpgrade.cucumberFarmWater)
        {
            cucumberWaterTime = 180;
        }

        if (farmUpgrade.potatoFarmWater)
        {
            potatoWaterTime -= Time.deltaTime;
        }
        else if (!farmUpgrade.potatoFarmWater)
        {
            potatoWaterTime = 180;
        }

        if (farmUpgrade.onionFarmWater)
        {
            onionWaterTime -= Time.deltaTime;
        }
        else if (!farmUpgrade.onionFarmWater)
        {
            onionWaterTime = 180;
        }

        if (wheatWaterTime <= 0)
        {
            WheatWaterCol = 300;
            farmUpgrade.wheatFarmWater = false;
        }
        if (carrotWaterTime <= 0)
        {
            CarrotWaterCol = 300;
            farmUpgrade.carrotFarmWater = false;
        }
        if (cucumberWaterTime <= 0)
        {
            CucumberWaterCol = 300;
            farmUpgrade.cucumberFarmWater = false;
        }
        if (potatoWaterTime <= 0)
        {
            PotatoWaterCol = 300;
            farmUpgrade.potatoFarmWater = false;
        }
        if (onionWaterTime <= 0)
        {
            OnionWaterCol = 300;
            farmUpgrade.onionFarmWater = false;
        }
    }

    private void UpgradeStatusManage()
    {
        if (infraclick.hitObj != null)
        {
            switch (infraclick.hitObj.name)
            {
                case "Silo":
                    switch (siloLevel)
                    {
                        case 0:
                            UpgradeStatus = "0 => 300";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "300 => 500";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "500 => 800";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString("#,##0") + "$";
                            break;
                        case 3:
                            UpgradeStatus = "800 => 1000";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString("#,##0") + "$";
                            break;
                        case 4:
                            UpgradeStatus = "1000 => 1300";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString("#,##0") + "$";
                            break;
                        case 5:
                            UpgradeStatus = "1300 => 1500";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString("#,##0") + "$";
                            break;
                        case 6:
                            UpgradeStatus = "1500 => 1800";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString("#,##0") + "$";
                            break;
                        case 7:
                            UpgradeStatus = "1800 => 2000";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString("#,##0") + "$";
                            break;
                        case 8:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
                case "Storages":
                    switch (storageLevel)
                    {
                        case 0:
                            UpgradeStatus = "0 => 1500";
                            UpgradePriceText.text = storageUpgreadeCost[storageLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "1500 => 3000";
                            UpgradePriceText.text = storageUpgreadeCost[storageLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "3000 => 4500";
                            UpgradePriceText.text = storageUpgreadeCost[storageLevel].ToString("#,##0") + "$";
                            break;
                        case 3:
                            UpgradePriceText.text = "";
                            UpgradeStatus = "MAX LEVEL";
                            break;
                    }
                    break;
                case "GreenHouses":
                    switch (greenHouseLevel)
                    {
                        case 0:
                            UpgradeStatus = "0% => 50%";
                            UpgradePriceText.text = greenHouseUpgreadeCost[greenHouseLevel].ToString("#,##0") + "$";

                            break;
                        case 1:
                            UpgradeStatus = "50% => 100%";
                            UpgradePriceText.text = greenHouseUpgreadeCost[greenHouseLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
                case "Animal_Farms":
                    switch (animalFarmLevel)
                    {
                        case 0:
                            UpgradeStatus = "0% => 20%";
                            UpgradePriceText.text = animalFarmUpgreadeCost[animalFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "20% => 40%";
                            UpgradePriceText.text = animalFarmUpgreadeCost[animalFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
                case "Village":
                    switch (VillageLevel)
                    {
                        case 0:
                            UpgradeStatus = "0% => 5%";
                            UpgradePriceText.text = villageUpgreadCost[VillageLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "5% => 10%";
                            UpgradePriceText.text = villageUpgreadCost[VillageLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "10% => 15%";
                            UpgradePriceText.text = villageUpgreadCost[VillageLevel].ToString("#,##0") + "$";
                            break;
                        case 3:
                            UpgradeStatus = "15% => 20%";
                            UpgradePriceText.text = villageUpgreadCost[VillageLevel].ToString("#,##0") + "$";
                            break;
                        case 4:
                            UpgradeStatus = "20% => 25%";
                            UpgradePriceText.text = villageUpgreadCost[VillageLevel].ToString("#,##0") + "$";
                            break;
                        case 5:
                            UpgradeStatus = "25% => 30%";
                            UpgradePriceText.text = villageUpgreadCost[VillageLevel].ToString("#,##0") + "$";
                            break;
                        case 6:
                            UpgradeStatus = "30% => 35%";
                            UpgradePriceText.text = villageUpgreadCost[VillageLevel].ToString("#,##0") + "$";
                            break;
                        case 7:
                            UpgradeStatus = "35% => 40%";
                            UpgradePriceText.text = villageUpgreadCost[VillageLevel].ToString("#,##0") + "$";
                            break;
                        case 8:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
                case "Reservoir":
                    switch (ReservoirLevel)
                    {
                        case 0:
                            UpgradeStatus = "0% => -200%";
                            UpgradePriceText.text = reservoirUpgreadCost[ReservoirLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
                case "Wheat":
                    switch (farmUpgrade.wheatFarmLevel)
                    {
                        case 0:
                            UpgradeStatus = "+0/min => +20/min";
                            UpgradePriceText.text = wheatUpgreadeCost[farmUpgrade.wheatFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = wheatUpgreadeCost[farmUpgrade.wheatFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = wheatUpgreadeCost[farmUpgrade.wheatFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 3:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
                case "Carrot":
                    switch (farmUpgrade.carrotFarmLevel)
                    {
                        case 0:
                            UpgradeStatus = "+0/min => +20/min";
                            UpgradePriceText.text = carrotUpgreadeCost[farmUpgrade.carrotFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = carrotUpgreadeCost[farmUpgrade.carrotFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = carrotUpgreadeCost[farmUpgrade.carrotFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 3:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
                case "Cucumber":
                    switch (farmUpgrade.cucumberFarmLevel)
                    {
                        case 0:
                            UpgradeStatus = "+0/min => +20/min";
                            UpgradePriceText.text = cucumberUpgreadeCost[farmUpgrade.cucumberFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = cucumberUpgreadeCost[farmUpgrade.cucumberFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = cucumberUpgreadeCost[farmUpgrade.cucumberFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 3:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
                case "Potato":
                    switch (farmUpgrade.potatoFarmLevel)
                    {
                        case 0:
                            UpgradeStatus = "+0/min => +20/min";
                            UpgradePriceText.text = potatoUpgreadeCost[farmUpgrade.potatoFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = potatoUpgreadeCost[farmUpgrade.potatoFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = potatoUpgreadeCost[farmUpgrade.potatoFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 3:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
                case "Onion":
                    switch (farmUpgrade.onionFarmLevel)
                    {
                        case 0:
                            UpgradeStatus = "+0/min => +20/min";
                            UpgradePriceText.text = onionUpgreadeCost[farmUpgrade.onionFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = onionUpgreadeCost[farmUpgrade.onionFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = onionUpgreadeCost[farmUpgrade.onionFarmLevel].ToString("#,##0") + "$";
                            break;
                        case 3:
                            UpgradeStatus = "MAX LEVEL";
                            UpgradePriceText.text = "";
                            break;
                    }
                    break;
            }
        }
    }

    private void SiloUpgrade()
    {
        switch (siloLevel)
        {
            case 1:
                siloCont = 1;
                silo1Lev = 1;
                siloCapacity = 300;
                break;
            case 2:
                siloCont = 1;
                silo1Lev = 2;
                siloCapacity = 500;
                break;
            case 3:
                siloCont = 2;
                silo2Lev = 1;
                siloCapacity = 800;
                break;
            case 4:
                siloCont = 2;
                silo2Lev = 2;
                siloCapacity = 1000;
                break;
            case 5:
                siloCont = 3;
                silo3Lev = 1;
                siloCapacity = 1300;
                break;
            case 6:
                siloCont = 3;
                silo3Lev = 2;
                siloCapacity = 1500;
                break;
            case 7:
                siloCont = 4;
                silo4Lev = 1;
                siloCapacity = 1800;
                break;
            case 8:
                siloCont = 4;
                silo4Lev = 2;
                siloCapacity = 2000;
                break;
        }
    }

    private void SiloManage()
    {
        switch (siloCont)
        {
            case 0:
                silo1Lev1.SetActive(false);
                silo2Lev1.SetActive(false);
                silo3Lev1.SetActive(false);
                silo4Lev1.SetActive(false);

                silo1Lev2.SetActive(false);
                silo2Lev2.SetActive(false);
                silo3Lev2.SetActive(false);
                silo4Lev2.SetActive(false);
                break;
            case 1:
                if (silo1Lev == 1)
                {
                    silo1Lev1.SetActive(true);
                    silo1Lev2.SetActive(false);
                }
                else if (silo1Lev == 2)
                {
                    silo1Lev1.SetActive(false);
                    silo1Lev2.SetActive(true);
                }
                break;
            case 2:
                if (silo2Lev == 1)
                {
                    silo2Lev1.SetActive(true);
                    silo2Lev2.SetActive(false);
                }
                else if (silo2Lev == 2)
                {
                    silo2Lev1.SetActive(false);
                    silo2Lev2.SetActive(true);
                }
                break;
            case 3:
                if (silo3Lev == 1)
                {
                    silo3Lev1.SetActive(true);
                    silo3Lev2.SetActive(false);
                }
                else if (silo3Lev == 2)
                {
                    silo3Lev1.SetActive(false);
                    silo3Lev2.SetActive(true);
                }
                break;
            case 4:
                if (silo4Lev == 1)
                {
                    silo4Lev1.SetActive(true);
                    silo4Lev2.SetActive(false);
                }
                else if (silo4Lev == 2)
                {
                    silo4Lev1.SetActive(false);
                    silo4Lev2.SetActive(true);
                }
                break;
        }
    }

    private void StorageManage()
    {
        switch (storageCount)
        {
            case 0:
                storageCount = 0;
                storageCapacity = 0;
                storage1.SetActive(false);
                storage2.SetActive(false);
                storage3.SetActive(false);
                break;
            case 1:
                storageCount = 1;
                storageCapacity = 1500;
                storage1.SetActive(true);
                break;
            case 2:
                storageCount = 2;
                storageCapacity = 3000;
                storage2.SetActive(true);
                break;
            case 3:
                storageCount = 3;
                storageCapacity = 4500;
                storage3.SetActive(true);
                break;
        }
    }

    private void AnimalFarmManage()
    {
        switch (animalFarmCount)
        {
            case 0:
                animalFarmCount = 0;
                animalFarm1.SetActive(false);
                animalFarm2.SetActive(false);
                break;
            case 1:
                animalFarmCount = 1;
                animalFarm1.SetActive(true);
                break;
            case 2:
                animalFarmCount = 2;
                animalFarm2.SetActive(true);
                break;
        }
    }

    private void GreenHouseManage()
    {
        switch (greenHouseLevel)
        {
            case 0:
                ActiveGreenHouse = false;
                greenHouseCount = 0;
                ExtraProduction = 0;
                GreenHouse1.SetActive(false);
                GreenHouse2.SetActive(false);
                break;
            case 1:
                ActiveGreenHouse = true;
                greenHouseCount = 1;
                ExtraProduction = 1.5f;
                GreenHouse1.SetActive(true);
                break;
            case 2:
                ActiveGreenHouse = true;
                greenHouseCount = 2;
                ExtraProduction = 2f;
                GreenHouse2.SetActive(true);
                break;
        }
    }

    private void VillageManage()
    {
        switch (VillageLevel)
        {
            case 0:
                VillageCount = 0;
                Houses[0].SetActive(false);
                Houses[1].SetActive(false);
                Houses[2].SetActive(false);
                Houses[3].SetActive(false);
                Houses[4].SetActive(false);
                Houses[5].SetActive(false);
                Houses[6].SetActive(false);
                Houses[7].SetActive(false);
                break;
            case 1:
                VillageCount = 1;
                Houses[0].SetActive(true);
                break;
            case 2:
                VillageCount = 2;
                Houses[1].SetActive(true);
                break;
            case 3:
                VillageCount = 3;
                Houses[2].SetActive(true);
                break;
            case 4:
                VillageCount = 4;
                Houses[3].SetActive(true);
                break;
            case 5:
                VillageCount = 5;
                Houses[4].SetActive(true);
                break;
            case 6:
                VillageCount = 6;
                Houses[5].SetActive(true);
                break;
            case 7:
                VillageCount = 7;
                Houses[6].SetActive(true);
                break;
            case 8:
                VillageCount = 8;
                Houses[7].SetActive(true);
                break;
        }
    }

    private void ReservoirManage()
    {
        switch (ReservoirLevel)
        {
            case 0:
                ReservoirCount = 0;
                Water.SetActive(false);
                Ground.SetActive(true);
                break;
            case 1:
                ReservoirCount = 1;
                reservoirActive = true;
                Water.SetActive(true);
                Ground.SetActive(false);
                break;
        }
    }

    public void OnClickWater()
    {
        if (infraclick.hitObj.name == "Wheat")
        {
            if (!farmUpgrade.wheatFarmWater && WheatWaterCol <= 0)
            {
                farmUpgrade.wheatFarmWater = true;
            }
        }
        else if (infraclick.hitObj.name == "Carrot")
        {
            if (!farmUpgrade.carrotFarmWater && CarrotWaterCol <= 0)
            {
                farmUpgrade.carrotFarmWater = true;
            }
        }
        else if (infraclick.hitObj.name == "Cucumber")
        {
            if (!farmUpgrade.cucumberFarmWater && CarrotWaterCol <= 0)
            {
                farmUpgrade.cucumberFarmWater = true;
            }
        }
        else if (infraclick.hitObj.name == "Potato")
        {
            if (!farmUpgrade.potatoFarmWater && CarrotWaterCol <= 0)
            {
                farmUpgrade.potatoFarmWater = true;
            }
        }

        else if (infraclick.hitObj.name == "Onion")
        {
            if (!farmUpgrade.onionFarmWater && CarrotWaterCol <= 0)
            {
                farmUpgrade.onionFarmWater = true;
            }
        }
    }

    public void OnClickUpgrade()
    {
        if (infraclick.hitObj.name == "Silo")
        {
            if (siloLevel != 8)
            {
                if (mManager.Money < siloUpgreadeCost[siloLevel])
                {
                    return;
                }
                UpgradeCostManage(siloUpgreadeCost[siloLevel]);
                siloLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Storages")
        {
            if (storageLevel != 3)
            {
                if (mManager.Money < storageUpgreadeCost[storageLevel])
                {
                    return;
                }
                UpgradeCostManage(storageUpgreadeCost[storageLevel]);
                storageLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "GreenHouses")
        {
            if (greenHouseLevel != 2)
            {
                if (mManager.Money < greenHouseUpgreadeCost[greenHouseLevel])
                {
                    return;
                }
                UpgradeCostManage(greenHouseUpgreadeCost[greenHouseLevel]);
                greenHouseLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Animal_Farms")
        {
            if (animalFarmLevel != 2)
            {
                if (mManager.Money < animalFarmUpgreadeCost[animalFarmLevel])
                {
                    return;
                }
                UpgradeCostManage(animalFarmUpgreadeCost[animalFarmLevel]);
                mManager.MoneyExtra += 0.2f;
                animalFarmLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Village")
        {
            if (VillageLevel != 8)
            {
                if (mManager.Money < villageUpgreadCost[VillageLevel])
                {
                    return;
                }
                mManager.MoneyExtra += 0.05f;
                UpgradeCostManage(villageUpgreadCost[VillageLevel]);
                VillageLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Reservoir")
        {
            if (ReservoirLevel != 1)
            {
                if (mManager.Money < reservoirUpgreadCost[ReservoirLevel])
                {
                    return;
                }
                UpgradeCostManage(reservoirUpgreadCost[ReservoirLevel]);
                ReservoirLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Wheat")
        {
            if (farmUpgrade.wheatFarmLevel != 3)
            {
                if (mManager.Money < wheatUpgreadeCost[farmUpgrade.wheatFarmLevel])
                {
                    return;
                }
                wheatFramActive = true;
                UpgradeCostManage(wheatUpgreadeCost[farmUpgrade.wheatFarmLevel]);
                farmUpgrade.wheatFarmLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Carrot")
        {
            if (farmUpgrade.carrotFarmLevel != 3)
            {
                if (mManager.Money < carrotUpgreadeCost[farmUpgrade.carrotFarmLevel])
                {
                    return;
                }
                carrotFramActive = true;
                UpgradeCostManage(carrotUpgreadeCost[farmUpgrade.carrotFarmLevel]);
                farmUpgrade.carrotFarmLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Cucumber")
        {
            if (farmUpgrade.cucumberFarmLevel != 3)
            {
                if (mManager.Money < cucumberUpgreadeCost[farmUpgrade.cucumberFarmLevel])
                {
                    return;
                }
                cucumberFramActive = true;
                UpgradeCostManage(cucumberUpgreadeCost[farmUpgrade.cucumberFarmLevel]);
                farmUpgrade.cucumberFarmLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Potato")
        {
            if (farmUpgrade.potatoFarmLevel != 3)
            {
                if (mManager.Money < potatoUpgreadeCost[farmUpgrade.potatoFarmLevel])
                {
                    return;
                }
                potatoFramActive = true;
                UpgradeCostManage(potatoUpgreadeCost[farmUpgrade.potatoFarmLevel]);
                farmUpgrade.potatoFarmLevel += 1;
            }
        }

        else if (infraclick.hitObj.name == "Onion")
        {
            if (farmUpgrade.onionFarmLevel != 3)
            {
                if (mManager.Money < onionUpgreadeCost[farmUpgrade.onionFarmLevel])
                {
                    return;
                }
                onionFramActive = true;
                UpgradeCostManage(onionUpgreadeCost[farmUpgrade.onionFarmLevel]);
                farmUpgrade.onionFarmLevel += 1;
            }
        }
    }

    private void UpgradeCostManage(double cost)
    {
        mManager.Money -= cost;
    }
}

