using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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
        CalculatePoints calculatePoints= gameObject.GetComponent<CalculatePoints>();
        foreach (GameObject distributBar in DistributeBars) {
            int ScoreofOne = distributBar.GetComponent<DistributeBarMono>().Submit();
            DistributeBars.Remove(distributBar);
            Destroy(distributBar);
        
        }
    }

}
