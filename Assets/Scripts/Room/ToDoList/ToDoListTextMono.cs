/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Mission
{
    public string Mission;
    public int MissionIndex;                                    //任务代号
    public List<string> Informations;
}

public class ToDoListTextMono : MonoBehaviour
{
    public GameObject TextPrefab;
    public List<Mission> Missions;
    void Start()
    {
        
        Missions.Add(new Mission
        {
            Mission = "王朝烈马",
            MissionIndex = 0,
            Informations = new List<string>()
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
    void AddInformation(int MissionIndex,string Information)
    {
        Transform TextCanvasTransform= transform.Find("TextCanvas");
        foreach (var Mission in Missions)
        {
            if (Mission.MissionIndex = MissionIndex)
            {
                int i = 0;
                while (i < Mission.Informations.Count)
                {
                    i++;
                }
                GameObject NewText_Information = GameObject.Instantiate(TextPrefab);
                NewText_Information.transform.parent = TextCanvasTransform;
                NewText_Information.transform.position=new Vector3(0, 0, 2.5 - i * 0.5);
                NewText_Information.GetComponent<TextMeshProUGUI>().text = Information;
                break;
            }
        }
    }
}
*/