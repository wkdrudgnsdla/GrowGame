using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FarmManager fManager;
    [SerializeField] private MoneyManager mManager;

    [SerializeField] private GameObject SellUI;
    [SerializeField] private GameObject SellPanel;

    [Header("texts")]
    //texts
    [SerializeField] private Text nowCropsText;
    [SerializeField] private Text nowWheatText;
    [SerializeField] private Text nowCarrotText;
    [SerializeField] private Text nowCucumberText;
    [SerializeField] private Text nowPotatoText;
    [SerializeField] private Text nowOnionText;

    [SerializeField] private Text sellUI_nowWheatText;
    [SerializeField] private Text sellUI_nowCarrotText;
    [SerializeField] private Text sellUI_nowCucumberText;
    [SerializeField] private Text sellUI_nowPotatoText;
    [SerializeField] private Text sellUI_nowOnionText;

    [SerializeField] private Text moneyText;
    [SerializeField] private Text TotalPriceText;

    [Header("infraButtons")]
    //Infras
    [SerializeField] private GameObject siloButton;
    [SerializeField] private GameObject storagesButton;
    [SerializeField] private GameObject animalFarmsButton;
    [SerializeField] private GameObject greenHousesButton;
    [SerializeField] private GameObject sellButton;
    [SerializeField] private GameObject VillageButton;
    [SerializeField] private GameObject ReservoirButton;

    //Farms
    [SerializeField] private GameObject WheatFieldButton;
    [SerializeField] private GameObject CarrotFieldButton;
    [SerializeField] private GameObject CucumberFieldButton;
    [SerializeField] private GameObject PotatoFieldButton;
    [SerializeField] private GameObject OnionFieldButton;

    [Header("infraPosition")]
    //Infras
    [SerializeField] private GameObject silo;
    [SerializeField] private GameObject storages;
    [SerializeField] private GameObject animalFarms;
    [SerializeField] private GameObject greenHouses;
    [SerializeField] private GameObject house;
    [SerializeField] private GameObject Village;
    [SerializeField] private GameObject Reservoir;

    //Farms
    [SerializeField] private GameObject WheatField;
    [SerializeField] private GameObject CarrotField;
    [SerializeField] private GameObject CucumberField;
    [SerializeField] private GameObject PotatoField;
    [SerializeField] private GameObject OnionField;

    private void Start()
    {
        SellUI.SetActive(false);
        SellPanel.SetActive(false);
    }

    private void Update()
    {
        UIPositionUpdaate();

        if (SellUI.activeSelf)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SellUI.SetActive(false);
                SellPanel.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        nowCropsText.text = ((int)fManager.nowCrops).ToString() + "/" + ((int)fManager.totalCrops).ToString();
        nowWheatText.text = ((int)fManager.nowWheat).ToString();
        nowCarrotText.text = ((int)fManager.nowCarrot).ToString();
        nowCucumberText.text = ((int)fManager.nowCucumber).ToString();
        nowPotatoText.text = ((int)fManager.nowPotato).ToString();
        nowOnionText.text = ((int)fManager.nowOnion).ToString();

        sellUI_nowWheatText.text = ((int)fManager.nowWheat).ToString();
        sellUI_nowCarrotText.text = ((int)fManager.nowCarrot).ToString();
        sellUI_nowCucumberText.text = ((int)fManager.nowCucumber).ToString();
        sellUI_nowPotatoText.text = ((int)fManager.nowPotato).ToString();
        sellUI_nowOnionText.text = ((int)fManager.nowOnion).ToString();

        moneyText.text = mManager.Money.ToString("#,##0") + "$";
        TotalPriceText.text = ((int)mManager.TotalSellPrice).ToString("#,##0") + "$";
    }

    private void UIPositionUpdaate()
    {
        siloButton.transform.position = Camera.main.WorldToScreenPoint(silo.transform.position);
        storagesButton.transform.position = Camera.main.WorldToScreenPoint(storages.transform.position);
        animalFarmsButton.transform.position = Camera.main.WorldToScreenPoint(animalFarms.transform.position);
        greenHousesButton.transform.position = Camera.main.WorldToScreenPoint(greenHouses.transform.position);
        sellButton.transform.position = Camera.main.WorldToScreenPoint(house.transform.position);
        VillageButton.transform.position = Camera.main.WorldToScreenPoint(Village.transform.position);
        ReservoirButton.transform.position = Camera.main.WorldToScreenPoint(Reservoir.transform.position);

        WheatFieldButton.transform.position = Camera.main.WorldToScreenPoint(WheatField.transform.position);
        CarrotFieldButton.transform.position = Camera.main.WorldToScreenPoint(CarrotField.transform.position);
        CucumberFieldButton.transform.position = Camera.main.WorldToScreenPoint(CucumberField.transform.position);
        PotatoFieldButton.transform.position = Camera.main.WorldToScreenPoint(PotatoField.transform.position);
        OnionFieldButton.transform.position = Camera.main.WorldToScreenPoint(OnionField.transform.position);
    }

    public void OnClickSellUI()
    {
        if (SellUI.activeSelf) return;
        SellUI.SetActive(true);
        SellPanel.SetActive(true);
    }
}
