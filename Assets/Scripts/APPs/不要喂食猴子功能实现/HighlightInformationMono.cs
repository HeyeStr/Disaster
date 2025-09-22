using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightInformationMono : MonoBehaviour
{
    // 添加静态HashSet记录已点击的文字
    private static HashSet<string> clickedTexts = new HashSet<string>();
    
    GameObject gameObjectList;
    public bool HighLightStringStarttoMove;
    public float MoveSpeed;
    
    public string StringInformation;                     //任务相关信息
    public string missionName;                          //任务相关信息
    public int missionIndex;                            //任务相关信息

    // 修改为string类型
    public string textId;
    
    void Start()
    {
        HighLightStringStarttoMove = false;
        if (clickedTexts.Contains(textId))
        {
            // 禁用
            GetComponent<Collider2D>().enabled = false;
            // 直接销毁
            // Destroy(gameObject);
        }
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
            if (!tasktoDoListTextMono.HasTask(missionIndex))
            {
                tasktoDoListTextMono.AddTask(missionName, missionIndex);
            }
            
            Vector3 Targetposition = tasktoDoListTextMono.GetNewInformationPosition(0);                  //0待定
            
            transform.position= math.lerp(transform.position, Targetposition, MoveSpeed);
            //transform.position +=  MoveSpeed * Time.deltaTime *  (Vector3)math.normalize(new float3(Targetposition.x - transform.position.x, Targetposition.y - transform.position.y, Targetposition.z - transform.position.z));

            if (math.distance(Targetposition, transform.position) < 0.3)
            {
                HighLightStringStarttoMove = false;
                
                gameObjectList.GetComponent<TaskToDoListTextMono>(). AddInformation(0, StringInformation);                           //0是待修改的量
                Destroy(gameObject);
            }
        }
    }
    
    void OnMouseDown()
    {
        // 检查点击
        if (clickedTexts.Contains(textId))
        {
            return; 
        }
        clickedTexts.Add(textId);

        HighLightStringStarttoMove = true;
        Transform Texttransform= transform.Find("Text1");
        Texttransform.gameObject.GetComponent<TextMeshProUGUI>().text = StringInformation;
    }
    
}