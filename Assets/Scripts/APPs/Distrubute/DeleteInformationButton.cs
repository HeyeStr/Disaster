using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteInformationButton : MonoBehaviour
{
    public GameObject AddressBar;
    public GameObject PhoneNumberBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("delete1");
        AddressBar.GetComponent<AddressBarMono>().DeleteInformation();
        PhoneNumberBar.GetComponent<PhoneNumberBarMono>().DeleteInformation();
    }
}
