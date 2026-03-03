using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private FarmManager fManager;

    public double Money;
    public float TotalSellPrice;

    private void FixedUpdate()
    {
        TotalSellPrice = (int)fManager.nowWheat * 500 + (int)fManager.nowCarrot * 900 + (int)fManager.nowCucumber * 1400 + (int)fManager.nowPotato * 2200 + (int)fManager.nowOnion * 3500;
    }

    public void OnClickSell()
    {
        Money += TotalSellPrice;
        fManager.nowWheat = 0;
        fManager.nowCarrot = 0;
        fManager.nowCucumber = 0;
        fManager.nowPotato = 0;
        fManager.nowOnion = 0;
    }
}
