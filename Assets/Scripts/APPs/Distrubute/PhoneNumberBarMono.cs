using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneNumberBarMono : MonoBehaviour
{
    public Camera mainCamera; // 2D�������
    private GameObject TodolistObject;
    public GameObject PhoneCloseButton;
    private ToDoList todolist;
    public GameObject DistributeBar;
    public bool InputMode;
    public bool WaittingTime;
    void Start()
    {
        mainCamera = Camera.main;
        TodolistObject = GameObject.FindGameObjectWithTag("ToDoList");
        todolist = TodolistObject.GetComponent<ToDoList>();
        InputMode = false;
        WaittingTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (WaittingTime)
        {
            if (TodolistObject.transform.position.x == -7.1f)
            {
                InputMode = true;
                WaittingTime = false;
            }
        }
        if (InputMode) 
        {
            if (Input.GetMouseButtonDown(0))
            {

                Vector3 mouseScreenPos = Input.mousePosition;
                if (mouseScreenPos.x > 1050f)
                {
                    todolist.OnCloseButtonClick();
                    InputMode = false;
                }
                //else if (mouseScreenPos.y > 4.2f || mouseScreenPos.y < -4.2f)
                //{
                //    todolist.OnCloseButtonClick();

                //    InputMode = false;
                //}
                else
                {
                    Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
                    RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

                    if (hit.collider != null)
                    {
                        GameObject clickedObject = hit.collider.gameObject;
                        if (clickedObject.tag == "InformationInToDoList")
                        {
                            string Information = hit.collider.gameObject.transform.GetComponent<TextMeshProUGUI>().text;
                            InputDistributeBar(Information);
                            todolist.OnCloseButtonClick();
                            InputMode = false;
                        }
                        else
                        {
                            Debug.Log("�����2D���岻���� InformationText ���: " + clickedObject.name);
                        }
                    }
                }

            }
            if (!todolist.moved)
            {
                InputMode = false;
            }
        }
    }
    private void InputDistributeBar(string PhoneNumber)
    {
        Transform PhoneNumberTexttransform = transform.Find("PhoneNumberText");
        PhoneNumberTexttransform.gameObject.GetComponent<TextMeshProUGUI>().text = PhoneNumber;
        DistributeBar.GetComponent<DistributeBarMono>().PhoneNumber = PhoneNumber;
    }
    private void OnMouseDown()
    {
        
        todolist.HandleClick();
        WaittingTime = true;

    }
    public void DeleteInformation()
    {
        Transform PhoneNumberTexttransform = transform.Find("PhoneNumberText");
        PhoneNumberTexttransform.gameObject.GetComponent<TextMeshProUGUI>().text = "";
        DistributeBar.GetComponent<DistributeBarMono>().PhoneNumber = "";
    }
}
