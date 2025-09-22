using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeBarMono : MonoBehaviour
{
    private GameObject DistributeControlObject;
    public  int missionIndex;
    public string Address;
    public string PhoneNumber;
    public int LivingResourceQuantity;
    public int FoodResourceQuantity;
    public int MedicalResourceQuantity;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Submit()
    {
        DistributeControlObject = GameObject.FindGameObjectWithTag("DistributeControl");
        DistributeControlObject.GetComponent<DistributeControlMono>().missions
        if ()
    }
    public void DeleteInformation()
    {

    }
}
