using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public InfraManager IManager;

    public int basicCrops = 3500;
    public int totalCrops => basicCrops + IManager.siloCapacity + 
        IManager.storageCapacity;

    public float nowWheat;
    public float nowCarrot;
    public float nowCucumber;
    public float nowPotato;
    public float nowOnion;
    public float nowCrops => nowWheat + nowCarrot + nowCucumber + nowPotato + nowOnion;

    private float[] perSecondBase = { 25f / 60f, 50f / 60f, 75f / 60f };
    private float[] perSecondWater = { 50f / 60f, 100f / 60f, 150f / 60f };

    public void Update()
    {
        if (nowCrops < totalCrops)
        {
            GrowWheat();
        }
    }

    public void GrowWheat()
    {
        if (!IManager.wheatFarmWater)
        {
            switch (IManager.wheatFarmLevel)
            {
                case 1:
                    nowWheat += perSecondBase[0] * Time.deltaTime;
                    break;
                case 2:
                    nowWheat += perSecondBase[1] * Time.deltaTime;
                    break;
                case 3:
                    nowWheat += perSecondBase[2] * Time.deltaTime;
                    break;
            }
        }
        else
        {
            switch (IManager.wheatFarmLevel)
            {
                case 1:
                    nowWheat += perSecondWater[0] * Time.deltaTime;
                    break;
                case 2:
                    nowWheat += perSecondWater[1] * Time.deltaTime;
                    break;  
                case 3:
                    nowWheat += perSecondWater[2] * Time.deltaTime;
                    break;
            }
        }
       
    }
}
