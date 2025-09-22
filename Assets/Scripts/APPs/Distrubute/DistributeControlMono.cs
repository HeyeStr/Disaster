using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeControlMono : MonoBehaviour
{
    public GameObject Mission_DistributeBar;
    public GameObject ToDoList;
    void Start()
    {
        ToDoList = GameObject.FindGameObjectWithTag("ToDoList");
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
