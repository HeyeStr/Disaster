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
    public GameObject ScoreDemand;
    private int scoredemand;


    void Start()
    {
        ToDoList = GameObject.FindGameObjectWithTag("ToDoList");
        GameObject GameDayManagerObj = GameObject.FindGameObjectWithTag("GameDayManager");
        GameDayManager gameDayManager = GameDayManagerObj.GetComponent<GameDayManager>();
         scoredemand = gameDayManager.DayScoreDemand[gameDayManager.currentDay - 1];
        ScoreDemand.GetComponent<TextMeshProUGUI>().text = scoredemand.ToString();
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
        foreach (GameObject distributBar in DistributeBars)
        {
            TotalScore += distributBar.GetComponent<DistributeBarMono>().Submit();
            Score.GetComponent<TextMeshProUGUI>().text = TotalScore.ToString();
            
            

        }
        GameObject GameDayManagerObj = GameObject.FindGameObjectWithTag("GameDayManager");
        GameDayManager gameDayManager= GameDayManagerObj.GetComponent<GameDayManager>();
        scoredemand = gameDayManager.DayScoreDemand[gameDayManager.currentDay - 1];
        if(TotalScore>= scoredemand)
        {
            gameDayManager.NextDay();
            GameObject monitorobj = GameObject.FindGameObjectWithTag("Monitor");
            SceneControlMono sceneControlMono = monitorobj.GetComponent<SceneControlMono>();
            //sceneControlMono.UnloadDistributeScene();
        }
        else
        {
            Debug.Log("failed");
            GameObject monitorobj = GameObject.FindGameObjectWithTag("Monitor");
            SceneControlMono sceneControlMono= monitorobj.GetComponent<SceneControlMono>();
            sceneControlMono.loadFailScene();
        }


            DistributeBars.Clear();
    }

}
