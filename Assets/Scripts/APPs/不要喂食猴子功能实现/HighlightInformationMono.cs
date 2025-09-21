using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class HighlightInformationMono : MonoBehaviour
{
    GameObject gameObjectList;
    public bool HighLightStringStarttoMove;
    public float MoveSpeed;


    public string StringInformation;                     //任务相关信息
    public string missionName;                          //任务相关信息
    public int missionIndex;                            //任务相关信息
    void Start()
    {
        HighLightStringStarttoMove = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (HighLightStringStarttoMove)
        {
            // 获取目标位置的世界坐标
            gameObjectList = GameObject.FindGameObjectWithTag("ToDoList");
            TaskToDoListTextMono tasktoDoListTextMono = gameObjectList.GetComponent<TaskToDoListTextMono>();
            ToDoList toDoList = gameObjectList.GetComponent<ToDoList>();
            if (!tasktoDoListTextMono.HasTask(missionIndex, missionName))
            {
                tasktoDoListTextMono.AddTask(missionName, missionIndex);
                
                int currentpage= tasktoDoListTextMono.GetTaskPage(missionIndex, missionName);
                toDoList.currentPage = currentpage;
                toDoList.UpdatePageContent();
            }
            else
            {
                int currentpage = tasktoDoListTextMono.GetTaskPage(missionIndex, missionName);
                toDoList.currentPage = currentpage;
                toDoList.UpdatePageContent();
            }


            Vector3 Targetposition = tasktoDoListTextMono.GetNewInformationPosition(0);                  //0待定
            

            transform.position= math.lerp(transform.position, Targetposition, 0.1f);
            //transform.position +=  MoveSpeed * Time.deltaTime *  (Vector3)math.normalize(new float3(Targetposition.x - transform.position.x, Targetposition.y - transform.position.y, Targetposition.z - transform.position.z));

            if (math.distance(Targetposition, transform.position) < 0.3)
            {
                
                Transform canvasTransform = gameObjectList.transform.Find("CanvasA");


                HighLightStringStarttoMove = false;
                
                gameObjectList.GetComponent<TaskToDoListTextMono>(). AddInformation(0, StringInformation);                                         //0是待修改的量
                Destroy(gameObject);
            }
        }
    }
    void OnMouseDown()
    {
        HighLightStringStarttoMove=true;


    }
}
