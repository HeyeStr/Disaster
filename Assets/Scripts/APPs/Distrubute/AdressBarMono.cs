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
    void Start()
    {
        mainCamera = Camera.main;
        TodolistObject = GameObject.FindGameObjectWithTag("ToDoList");
        todolist = TodolistObject.GetComponent<ToDoList>();
        InputMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputMode)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Vector3 mouseScreenPos = Input.mousePosition;
                if (mouseScreenPos.x > -4f)
                {
                    InputMode = false;
                }
                else if (mouseScreenPos.y > 4.2f || mouseScreenPos.y < -4.2f)
                {
                    InputMode = false;
                }
                else
                {
                    Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
                    RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

                    if (hit.collider != null)
                    {
                        GameObject clickedObject = hit.collider.gameObject;
                        if(clickedObject.tag== "InformationInToDoList")
                        {
                            string Information = hit.collider.gameObject.transform.GetComponent<TextMeshProUGUI>().text;
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


    private void InputDistributeBar(string Address)
    {
        DistributeBar.GetComponent<DistributeBarMono>().PhoneNumber = Address;
    }
    private void OnMouseDown()
    {

        todolist.HandleClick();
        InputMode = true;

    }
}
