using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Mission
{
    public int MissionIndex;                                    //ÈÎÎñ´úºÅ
    public string Address;
    public string PhoneNumber;
    public List<Information> Informations;
     
}
[System.Serializable]
public struct Information
{
    public string information;
    public bool IsTelephone;
} 

[System.Serializable]
public struct MissionInformation
{
    public int MissionInedx;
    public string Address;
    public string PhoneNumber;
    public int FoodResource;
    public int MedicineResource;
    public int LivingResource;
}