using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Mission
{
    public string MissionName;
    public int MissionIndex;                                    //ÈÎÎñ´úºÅ
    public string Address;
    public string PhoneNumber;
    public List<string> Informations;
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