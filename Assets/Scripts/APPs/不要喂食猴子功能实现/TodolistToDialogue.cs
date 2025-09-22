using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using Dialogue;

public class TodolistToDialogue : MonoBehaviour
{
    private GameObject dialogueObj;
    public bool startToMove;
    public float moveSpeed = 1f;
    private Vector3 targetPosition;
    private DialogueManager dialogueManager;

    void Start()
    {
        startToMove = false;
        // 获取对话框对象和管理器
        dialogueObj = GameObject.FindGameObjectWithTag("Dialogue");
        if (dialogueObj != null)
        {
            dialogueManager = dialogueObj.GetComponent<DialogueManager>();
        }
    }

    void Update()
    {
        if (startToMove)
        {
            // 获取对话框的位置作为目标位置
            if (dialogueObj != null)
            {
                targetPosition = dialogueObj.transform.position;
                
                transform.position = math.lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                if (math.distance(targetPosition, transform.position) < 0.3f)
                {
                    startToMove = false;
                    
                    // 获取文本内容并传递给对话系统
                    string textContent = GetComponent<TextMeshProUGUI>()?.text ?? "";
                    if (dialogueManager != null)
                    {
                        // 这里需要根据对话系统接口进行适当修改
                        // dialogueManager.StartDialogue(textContent);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnMouseDown()
    {
        startToMove = true;
    }
}
