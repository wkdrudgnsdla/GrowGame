using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FarmUpgrade : MonoBehaviour
{
    [SerializeField] private FarmManager FM;

    [Header("Wheat")]
    [SerializeField] private List<GameObject> wheatFarm = new List<GameObject>();
    [SerializeField] private List<GameObject> wheat_water = new List<GameObject>();

    [Header("Carrot")]
    [SerializeField] private List<GameObject> carrotFarm = new List<GameObject>();
    [SerializeField] private List<GameObject> carrot_water = new List<GameObject>();

    [Header("Cucumber")]
    [SerializeField] private List<GameObject> cucumberFarm = new List<GameObject>();
    [SerializeField] private List<GameObject> cucumber_water = new List<GameObject>();

    [Header("Potato")]
    [SerializeField] private List<GameObject> potatoFarm = new List<GameObject>();
    [SerializeField] private List<GameObject> potato_water = new List<GameObject>();

    [Header("Onion")]
    [SerializeField] private List<GameObject> onionFarm = new List<GameObject>();
    [SerializeField] private List<GameObject> onion_water = new List<GameObject>();

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

    private int lastWheatLevel = -1;
    private bool lastWheatWater = false;
    private int lastCarrotLevel = -1;
    private bool lastCarrotWater = false;
    private int lastCucumberLevel = -1;
    private bool lastCucumberWater = false;
    private int lastPotatoLevel = -1;
    private bool lastPotatoWater = false;
    private int lastOnioinLevel = -1;
    private bool lastOnioinWater = false;

    private void Update()
    {
        WheatManage();
        CarrotManage();
        CucumberManage();
        PotatoManage();
        OnionManage();
    }

    private void WheatManage()
    {
        if (wheatFarmLevel == lastWheatLevel && wheatFarmWater == lastWheatWater) return;

        lastWheatLevel = wheatFarmLevel;
        lastWheatWater = wheatFarmWater;

        switch (wheatFarmLevel)
        {
            case 1:
                wheatFarm[0].SetActive(false);
                if (!wheatFarmWater)
                {
                    wheatFarm[1].SetActive(true);
                    wheat_water[0].SetActive(false);
                }
                else
                {
                    wheatFarm[1].SetActive(false);
                    wheat_water[0].SetActive(true);
                }
                break;
            case 2:
                wheatFarm[1].SetActive(false);
                wheat_water[0].SetActive(false);
                if (!wheatFarmWater)
                {
                    wheatFarm[2].SetActive(true);
                    wheat_water[1].SetActive(false);
                }
                else
                {
                    wheat_water[1].SetActive(true);
                    wheatFarm[2].SetActive(false);
                }
                break;
            case 3:
                wheatFarm[2].SetActive(false);
                wheat_water[1].SetActive(false);
                if (!wheatFarmWater)
                {
                    wheatFarm[3].SetActive(true);
                    wheat_water[2].SetActive(false);
                }
                else
                {
                    wheatFarm[3].SetActive(false);
                    wheat_water[2].SetActive(true);
                }
                break;
        }
    }

    private void CarrotManage()
    {
        if (carrotFarmLevel == lastCarrotLevel && carrotFarmWater == lastCarrotWater) return;

        lastCarrotLevel = carrotFarmLevel;
        lastCarrotWater = carrotFarmWater;

        switch (carrotFarmLevel)
        {
            case 1:
                carrotFarm[0].SetActive(false);
                if (!carrotFarmWater)
                {
                    carrotFarm[1].SetActive(true);
                    carrot_water[0].SetActive(false);
                }
                else
                {
                    carrotFarm[1].SetActive(false);
                    carrot_water[0].SetActive(true);
                }
                break;
            case 2:
                carrotFarm[1].SetActive(false);
                carrot_water[0].SetActive(false);
                if (!carrotFarmWater)
                {
                    carrotFarm[2].SetActive(true);
                    carrot_water[1].SetActive(false);
                }
                else
                {
                    carrotFarm[2].SetActive(false);
                    carrot_water[1].SetActive(true);
                }
                break;
            case 3:
                carrotFarm[2].SetActive(false);
                carrot_water[1].SetActive(false);
                if (!carrotFarmWater)
                {
                    carrotFarm[3].SetActive(true);
                    carrot_water[2].SetActive(false);
                }
                else
                {
                    carrotFarm[3].SetActive(false);
                    carrot_water[2].SetActive(true);
                }
                break;
        }
    }

    private void CucumberManage()
    {
        if (cucumberFarmLevel == lastCucumberLevel && cucumberFarmWater == lastCucumberWater) return;

        lastCucumberLevel = cucumberFarmLevel;
        lastCucumberWater = cucumberFarmWater;

        switch (cucumberFarmLevel)
        {
            case 1:
                cucumberFarm[0].SetActive(false);
                if (!cucumberFarmWater)
                {
                    cucumberFarm[1].SetActive(true);
                    cucumber_water[0].SetActive(false);
                }
                else
                {
                    cucumberFarm[1].SetActive(false);
                    cucumber_water[0].SetActive(true);
                }
                break;
            case 2:
                cucumberFarm[1].SetActive(false);
                cucumber_water[0].SetActive(false);
                if (!cucumberFarmWater)
                {
                    cucumberFarm[2].SetActive(true);
                    cucumber_water[1].SetActive(false);
                }
                else
                {
                    cucumberFarm[2].SetActive(false);
                    cucumber_water[1].SetActive(true);
                }
                break;
            case 3:
                cucumberFarm[2].SetActive(false);
                cucumber_water[1].SetActive(false);
                if (!cucumberFarmWater)
                {
                    cucumberFarm[3].SetActive(true);
                    cucumber_water[2].SetActive(false);
                }
                else
                {
                    cucumberFarm[3].SetActive(false);
                    cucumber_water[2].SetActive(true);
                }
                break;
        }
    }

    private void PotatoManage()
    {
        if (potatoFarmLevel == lastPotatoLevel && potatoFarmWater == lastPotatoWater) return;

        lastPotatoLevel = potatoFarmLevel;
        lastPotatoWater = potatoFarmWater;

        switch (potatoFarmLevel)
        {
            case 1:
                potatoFarm[0].SetActive(false);
                if (!potatoFarmWater)
                {
                    potatoFarm[1].SetActive(true);
                    potato_water[0].SetActive(false);
                }
                else
                {
                    potatoFarm[1].SetActive(false);
                    potato_water[0].SetActive(true);
                }
                break;
            case 2:
                potatoFarm[1].SetActive(false);
                potato_water[0].SetActive(false);
                if (!potatoFarmWater)
                {
                    potatoFarm[2].SetActive(true);
                    potato_water[1].SetActive(false);
                }
                else
                {
                    potatoFarm[2].SetActive(false);
                    potato_water[1].SetActive(true);
                }
                break;
            case 3:
                potatoFarm[2].SetActive(false);
                potato_water[1].SetActive(false);
                if (!potatoFarmWater)
                {
                    potatoFarm[3].SetActive(true);
                    potato_water[2].SetActive(false);
                }
                else
                {
                    potatoFarm[3].SetActive(false);
                    potato_water[2].SetActive(true);
                }
                break;
        }
    }

    private void OnionManage()
    {
        if (onionFarmLevel == lastOnioinLevel && onionFarmWater == lastOnioinWater) return;

        lastOnioinLevel = onionFarmLevel;
        lastOnioinWater = onionFarmWater;

        switch (onionFarmLevel)
        {
            case 1:
                onionFarm[0].SetActive(false);
                if (!onionFarmWater)
                {
                    onionFarm[1].SetActive(true);
                    onion_water[0].SetActive(false);
                }
                else
                {
                    onionFarm[1].SetActive(false);
                    onion_water[0].SetActive(true);
                }
                break;
            case 2:
                onionFarm[1].SetActive(false);
                onion_water[0].SetActive(false);
                if (!onionFarmWater)
                {
                    onionFarm[2].SetActive(true);
                    onion_water[1].SetActive(false);
                }
                else
                {
                    onionFarm[2].SetActive(false);
                    onion_water[1].SetActive(true);
                }
                break;
            case 3:
                onionFarm[2].SetActive(false);
                onion_water[1].SetActive(false);
                if (!onionFarmWater)
                {
                    onionFarm[3].SetActive(true);
                    onion_water[2].SetActive(false);
                }
                else
                {
                    onionFarm[3].SetActive(false);
                    onion_water[2].SetActive(true);
                }
                break;
        }
    }
}
