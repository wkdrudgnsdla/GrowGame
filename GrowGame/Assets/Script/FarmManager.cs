using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public InfraManager IManager;

    public int basicCrops = 3500;
    public int totalCrops => basicCrops + IManager.siloCapacity + 
        IManager.storageCapacity;

    public int nowWheat;
    public int nowCarrot;
    public int nowCucumber;
    public int nowPotato;
    public int nowOnion;
    public int nowCrops => nowWheat + nowCarrot + nowCucumber + nowPotato + nowOnion;

}
