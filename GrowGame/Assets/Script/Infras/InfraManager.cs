using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InfraManager : MonoBehaviour
{
    [SerializeField] private InfraClick infraclick;

    public string UpgradeStatus;

    [Header("Slio")]
    public int siloLevel = 0;

    public int siloCont = 0;
    public int silo1Lev = 1;
    public int silo2Lev = 1;
    public int silo3Lev = 1;
    public int silo4Lev = 1;
    public int siloCapacity;

    [SerializeField] private GameObject silo1Lev1;
    [SerializeField] private GameObject silo2Lev1;
    [SerializeField] private GameObject silo3Lev1;
    [SerializeField] private GameObject silo4Lev1;
    [SerializeField] private GameObject silo1Lev2;
    [SerializeField] private GameObject silo2Lev2;
    [SerializeField] private GameObject silo3Lev2;
    [SerializeField] private GameObject silo4Lev2;

    [Header("Storage")]
    public int storageLevel;
    public int storageCapacity;
    public int storageCount = 0;
    [SerializeField] private GameObject storage1;
    [SerializeField] private GameObject storage2;
    [SerializeField] private GameObject storage3;

    [Header("Animal_Farms")]
    public int animalFarmLevel;
    public int animalFarmCount = 0;
    [SerializeField] private GameObject animalFarm1;
    [SerializeField] private GameObject animalFarm2;

    [Header("GreenHouse")]
    public int greenHouseLevel;
    public int greenHouseCount = 0;
    [SerializeField] private GameObject GreenHouse1;
    [SerializeField] private GameObject GreenHouse2;


    [Header("Farms")]
    public int wheatFarmLevel;
    public int carrotFarmLevel;
    public int cucumberFarmLevel;
    public int potatoFarmLevel;
    public int onionFarmLevel;

    public bool wheatFarmWater;
    public bool carrotFarmWater;
    public bool cucumberFarmWater;
    public bool potatoFarmWater;
    public bool onionFarmWater;

    private void Update()
    {
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
                            break;
                        case 1:
                            UpgradeStatus = "300 => 500";
                            break;
                        case 2:
                            UpgradeStatus = "500 => 800";
                            break;
                        case 3:
                            UpgradeStatus = "800 => 1000";
                            break;
                        case 4:
                            UpgradeStatus = "1000 => 1300";
                            break;
                        case 5:
                            UpgradeStatus = "1300 => 1500";
                            break;
                        case 6:
                            UpgradeStatus = "1500 => 1800";
                            break;
                        case 7:
                            UpgradeStatus = "1800 => 2000";
                            break;
                        case 8:
                            UpgradeStatus = "MAX LEVEL";
                            break;
                    }
                    break;
                case "Storages":
                    switch (storageLevel)
                    {
                        case 0:
                            UpgradeStatus = "0 => 1500";
                            break;
                        case 1:
                            UpgradeStatus = "1500 => 3000";
                            break;
                        case 2:
                            UpgradeStatus = "3000 => 4500";
                            break;
                        case 3:
                            UpgradeStatus = "MAX LEVEL";
                            break;
                    }
                    break;
                /*case "GreenHouses":
                    switch (greenHouseLevel)
                    {

                    }
                    break;*/
                case "Animal_Farms":
                    switch (animalFarmLevel)
                    {
                        case 0:
                            UpgradeStatus = "0% => 20%";
                            break;
                        case 1:
                            UpgradeStatus = "20% => 40%";
                            break;
                        case 2:
                            UpgradeStatus = "MAX LEVEL";
                            break;
                    }
                    break;
            }
        }
    }

    private void SiloUpgrade()
    {
        switch(siloLevel)
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
                if(silo1Lev == 1)
                {
                    silo1Lev1.SetActive(true);
                    silo1Lev2.SetActive(false);
                }
                else if(silo1Lev == 2)
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
                break;
            case 2:
                greenHouseCount = 2;
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

    public void OnClickUpgrade()
    {
        if (infraclick.hitObj.name == "Silo")
        {
            if (siloLevel != 8)
            {
                siloLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Storages")
        {
            if (storageLevel != 3)
            {
                storageLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "GreenHouses")
        {
            if (greenHouseLevel != 2)
            {
                greenHouseLevel += 1;
            }
        }
        else if (infraclick.hitObj.name == "Animal_Farms")
        {
            if (animalFarmLevel != 2)
            {
                animalFarmLevel += 1;
            }
        }
    }
}
