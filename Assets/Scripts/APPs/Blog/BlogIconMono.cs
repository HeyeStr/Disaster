using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlogIconMono : MonoBehaviour
{
    public GameObject MonitorGameObject;
    // Start is called before the first frame update
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
        if (MonitorGameObject != null)
        {
            SceneControlMono sceneControl = MonitorGameObject.GetComponent<SceneControlMono>();
            if (sceneControl != null)
            {

                sceneControl.LoadBlogScene();
                sceneControl.UnloadDeskScene();
            }
        }
    }
}
