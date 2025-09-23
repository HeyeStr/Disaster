using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class DistributeControlMono : MonoBehaviour
{
    public GameObject ToDoList;
    public List<GameObject> DistributeBars;
    public GameObject Score;
   
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
        Debug.Log("SubmitAllllllllllllllllllllllllllllllllllllllllllllllllllllllllllll");
        CalculatePoints calculatePoints= gameObject.GetComponent<CalculatePoints>();
        int TotalScore = 0;
        foreach (GameObject distributBar in DistributeBars) {
            TotalScore += distributBar.GetComponent<DistributeBarMono>().Submit();
            Score.GetComponent<TextMeshProUGUI>().text=TotalScore.ToString();

        }

        DistributeBars.Clear();
    }

}
