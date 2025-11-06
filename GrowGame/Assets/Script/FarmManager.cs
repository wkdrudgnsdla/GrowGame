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
            GrowCarrot();
            GrowCucumber();
            GrowPotato();
            GrowOnion();
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

    public void GrowCarrot()
    {
        if (!IManager.carrotFarmWater)
        {
            switch (IManager.carrotFarmLevel)
            {
                case 1:
                    nowCarrot += perSecondBase[0] * Time.deltaTime;
                    break;
                case 2:
                    nowCarrot += perSecondBase[1] * Time.deltaTime;
                    break;
                case 3:
                    nowCarrot += perSecondBase[2] * Time.deltaTime;
                    break;
            }
        }
        else
        {
            switch (IManager.carrotFarmLevel)
            {
                case 1:
                    nowCarrot += perSecondWater[0] * Time.deltaTime;
                    break;
                case 2:
                    nowCarrot += perSecondWater[1] * Time.deltaTime;
                    break;
                case 3:
                    nowCarrot += perSecondWater[2] * Time.deltaTime;
                    break;
            }
        }

    }

    public void GrowCucumber()
    {
        if (!IManager.cucumberFarmWater)
        {
            switch (IManager.cucumberFarmLevel)
            {
                case 1:
                    nowCucumber += perSecondBase[0] * Time.deltaTime;
                    break;
                case 2:
                    nowCucumber += perSecondBase[1] * Time.deltaTime;
                    break;
                case 3:
                    nowCucumber += perSecondBase[2] * Time.deltaTime;
                    break;
            }
        }
        else
        {
            switch (IManager.cucumberFarmLevel)
            {
                case 1:
                    nowCucumber += perSecondWater[0] * Time.deltaTime;
                    break;
                case 2:
                    nowCucumber += perSecondWater[1] * Time.deltaTime;
                    break;
                case 3:
                    nowCucumber += perSecondWater[2] * Time.deltaTime;
                    break;
            }
        }

    }

    public void GrowPotato()
    {
        if (!IManager.potatoFarmWater)
        {
            switch (IManager.potatoFarmLevel)
            {
                case 1:
                    nowPotato += perSecondBase[0] * Time.deltaTime;
                    break;
                case 2:
                    nowPotato += perSecondBase[1] * Time.deltaTime;
                    break;
                case 3:
                    nowPotato += perSecondBase[2] * Time.deltaTime;
                    break;
            }
        }
        else
        {
            switch (IManager.potatoFarmLevel)
            {
                case 1:
                    nowPotato += perSecondWater[0] * Time.deltaTime;
                    break;
                case 2:
                    nowPotato += perSecondWater[1] * Time.deltaTime;
                    break;
                case 3:
                    nowPotato += perSecondWater[2] * Time.deltaTime;
                    break;
            }
        }

    }

    public void GrowOnion()
    {
        if (!IManager.onionFarmWater)
        {
            switch (IManager.onionFarmLevel)
            {
                case 1:
                    nowOnion += perSecondBase[0] * Time.deltaTime;
                    break;
                case 2:
                    nowOnion += perSecondBase[1] * Time.deltaTime;
                    break;
                case 3:
                    nowOnion += perSecondBase[2] * Time.deltaTime;
                    break;
            }
        }
        else
        {
            switch (IManager.onionFarmLevel)
            {
                case 1:
                    nowOnion += perSecondWater[0] * Time.deltaTime;
                    break;
                case 2:
                    nowOnion += perSecondWater[1] * Time.deltaTime;
                    break;
                case 3:
                    nowOnion += perSecondWater[2] * Time.deltaTime;
                    break;
            }
        }

    }
}
