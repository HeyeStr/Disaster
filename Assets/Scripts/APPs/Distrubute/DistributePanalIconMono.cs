using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributePanalIconMono : MonoBehaviour
{
    public GameObject MonitorGameObject;

    void Start()
    {

        MonitorGameObject = GameObject.FindGameObjectWithTag("Monitor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if(MonitorGameObject != null)
        {
            SceneControlMono sceneControl = MonitorGameObject.GetComponent<SceneControlMono>();
            if (sceneControl != null)
            {
                sceneControl.LoadDistributeScene();
                sceneControl.UnloadDeskScene();
            }
        }
    }
}
