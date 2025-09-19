using System.Collections;

using UnityEngine;

public class BlogDeleteButton : MonoBehaviour
{
    private GameObject MonitorGameObject;

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
        MonitorGameObject.GetComponent<SceneControlMono>().UnloadBlogScene();
        MonitorGameObject.GetComponent<SceneControlMono>().loadDeskScene();
    }
}
