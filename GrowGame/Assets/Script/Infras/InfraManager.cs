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

    private float[] siloUpgreadeCost = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};

    [Header("Storage")]
    public int storageLevel;
    public int storageCapacity;
    public int storageCount = 0;
    [SerializeField] private GameObject storage1;
    [SerializeField] private GameObject storage2;
    [SerializeField] private GameObject storage3;

    private float[] storageUpgreadeCost = { 0f, 0f, 0f};

    [Header("Animal_Farms")]
    public int animalFarmLevel;
    public int animalFarmCount = 0;
    [SerializeField] private GameObject animalFarm1;
    [SerializeField] private GameObject animalFarm2;

    private float[] animalFarmUpgreadeCost = { 0f, 0f};

    [Header("GreenHouse")]
    public int greenHouseLevel;
    public int greenHouseCount = 0;
    public int ExtraProduction = 0;
    [SerializeField] private GameObject GreenHouse1;
    [SerializeField] private GameObject GreenHouse2;

    private float[] greenHouseUpgreadeCost = { 0f, 0f};


    [Header("Farm")]
    public float WheatWaterCol;
    public float CarrotWaterCol;
    public float CucumberWaterCol;
    public float PotatoWaterCol;
    public float OnionWaterCol;

    [SerializeField] private float wheatWaterTime;
    [SerializeField] private float carrotWaterTime;
    [SerializeField] private float cucumberWaterTime;
    [SerializeField] private float potatoWaterTime;
    [SerializeField] private float onionWaterTime;

    [SerializeField] private Image WaterUIImage;

    private float[] wheatUpgreadeCost = { 0f, 50000f,10000f };
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
        StrageUpgrade();

        AnimalFarmUpgrade();
        AnimalFarmManage();

        GreenHouseUpgrade();
        GreenHouseManage();

        UpgradeStatusManage();
    }

    private void WaterManage()
    {
        if (WheatWaterCol > 0)
        {
            WheatWaterCol -= Time.deltaTime;
        }
        if (CarrotWaterCol > 0)
        {
            CarrotWaterCol -= Time.deltaTime;
        }
        if (CucumberWaterCol > 0)
        {
            CucumberWaterCol -= Time.deltaTime;
        }
        if (PotatoWaterCol > 0)
        {
            PotatoWaterCol -= Time.deltaTime;
        }
        if (OnionWaterCol > 0)
        {
            OnionWaterCol -= Time.deltaTime;
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
            WheatWaterCol = 480;
            farmUpgrade.wheatFarmWater = false;
        }
        if (carrotWaterTime <= 0)
        {
            CarrotWaterCol = 480;
            farmUpgrade.carrotFarmWater = false;
        }
        if (cucumberWaterTime <= 0)
        {
            CucumberWaterCol = 480;
            farmUpgrade.cucumberFarmWater = false;
        }
        if (potatoWaterTime <= 0)
        {
            PotatoWaterCol = 480;
            farmUpgrade.potatoFarmWater = false;
        }
        if (onionWaterTime <= 0)
        {
            OnionWaterCol = 480;
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
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString() + "$";
                            break;
                        case 1:
                            UpgradeStatus = "300 => 500";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString() + "$";
                            break;
                        case 2:
                            UpgradeStatus = "500 => 800";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString() + "$";
                            break;
                        case 3:
                            UpgradeStatus = "800 => 1000";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString() + "$";
                            break;
                        case 4:
                            UpgradeStatus = "1000 => 1300";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString() + "$";
                            break;
                        case 5:
                            UpgradeStatus = "1300 => 1500";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString() + "$";
                            break;
                        case 6:
                            UpgradeStatus = "1500 => 1800";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString() + "$";
                            break;
                        case 7:
                            UpgradeStatus = "1800 => 2000";
                            UpgradePriceText.text = siloUpgreadeCost[siloLevel].ToString() + "$";
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
                            UpgradePriceText.text = storageUpgreadeCost[storageLevel].ToString() + "$";
                            break;
                        case 1:
                            UpgradeStatus = "1500 => 3000";
                            UpgradePriceText.text = storageUpgreadeCost[storageLevel].ToString() + "$";
                            break;
                        case 2:
                            UpgradeStatus = "3000 => 4500";
                            UpgradePriceText.text = storageUpgreadeCost[storageLevel].ToString() + "$";
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
                            UpgradeStatus = "0% => 5%";
                            UpgradePriceText.text = greenHouseUpgreadeCost[greenHouseLevel].ToString() + "$";

                            break;
                        case 1:
                            UpgradeStatus = "5% => 10%";
                            UpgradePriceText.text = greenHouseUpgreadeCost[greenHouseLevel].ToString() + "$";
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
                            UpgradePriceText.text = animalFarmUpgreadeCost[animalFarmLevel].ToString() + "$";
                            break;
                        case 1:
                            UpgradeStatus = "20% => 40%";
                            UpgradePriceText.text = animalFarmUpgreadeCost[animalFarmLevel].ToString() + "$";
                            break;
                        case 2:
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
                            UpgradePriceText.text = wheatUpgreadeCost[farmUpgrade.wheatFarmLevel].ToString() + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = wheatUpgreadeCost[farmUpgrade.wheatFarmLevel].ToString() + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = wheatUpgreadeCost[farmUpgrade.wheatFarmLevel].ToString() + "$";
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
                            UpgradePriceText.text = carrotUpgreadeCost[farmUpgrade.carrotFarmLevel].ToString() + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = carrotUpgreadeCost[farmUpgrade.carrotFarmLevel].ToString() + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = carrotUpgreadeCost[farmUpgrade.carrotFarmLevel].ToString() + "$";
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
                            UpgradePriceText.text = cucumberUpgreadeCost[farmUpgrade.cucumberFarmLevel].ToString() + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = cucumberUpgreadeCost[farmUpgrade.cucumberFarmLevel].ToString() + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = cucumberUpgreadeCost[farmUpgrade.cucumberFarmLevel].ToString() + "$";
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
                            UpgradePriceText.text = potatoUpgreadeCost[farmUpgrade.potatoFarmLevel].ToString() + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = potatoUpgreadeCost[farmUpgrade.potatoFarmLevel].ToString() + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = potatoUpgreadeCost[farmUpgrade.potatoFarmLevel].ToString() + "$";
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
                            UpgradePriceText.text = onionUpgreadeCost[farmUpgrade.onionFarmLevel].ToString() + "$";
                            break;
                        case 1:
                            UpgradeStatus = "+25/min => +50/min";
                            UpgradePriceText.text = onionUpgreadeCost[farmUpgrade.onionFarmLevel].ToString() + "$";
                            break;
                        case 2:
                            UpgradeStatus = "+50/min => +75/min";
                            UpgradePriceText.text = onionUpgreadeCost[farmUpgrade.onionFarmLevel].ToString() + "$";
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

    private void StrageUpgrade()
    {
        switch (storageLevel)
        {
            case 1:
                storageCount = 1;
                storageCapacity = 1500;
                break;
            case 2:
                storageCount = 2;
                storageCapacity = 3000;
                break;
            case 3:
                storageCount = 3;
                storageCapacity = 4500;
                break;
        }
    }

    private void StorageManage()
    {
        switch (storageCount)
        {
            case 0:
                storage1.SetActive(false);
                storage2.SetActive(false);
                storage3.SetActive(false);
                break;
            case 1:
                storage1.SetActive(true);
                break;
            case 2:
                storage2.SetActive(true);
                break;
            case 3:
                storage3.SetActive(true);
                break;
        }
    }

    private void AnimalFarmUpgrade()
    {
        switch (animalFarmLevel)
        {
            case 1:
                animalFarmCount = 1;
                break;
            case 2:
                animalFarmCount = 2;
                break;
        }
    }
    private void AnimalFarmManage()
    {
        switch (animalFarmCount)
        {
            case 0:
                animalFarm1.SetActive(false);
                animalFarm2.SetActive(false);
                break;
            case 1:
                animalFarm1.SetActive(true);
                break;
            case 2:
                animalFarm2.SetActive(true);
                break;
        }
    }

    private void GreenHouseUpgrade()
    {
        switch (greenHouseLevel)
        {
            case 1:
                greenHouseCount = 1;
                ExtraProduction = 5;
                break;
            case 2:
                greenHouseCount = 2;
                ExtraProduction = 10;
                break;
        }
    }

    private void GreenHouseManage()
    {
        switch (greenHouseCount)
        {
            case 0:
                GreenHouse1.SetActive(false);
                GreenHouse2.SetActive(false);
                break;
            case 1:
                GreenHouse1.SetActive(true);
                break;
            case 2:
                GreenHouse2.SetActive(true);
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
                animalFarmLevel += 1;
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
                UpgradeCostManage(onionUpgreadeCost[farmUpgrade.onionFarmLevel]);
                farmUpgrade.onionFarmLevel += 1;
            }
        }
    }

    private void UpgradeCostManage(float cost)
    {
        mManager.Money -= cost;
    }
}

