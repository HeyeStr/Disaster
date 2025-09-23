using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDistributeBarMono : MonoBehaviour
{
    public GameObject DistributeBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnMouseDown()
    {
        Debug.Log("Delete");
        GameObject Monitor = GameObject.FindGameObjectWithTag("Monitor");
        ResourcesManager RS= Monitor.GetComponent<ResourcesManager>();
        DistributeBarMono distributeBarMono=DistributeBar.GetComponent<DistributeBarMono>();
        RS.FoodResource += distributeBarMono.FoodResourceQuantity;
        RS.LivingResource += distributeBarMono.LivingResourceQuantity;
        RS.MedicalResource += distributeBarMono.MedicalResourceQuantity;
        GameObject distributePage = GameObject.FindGameObjectWithTag("DistributePage");
        var distributebars= distributePage.GetComponent<DistributeControlMono>().DistributeBars;

        for (int i = 0; i < distributebars.Count; i++)
        {
            if (distributebars[i] == DistributeBar)
            {
                distributebars.Remove(distributebars[i]);
            }
        }
        Destroy(DistributeBar);
        
    }
}
