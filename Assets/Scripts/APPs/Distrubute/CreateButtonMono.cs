using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateButtonMono : MonoBehaviour
{
    public GameObject DistributePageObj;
    void Start()
    {
        DistributePageObj = GameObject.FindGameObjectWithTag("DistributePage");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        DistributePageObj.GetComponent<Distribute.DistributePageButton>().CreateNewMessage();
    }
}
