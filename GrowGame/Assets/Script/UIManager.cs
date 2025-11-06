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

    public void Update()
    {
        nowCropsText.text = ((int)fManager.nowCrops).ToString() + "/" + ((int)fManager.totalCrops).ToString();
        nowWheatText.text = ((int)fManager.nowWheat).ToString();
        nowCarrotText.text = ((int)fManager.nowCarrot).ToString();
        nowCucumberText.text = ((int)fManager.nowCucumber).ToString();
        nowPotatoText.text = ((int)fManager.nowPotato).ToString();
        nowOnionText.text = ((int)fManager.nowOnion).ToString();
    }
}
