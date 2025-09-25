using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadmeIconMono : MonoBehaviour
{
    public GameObject MonitorObject;
    void Start()
    {
        MonitorObject = GameObject.FindGameObjectWithTag("Monitor");

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        MonitorObject.GetComponent<SceneControlMono>().LoadreadmeScene();
        MonitorObject.GetComponent<SceneControlMono>().UnloadDeskScene();
    }
}
