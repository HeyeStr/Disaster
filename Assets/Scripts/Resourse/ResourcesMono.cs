using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesMono : MonoBehaviour
{
    public int LivingResource;
    public int FoodResource;
    public int MedicalResource;
    void Start()
    {
        LivingResource = 0;
        FoodResource = 0;
        MedicalResource = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddLivingResource(int addition)
    {
        LivingResource += addition;
    }
    public void DeclineLivingResource(int decline)
    {
        LivingResource -= decline;
    }
    public void AddFoodResource(int addition)
    {
        FoodResource += addition;
    }
    public void DeclineFoodResource(int decline)
    {
        FoodResource -= decline;
    }
    public void AddMedicalResource(int addition)
    {
        MedicalResource += addition;
    }
    public void DeclineMedicalResource(int decline)
    {
        MedicalResource -= decline;
    }
}
