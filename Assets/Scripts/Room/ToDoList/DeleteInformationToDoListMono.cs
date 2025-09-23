using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeleteInformationToDoListMono : MonoBehaviour
{
    private GameObject TodoList;
    public  GameObject TextInTodoList;  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        TodoList = GameObject.FindGameObjectWithTag("ToDoList");
        string textinformation= TextInTodoList.GetComponent<TextMeshProUGUI>().text;
        TodoList.GetComponent<TaskToDoListTextMono>().DeleteInformation(textinformation, TodoList.GetComponent<ToDoList>().currentPage);

    }

}
