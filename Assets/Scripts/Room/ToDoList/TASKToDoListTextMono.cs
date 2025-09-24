using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class TaskToDoListTextMono : MonoBehaviour
{
    public List<Mission> Missions;
    int x;

    void Start()
    {
        Missions = new List<Mission>();

        AddTask(0);
        //AddTask(1);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G) && x == 0)
        {
            Debug.Log("ggggggggggg");
            AddInformation(0, "1398765432", true);
            AddInformation(0, "食品类：3份额", false);
            AddInformation(0, "本站共接收周边受灾群众42人", false);
            AddInformation(0, "食品类：3份额", false);
            AddInformation(0, "安置点负责人周老师 139 8765 432", false);
            x++;
        }
    }
    public void AddInformation(int MissionIndex, string Information,bool isTelephone)
    {
        Debug.Log("AddInformation");

        foreach (var Mission in Missions)
        {
            if (Mission.MissionIndex == MissionIndex)
            {
                
                
                Mission.Informations.Add ( new Information { information= Information ,IsTelephone=isTelephone});
                gameObject.GetComponent<ToDoList>().DisplayTaskPage(MissionIndex);
                break;
            }
        }
    }
    public void DeleteInformation(string Information,int page)
    {
        
        for(int i=0;i< Missions[page].Informations.Count; i++)
        {
            if (Missions[page].Informations[i].information == Information) {
                Missions[page].Informations.Remove(Missions[page].Informations[i]);

                ToDoList todolist = transform.gameObject.GetComponent<ToDoList>();
                todolist.DisplayTaskPage(Missions[page].MissionIndex);
            }
        }
    }
    public void AddTask(int Missionindex)
    {
        GameObject Monitor = GameObject.FindGameObjectWithTag("Monitor");
        AllMissionsMono allMissionsMono = Monitor.GetComponent<AllMissionsMono>();
        Debug.Log("AddTask");
        int i = 0;
        while (i < Missions.Count) {
            i++;
        }
        Debug.Log("Missions"+ Missions);
        Missions.Add(new Mission
        {
            MissionIndex = Missionindex,
            Informations = new List<Information>(),
            Address = allMissionsMono.GetAddress(Missionindex),
            PhoneNumber=allMissionsMono.GetPhoneNumber(Missionindex)
        });

        ToDoList todolist = transform.gameObject.GetComponent<ToDoList>();
        transform.gameObject.GetComponent<ToDoList>().totalPages++;
        todolist.DisplayTaskPage(i);
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
