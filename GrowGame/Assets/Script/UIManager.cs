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
    [SerializeField] private GameObject siloButton;
    [SerializeField] private GameObject storagesButton;
    [SerializeField] private GameObject animalFarmsButton;
    [SerializeField] private GameObject greenHousesButton;

    [Header("infraPosition")]
    [SerializeField] private GameObject silo;
    [SerializeField] private GameObject storages;
    [SerializeField] private GameObject animalFarms;
    [SerializeField] private GameObject greenHouses;

    public void Update()
    {
        nowCropsText.text = ((int)fManager.nowCrops).ToString() + "/" + ((int)fManager.totalCrops).ToString();
        nowWheatText.text = ((int)fManager.nowWheat).ToString();
        nowCarrotText.text = ((int)fManager.nowCarrot).ToString();
        nowCucumberText.text = ((int)fManager.nowCucumber).ToString();
        nowPotatoText.text = ((int)fManager.nowPotato).ToString();
        nowOnionText.text = ((int)fManager.nowOnion).ToString();
    }

    public void FixedUpdate()
    {
        siloButton.transform.position = Camera.main.WorldToScreenPoint(silo.transform.position);
        storagesButton.transform.position = Camera.main.WorldToScreenPoint(storages.transform.position);
        animalFarmsButton.transform.position = Camera.main.WorldToScreenPoint(animalFarms.transform.position);
        greenHousesButton.transform.position = Camera.main.WorldToScreenPoint(greenHouses.transform.position);
    }
}
