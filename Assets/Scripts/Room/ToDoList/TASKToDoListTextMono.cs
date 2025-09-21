using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class TaskToDoListTextMono : MonoBehaviour
{
    public GameObject TextPrefab;
    public List<Mission> Missions;
    void Start()
    {
        Missions = new List<Mission>();

        AddTask("王朝烈马", 0);
        Debug.Log(Missions[0].MissionName);
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
        Transform TextCanvasTransform = transform.Find("TextCanvas");
        foreach (var Mission in Missions)
        {
            if (Mission.MissionIndex == MissionIndex)
            {
                int i = 0;
                while (i < Mission.Informations.Count)
                {
                    i++;
                }
                Mission.Informations.Add ( Information);
                GameObject NewText_Information = GameObject.Instantiate(TextPrefab);
                NewText_Information.transform.parent = TextCanvasTransform;
                NewText_Information.transform.position = new Vector3(transform.position.x, 2.5f - i * 0.5f, 0 );
                NewText_Information.GetComponent<TextMeshProUGUI>().text = Information;
                break;
            }
        }
    }
    public void AddTask(string Missionname,int Missionindex)
    {
        Debug.Log("AddTask");
        int i = 0;
        while (i < Missions.Count) {
            i++;
        }
        Missions.Add(new Mission { 
            MissionName= Missionname,
            MissionIndex=Missionindex,
            Informations=new List<string>()
        });
        ToDoList todolist = transform.gameObject.GetComponent<ToDoList>();
        transform.gameObject.GetComponent<ToDoList>().totalPages++;
        todolist.UpdatePageContent();
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
