using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMissionsMono : MonoBehaviour
{
    public AllMissionsData allMissionsData;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GetAddress(int MissionInedx)
    {
        for (int i = 0; i < allMissionsData.AllMisions.Count; i++) {
            if (allMissionsData.AllMisions[i].MissionInedx == MissionInedx) {

                return allMissionsData.AllMisions[i].Address;


            }
        }
        return "";
    }
    public string GetPhoneNumber(int MissionInedx)
    {
        for (int i = 0; i < allMissionsData.AllMisions.Count; i++)
        {
            if (allMissionsData.AllMisions[i].MissionInedx == MissionInedx)
            {

                return allMissionsData.AllMisions[i].PhoneNumber;


            }
        }
        return "";
    }
    public int GetFoodResource(int MissionInedx)
    {
        for (int i = 0; i < allMissionsData.AllMisions.Count; i++)
        {
            if (allMissionsData.AllMisions[i].MissionInedx == MissionInedx)
            {

                return allMissionsData.AllMisions[i].FoodResource;


            }
        }
        return 0;
    }
    public int GetMedicineResource(int MissionInedx)
    {
        for (int i = 0; i < allMissionsData.AllMisions.Count; i++)
        {
            if (allMissionsData.AllMisions[i].MissionInedx == MissionInedx)
            {

                return allMissionsData.AllMisions[i].MedicineResource;


            }
        }
        return 0;
    }
    public int GetLivingResource(int MissionInedx)
    {
        for (int i = 0; i < allMissionsData.AllMisions.Count; i++)
        {
            if (allMissionsData.AllMisions[i].MissionInedx == MissionInedx)
            {

                return allMissionsData.AllMisions[i].LivingResource;


            }
        }
        return 0;
    }

}
