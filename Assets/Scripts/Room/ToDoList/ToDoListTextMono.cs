using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct Mission
{
    public string MissionName;
    public int MissionIndex;                                    //任务代号
    public List<string> Informations;
}

public class ToDoListTextMono : MonoBehaviour
{
    public GameObject TextPrefab;
    public List<Mission> Missions;
    void Start()
    {
        Missions = new List<Mission>();
        Missions.Add(new Mission
        {
            MissionName = "王朝烈马",
            MissionIndex = 0,
            Informations = new List<string>()
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
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
                GameObject NewText_Information = GameObject.Instantiate(TextPrefab);
                NewText_Information.transform.parent = TextCanvasTransform;
                NewText_Information.transform.position = new Vector3(transform.position.x, 2.5f - i * 0.5f, 0 );
                NewText_Information.GetComponent<TextMeshProUGUI>().text = Information;
                break;
            }
        }
    }
}
