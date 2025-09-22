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
        DistributeObject = GameObject.FindGameObjectWithTag("Distribute");
        DistributeObject.GetComponent<DistributeBarMono>().Submit();

    }
}
