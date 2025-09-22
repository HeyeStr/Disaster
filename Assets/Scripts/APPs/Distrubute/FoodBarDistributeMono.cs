using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBarDistributeMono : MonoBehaviour
{
    public GameObject MonitorObject;
    public int FoodDistributeQuantity;
    public int NewFoodQuantity;
    public int MissionIndex;
    ResourcesManager SourceManager;
    void Start()
    {
        MonitorObject = GameObject.FindGameObjectWithTag("Monitor");
        SourceManager = MonitorObject.GetComponent<ResourcesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NewFoodQuantity != FoodDistributeQuantity)
        {
            int TotalFoodResource = SourceManager.FoodResource;
            if (NewFoodQuantity > FoodDistributeQuantity)
            {
                

                if (TotalFoodResource - NewFoodQuantity + FoodDistributeQuantity < 0)
                {
                    //food资源小于0，不能这样操作！！！！！！！！！！！！！
                }
                else
                {
                    TotalFoodResource -= NewFoodQuantity - FoodDistributeQuantity;

                    SourceManager.FoodResource = TotalFoodResource;
                }
            }
            else
            {
                TotalFoodResource += FoodDistributeQuantity - NewFoodQuantity;
                SourceManager.MedicalResource = TotalFoodResource;
                FoodDistributeQuantity = NewFoodQuantity;
            }
        }
    }
}
