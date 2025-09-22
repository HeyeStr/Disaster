using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneNumberBarMono : MonoBehaviour
{
    public GameObject DistributeBar;
    public bool InputMode;
    void Start()
    {
        InputMode = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if (InputMode) 
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
        
        }
    }
    private void InputDistributeBar(string PhoneNumber)
    {
        DistributeBar.GetComponent<DistributeBarMono>().PhoneNumber = PhoneNumber;
    }
    private void OnMouseDown()
    {
        InputMode = true;
    }
}
