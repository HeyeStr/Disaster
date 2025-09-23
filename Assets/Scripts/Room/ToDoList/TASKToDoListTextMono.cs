using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class TaskToDoListTextMono : MonoBehaviour
{
    public List<Mission> Missions;

    void Start()
    {
        Missions = new List<Mission>();

        AddTask("王朝烈马", 0);
        Debug.Log(Missions[0].MissionName);
        AddTask("78游戏", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            AddInformation(0, "王朝烈马");

        }
    }
    public void AddInformation(int MissionIndex, string Information)
    {
        Debug.Log("AddInformation");

        foreach (var Mission in Missions)
        {
            if (Mission.MissionIndex == MissionIndex)
            {
                
                
                Mission.Informations.Add ( Information);
                gameObject.GetComponent<ToDoList>().DisplayTaskPage(MissionIndex);
                break;
            }
        }
    }
    public void AddTask(string Missionname,int Missionindex)
    {
        GameObject Monitor = GameObject.FindGameObjectWithTag("Monitor");
        AllMissionsMono allMissionsMono = Monitor.GetComponent<AllMissionsMono>();
        Debug.Log("AddTask");
        int i = 0;
        while (i < Missions.Count) {
            i++;
        }
        Missions.Add(new Mission
        {
            MissionName = Missionname,
            MissionIndex = Missionindex,
            Informations = new List<string>(),
            Address = allMissionsMono.GetAddress(Missionindex),
            PhoneNumber=allMissionsMono.GetPhoneNumber(Missionindex)
        });

        ToDoList todolist = transform.gameObject.GetComponent<ToDoList>();
        transform.gameObject.GetComponent<ToDoList>().totalPages++;
        todolist.DisplayTaskPage(0);
    }
    public bool HasTask(int MissionIndex)
    {
        foreach(var mission in Missions)
        {
            if (mission.MissionIndex == MissionIndex)
            {
                return true;
            }
        }
        return false;
        
    }

    public int GetTaskPage(int MissionIndex)
    {
        for (int i=0;i<Missions.Count;i++) {
            if (MissionIndex == Missions[i].MissionIndex)
            {
                return i;
            }
        }
        return 0;
    }
    public Vector3 GetNewInformationPosition(int Missionindex)
    {
        foreach (var Mission in Missions)
        {
            if (Mission.MissionIndex == Missionindex)
            {
                int i = 0;
                while (i < Mission.Informations.Count)
                {
                    i++;
                }
               return new Vector3(transform.position.x, 2.5f - i * 0.5f, 0);
            }
        }
        return new Vector3(transform.position.x, 2.5f, 0);
    }
    
}
