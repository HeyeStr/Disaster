using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeControlMono : MonoBehaviour
{
    public GameObject ToDoList;

    public List<GameObject> DistributeBars;
    void Start()
    {
        ToDoList = GameObject.FindGameObjectWithTag("ToDoList");
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SubmitAll()
    {
        foreach (GameObject distributBar in DistributeBars) {
            distributBar.GetComponent<DistributeBarMono>().Submit();
            DistributeBars.Remove(distributBar);
            Destroy(distributBar);
        
        }
    }

}
