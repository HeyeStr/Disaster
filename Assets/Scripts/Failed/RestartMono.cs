using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartMono : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("restart");
        GameObject Monitor = GameObject.FindGameObjectWithTag("Monitor");
        SceneControlMono sceneControlMono = Monitor.GetComponent<SceneControlMono>(); 
        sceneControlMono.UnloadDistributeScene();
        GameObject failedsenemanager = GameObject.FindGameObjectWithTag("FailedSceneManager");
        FailedScenemanagerMono failedScenemanagerMono=failedsenemanager.GetComponent<FailedScenemanagerMono>();
        failedScenemanagerMono.ReloadMainScene();

    }
}
