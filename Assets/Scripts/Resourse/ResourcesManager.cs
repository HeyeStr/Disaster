using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public int LivingResource;
    public int FoodResource;
    public int MedicalResource;

    
    void Start()
    {
        LivingResource = 3; FoodResource = 4; MedicalResource = 1;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetLivingResource(int Quantity)
    {
        LivingResource = Quantity;
    }
    
    public void SetFoodResource(int Quantity)
    {
        FoodResource = Quantity;
    }
    public void SetMedicalResource(int Quantity)
    {
        MedicalResource = Quantity;
    }
}
