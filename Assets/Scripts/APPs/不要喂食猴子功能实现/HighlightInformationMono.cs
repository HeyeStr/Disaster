using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightInformationMono : MonoBehaviour, IPointerClickHandler
{
    GameObject gameObjectList;
    public bool HighLightStringStarttoMove;
    public float MoveSpeed;
    
    void Start()
    {
        HighLightStringStarttoMove = false;
        
        // 确保有GraphicRaycaster组件
        if (GetComponent<GraphicRaycaster>() == null)
        {
            gameObject.AddComponent<GraphicRaycaster>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (HighLightStringStarttoMove)
        {
            // 获取目标位置的世界坐标
            gameObjectList = GameObject.FindGameObjectWithTag("ToDoList");
            Vector3 Targetposition = gameObjectList.GetComponent<TaskToDoListTextMono>().GetNewInformationPosition(0);                  //0待定

            // 移动到目标位置
            transform.position= math.lerp(transform.position, Targetposition, 0.1f);
            //transform.position +=  MoveSpeed * Time.deltaTime *  (Vector3)math.normalize(new float3(Targetposition.x - transform.position.x, Targetposition.y - transform.position.y, Targetposition.z - transform.position.z));

            if (math.distance(Targetposition, transform.position) < 0.3)
            {
                
                Transform canvasTransform = gameObjectList.transform.Find("CanvasA");


                HighLightStringStarttoMove = false;
                
                gameObjectList.GetComponent<TaskToDoListTextMono>(). AddInformation(0,transform.gameObject.GetComponent <TextMeshProUGUI>().text);                                         //0是待修改的量
                Destroy(gameObject);
            }
        }
    }
    void OnMouseDown()
    {
        HighLightStringStarttoMove=true;
        //string HighLightString = transform.gameObject.GetComponent<TextMeshProUGUI>().text;
        //gameObjectList = GameObject.FindGameObjectWithTag("ToDoList");
        //// 查找Canvas子物体 
        //Transform canvasTransform = gameObjectList.transform.Find("CanvasA");
        //// 在Canvas中查找InformationButton
        //Transform InformationRecord = canvasTransform.Find("InformationRecord");

        //GameObject informationButton = InformationRecord.gameObject;
        //informationButton.GetComponent<TextMeshProUGUI>().text = "";
        



    }
    
    // 使用UI事件系统替代OnMouseDown
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("HighlightInformationMono 被点击了！");
        HighLightStringStarttoMove = true;
    }
}
