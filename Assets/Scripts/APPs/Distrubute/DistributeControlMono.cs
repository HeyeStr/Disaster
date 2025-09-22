using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeControlMono : MonoBehaviour
{
    public GameObject Mission_DistributeBar;
    public GameObject ToDoList;
    public  List<Mission> missions;
    void Start()
    {
        ToDoList = GameObject.FindGameObjectWithTag("ToDoList");
        missions = ToDoList.GetComponent<TaskToDoListTextMono>().Missions;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
