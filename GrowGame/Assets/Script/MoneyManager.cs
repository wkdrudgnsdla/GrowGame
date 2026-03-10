using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private FarmManager fManager;

    public double Money;
    public float TotalSellPrice => ((int) fManager.nowWheat * 500 + (int) fManager.nowCarrot* 900 + (int) fManager.nowCucumber* 1400 + (int) fManager.nowPotato* 2200 + (int) fManager.nowOnion* 3500) * MoneyExtra;

    public float MoneyExtra = 1;


    public void OnClickSell()
    {
        Money += TotalSellPrice;
        fManager.nowWheat -= (int)fManager.nowWheat;
        fManager.nowCarrot -= (int)fManager.nowCarrot;
        fManager.nowCucumber -= (int)fManager.nowCucumber;
        fManager.nowPotato -= (int)fManager.nowPotato;
        fManager.nowOnion -= (int)fManager.nowOnion;
    }
}
