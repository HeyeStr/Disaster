using Distribute;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitBar : MonoBehaviour
{

    private GameObject DistributeObject;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("SubmitBar");
        DistributeObject = GameObject.FindGameObjectWithTag("DistributePage");
        DistributeObject.GetComponent<DistributeControlMono>().SubmitAll();
        

    }
}
