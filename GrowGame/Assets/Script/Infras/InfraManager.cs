using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InfraManager : MonoBehaviour
{
    [Header("Slio")]
    public int siloCont = 0;
    public int silo1Lev = 1;
    public int silo2Lev = 1;
    public int silo3Lev = 1;
    public int silo4Lev = 1;

    private GameObject silo1Lev1;
    private GameObject silo2Lev1;
    private GameObject silo3Lev1;
    private GameObject silo4Lev1;
    private GameObject silo1Lev2;
    private GameObject silo2Lev2;
    private GameObject silo3Lev2;
    private GameObject silo4Lev2;

    [Header("Storage")]
    public int storageCount = 0;
    private GameObject storage1;
    private GameObject storage2;
    private GameObject storage3;

    [Header("Animal_Farms")]
    public int animalFarmCount = 0;
    private GameObject animalFarm1;
    private GameObject animalFarm2;

    [Header("GreenHouse")]
    public int greenHouseCount = 0;
    private GameObject GreenHouse1;
    private GameObject GreenHouse2;

    private void Awake()
    {
        //silo
        silo1Lev1 = GameObject.Find("lev1Silo1");
        silo2Lev1 = GameObject.Find("lev1Silo2");
        silo3Lev1 = GameObject.Find("lev1Silo3");
        silo4Lev1 = GameObject.Find("lev1Silo4");
        silo1Lev2 = GameObject.Find("lev2Silo1");
        silo2Lev2 = GameObject.Find("lev2Silo2");
        silo3Lev2 = GameObject.Find("lev2Silo3");
        silo4Lev2 = GameObject.Find("lev2Silo4");

        //storage
        storage1 = GameObject.Find("Storage1");
        storage2 = GameObject.Find("Storage2");
        storage3 = GameObject.Find("Storage3");

        //animal_Farm
        animalFarm1 = GameObject.Find("Animal_Farm1");
        animalFarm2 = GameObject.Find("Animal_Farm2");

        GreenHouse1 = GameObject.Find("GreenHouse1");
        GreenHouse2 = GameObject.Find("GreenHouse2");
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        SiloManage();
        StorageManage();
        AnimalFarmManage();
        GreenHouseManage();
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
            case <=1:
                if(silo1Lev == 1)
                {
                    silo1Lev1.SetActive(true);
                    silo1Lev2.SetActive(false);
                }
                else if(silo1Lev == 2)
                {
                    silo1Lev1.SetActive(false);
                    silo1Lev2 .SetActive(true);
                }
                break;
            case <=2:
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
            case <=3:
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
}
