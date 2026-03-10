using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public FarmUpgrade FU;
    public InfraManager IM;

    public int basicCrops = 3500;
    public int totalCrops => basicCrops + IM.siloCapacity +
        IM.storageCapacity;

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
        if (!IM.ActiveGreenHouse)
        {
            if (!FU.wheatFarmWater)
            {
                switch (FU.wheatFarmLevel)
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
                switch (FU.wheatFarmLevel)
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
        else if (IM.ActiveGreenHouse)
        {
            if (!FU.wheatFarmWater)
            {
                switch (FU.wheatFarmLevel)
                {
                    case 1:
                        nowWheat += perSecondBase[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowWheat += perSecondBase[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowWheat += perSecondBase[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
            else
            {
                switch (FU.wheatFarmLevel)
                {
                    case 1:
                        nowWheat += perSecondWater[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowWheat += perSecondWater[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowWheat += perSecondWater[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
        }


    }

    public void GrowCarrot()
    {
        if (!IM.ActiveGreenHouse)
        {
            if (!FU.carrotFarmWater)
            {
                switch (FU.carrotFarmLevel)
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
                switch (FU.carrotFarmLevel)
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
        else if (IM.ActiveGreenHouse)
        {
            if (!FU.carrotFarmWater)
            {
                switch (FU.carrotFarmLevel)
                {
                    case 1:
                        nowCarrot += perSecondBase[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowCarrot += perSecondBase[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowCarrot += perSecondBase[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
            else
            {
                switch (FU.carrotFarmLevel)
                {
                    case 1:
                        nowCarrot += perSecondWater[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowCarrot += perSecondWater[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowCarrot += perSecondWater[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
        }

    }

    public void GrowCucumber()
    {
        if (!IM.ActiveGreenHouse)
        {
            if (!FU.cucumberFarmWater)
            {
                switch (FU.cucumberFarmLevel)
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
                switch (FU.cucumberFarmLevel)
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
        else if(IM.ActiveGreenHouse)
        {
            if (!FU.cucumberFarmWater)
            {
                switch (FU.cucumberFarmLevel)
                {
                    case 1:
                        nowCucumber += perSecondBase[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowCucumber += perSecondBase[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowCucumber += perSecondBase[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
            else
            {
                switch (FU.cucumberFarmLevel)
                {
                    case 1:
                        nowCucumber += perSecondWater[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowCucumber += perSecondWater[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowCucumber += perSecondWater[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
        }

    }

    public void GrowPotato()
    {
        if (!IM.ActiveGreenHouse)
        {
            if (!FU.potatoFarmWater)
            {
                switch (FU.potatoFarmLevel)
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
                switch (FU.potatoFarmLevel)
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
        else if (IM.ActiveGreenHouse)
        {
            if (!FU.potatoFarmWater)
            {
                switch (FU.potatoFarmLevel)
                {
                    case 1:
                        nowPotato += perSecondBase[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowPotato += perSecondBase[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowPotato += perSecondBase[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
            else
            {
                switch (FU.potatoFarmLevel)
                {
                    case 1:
                        nowPotato += perSecondWater[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowPotato += perSecondWater[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowPotato += perSecondWater[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
        }
        

    }

    public void GrowOnion()
    {
        if (!IM.ActiveGreenHouse)
        {
            if (!FU.onionFarmWater)
            {
                switch (FU.onionFarmLevel)
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
                switch (FU.onionFarmLevel)
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
        if (IM.ActiveGreenHouse)
        {
            if (!FU.onionFarmWater)
            {
                switch (FU.onionFarmLevel)
                {
                    case 1:
                        nowOnion += perSecondBase[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowOnion += perSecondBase[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowOnion += perSecondBase[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
            else
            {
                switch (FU.onionFarmLevel)
                {
                    case 1:
                        nowOnion += perSecondWater[0] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 2:
                        nowOnion += perSecondWater[1] * Time.deltaTime * IM.ExtraProduction;
                        break;
                    case 3:
                        nowOnion += perSecondWater[2] * Time.deltaTime * IM.ExtraProduction;
                        break;
                }
            }
        }

    }
}
