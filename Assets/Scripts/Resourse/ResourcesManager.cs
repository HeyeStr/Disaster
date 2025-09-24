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
