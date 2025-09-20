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
    public GameObject TargetPositionText;
    public float MoveSpeed;
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
            Vector3 Targetposition = TargetPositionText.transform.position;
            
            // 移动到目标位置
            transform.position += MoveSpeed * Time.deltaTime * (Vector3)math.normalize(new float3(Targetposition.x - transform.position.x, Targetposition.y - transform.position.y, Targetposition.z - transform.position.z));

            if (math.distance(Targetposition, transform.position) < 0.3)
            {
                transform.position = Targetposition;
                Transform canvasTransform = gameObjectList.transform.Find("CanvasA");

                transform.parent = canvasTransform.transform;


                HighLightStringStarttoMove = false;
            }
        }
    }
    void OnMouseDown()
    {
        string HighLightString = transform.gameObject.GetComponent<TextMeshProUGUI>().text;
        gameObjectList = GameObject.FindGameObjectWithTag("ToDoList");
        // 查找Canvas子物体 
        Transform canvasTransform = gameObjectList.transform.Find("CanvasA");
        // 在Canvas中查找InformationButton
        Transform InformationRecord = canvasTransform.Find("InformationRecord");

        GameObject informationButton = InformationRecord.gameObject;
        informationButton.GetComponent<TextMeshProUGUI>().text = "";
        TargetPositionText = informationButton;
        HighLightStringStarttoMove =true;



    }
}
