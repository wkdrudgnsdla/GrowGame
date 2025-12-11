using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public FarmManager fManager;

    [SerializeField] private Text nowCropsText;
    [SerializeField] private Text nowWheatText;
    [SerializeField] private Text nowCarrotText;
    [SerializeField] private Text nowCucumberText;
    [SerializeField] private Text nowPotatoText;
    [SerializeField] private Text nowOnionText;

    [Header("infraButtons")]
    //Infras
    [SerializeField] private GameObject siloButton;
    [SerializeField] private GameObject storagesButton;
    [SerializeField] private GameObject animalFarmsButton;
    [SerializeField] private GameObject greenHousesButton;

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

    //Farms
    [SerializeField] private GameObject WheatField;
    [SerializeField] private GameObject CarrotField;
    [SerializeField] private GameObject CucumberField;
    [SerializeField] private GameObject PotatoField;
    [SerializeField] private GameObject OnionField;

    public void Update()
    {
        nowCropsText.text = ((int)fManager.nowCrops).ToString() + "/" + ((int)fManager.totalCrops).ToString();
        nowWheatText.text = ((int)fManager.nowWheat).ToString();
        nowCarrotText.text = ((int)fManager.nowCarrot).ToString();
        nowCucumberText.text = ((int)fManager.nowCucumber).ToString();
        nowPotatoText.text = ((int)fManager.nowPotato).ToString();
        nowOnionText.text = ((int)fManager.nowOnion).ToString();
        UIPositionUpdaate();
    }

    public void UIPositionUpdaate()
    {
        siloButton.transform.position = Camera.main.WorldToScreenPoint(silo.transform.position);
        storagesButton.transform.position = Camera.main.WorldToScreenPoint(storages.transform.position);
        animalFarmsButton.transform.position = Camera.main.WorldToScreenPoint(animalFarms.transform.position);
        greenHousesButton.transform.position = Camera.main.WorldToScreenPoint(greenHouses.transform.position);

        WheatFieldButton.transform.position = Camera.main.WorldToScreenPoint(WheatField.transform.position);
        CarrotFieldButton.transform.position = Camera.main.WorldToScreenPoint(CarrotField.transform.position);
        CucumberFieldButton.transform.position = Camera.main.WorldToScreenPoint(CucumberField.transform.position);
        PotatoFieldButton.transform.position = Camera.main.WorldToScreenPoint(PotatoField.transform.position);
        OnionFieldButton.transform.position = Camera.main.WorldToScreenPoint(OnionField.transform.position);
    }
}
