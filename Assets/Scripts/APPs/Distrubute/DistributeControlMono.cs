using Distribute;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class DistributeControlMono : MonoBehaviour
{
    public GameObject ToDoList;
    public List<GameObject> DistributeBars;
    public GameObject Score;
    public GameObject ScoreDemand;
    private int scoredemand;
    public float fadeDurTime;

    void Start()
    {
        ToDoList = GameObject.FindGameObjectWithTag("ToDoList");
        GameObject GameDayManagerObj = GameObject.FindGameObjectWithTag("GameDayManager");
        GameDayManager gameDayManager = GameDayManagerObj.GetComponent<GameDayManager>();
         scoredemand = gameDayManager.DayScoreDemand[gameDayManager.currentDay - 1];
        ScoreDemand.GetComponent<TextMeshProUGUI>().text = scoredemand.ToString();
        fadeDurTime = 3;
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
        GameObject todolistobj = GameObject.FindGameObjectWithTag("ToDoList");
        todolistobj.GetComponent<TaskToDoListTextMono>().Missions.Clear();
            
         StartCoroutine(Wait(TotalScore, 2f));
            

        
        
    }
    private IEnumerator Wait(int TotalScore, float DurTime)
    {
        for (int i = 0; i < DistributeBars.Count; i++) {
            float timer = 0f;
            int InitialScore = TotalScore;
            TotalScore += DistributeBars[i].GetComponent<DistributeBarMono>().Submit();
            int FinalScore = TotalScore;
            while (timer < DurTime)
            {
                timer += Time.deltaTime;
                float progress = Mathf.Clamp01(timer / DurTime);
                Score.GetComponent<TextMeshProUGUI>().text = math.lerp(InitialScore, FinalScore, progress).ToString();
                yield return null;
            }
            Score.GetComponent<TextMeshProUGUI>().text = FinalScore.ToString();
            Destroy(DistributeBars[i]);
        }
        float timing = 0f;
        while (timing < DurTime/2)
        {
            timing += Time.deltaTime;
            yield return null;
        }
        GameObject GameDayManagerObj = GameObject.FindGameObjectWithTag("GameDayManager");
        GameDayManager gameDayManager = GameDayManagerObj.GetComponent<GameDayManager>();
        scoredemand = gameDayManager.DayScoreDemand[gameDayManager.currentDay - 1];
        Debug.Log("hereeeeeeeeeeeeeeee");
        gameObject.GetComponent<DistributePageButton>().CommitPlan();
        if (TotalScore >= scoredemand)
        {
            if (gameDayManager.currentDay != 4)
            {
                gameDayManager.NextDay();
                StartCoroutine(FadeOutPass());
            }
            else
            {
                StartCoroutine(FadeOutSuccess());
            }


        }
        else
        {
            Debug.Log("failed");
            StartCoroutine(FadeOutFail());

        }


        DistributeBars.Clear();
    }
    private IEnumerator FadeOutPass()
    {
        float timer = 0f;
        while (timer < fadeDurTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        GameObject monitorobj = GameObject.FindGameObjectWithTag("Monitor");
        SceneControlMono sceneControlMono = monitorobj.GetComponent<SceneControlMono>();
        
        sceneControlMono.LoadFadeInScene();
        sceneControlMono.loadDeskScene();
        sceneControlMono.UnloadDistributeScene();
    }
    private IEnumerator FadeOutFail()
    {
        float timer = 0f;
        while (timer < fadeDurTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        GameObject monitorobj = GameObject.FindGameObjectWithTag("Monitor");
        SceneControlMono sceneControlMono = monitorobj.GetComponent<SceneControlMono>();
        sceneControlMono.loadFailScene();
    }
    private IEnumerator FadeOutSuccess()
    {
        float timer = 0f;
        while (timer < fadeDurTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        GameObject monitorobj = GameObject.FindGameObjectWithTag("Monitor");
        SceneControlMono sceneControlMono = monitorobj.GetComponent<SceneControlMono>();
        sceneControlMono.LoadSuccessScene();
    }

}
