using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBarDistributeMono : MonoBehaviour
{
    public GameObject MonitorObject;
    public int LivingDistributeQuantity;
    public int NewLivingQuantity;
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
        if (NewLivingQuantity != LivingDistributeQuantity)
        {
            int TotalLivingResource = SourceManager.LivingResource;
            if (NewLivingQuantity > LivingDistributeQuantity)
            {
                

                if (TotalLivingResource - NewLivingQuantity + LivingDistributeQuantity < 0)
                {
                    //food资源小于0，不能这样操作！！！！！！！！！！！！！
                }
                else
                {
                    TotalLivingResource -= NewLivingQuantity - LivingDistributeQuantity;

                    SourceManager.LivingResource = TotalLivingResource;
                }
            }
            else
            {
                TotalLivingResource += LivingDistributeQuantity - NewLivingQuantity;
                SourceManager.MedicalResource = TotalLivingResource;
                LivingDistributeQuantity = NewLivingQuantity;
            }
        }
    }
}
