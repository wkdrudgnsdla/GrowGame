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

    private void Update()
    {
        WheatManage();
    }

    private void WheatManage()
    {
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
}
