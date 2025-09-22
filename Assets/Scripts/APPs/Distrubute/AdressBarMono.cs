using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddressBarMono : MonoBehaviour
{
    public Camera mainCamera; // 2D相机引用
    private GameObject TodolistObject;
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
            if (TodolistObject.transform.position.x== -7.1f)
            {
                InputMode = true;
                WaittingTime = false;
            }
        }
        if (InputMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("InputMode");
                Vector3 mouseScreenPos = Input.mousePosition;
                Debug.Log("mouseScreenPos.x"+ mouseScreenPos.x);
                if (mouseScreenPos.x > 1050f)
                {
                    todolist.OnCloseButtonClick();

                    Debug.Log("mouseScreenPos.x > -4f");
                    InputMode = false;
                }
                //else if (mouseScreenPos.y > 4.2f || mouseScreenPos.y < -4.2f)
                //{
                //    todolist.OnCloseButtonClick();
                //    Debug.Log("mouseScreenPos.y > 4.2f || mouseScreenPos.y < -4.2f");
                //    InputMode = false;
                //}
                else
                {
                    Debug.Log("else");
                    Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
                    RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

                    if (hit.collider != null)
                    {
                        Debug.Log("2");
                        GameObject clickedObject = hit.collider.gameObject;
                        if(clickedObject.tag== "InformationInToDoList")
                        {
                            string Information = hit.collider.gameObject.transform.GetComponent<TextMeshProUGUI>().text;
                            todolist.OnCloseButtonClick();
                            InputDistributeBar(Information);
                            InputMode = false;
                        }
                        else
                        {
                            Debug.Log("点击的2D物体不包含 InformationText 组件: " + clickedObject.name);
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
        Transform AddressTexttransform = transform.Find("AddressText");
        AddressTexttransform.gameObject.GetComponent<TextMeshProUGUI>().text= PhoneNumber;
        DistributeBar.GetComponent<DistributeBarMono>().PhoneNumber = PhoneNumber;
    }
    private void OnMouseDown()
    {

        todolist.HandleClick();
        WaittingTime = true;

    }
    public void DeleteInformation()
    {
        Transform AddressTexttransform = transform.Find("AddressText");
        AddressTexttransform.gameObject.GetComponent<TextMeshProUGUI>().text = "";
    } 
}
