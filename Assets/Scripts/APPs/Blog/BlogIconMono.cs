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
                // 加载当前天数的博客场景，而不是硬编码的第1天
                if (GameDayManager.Instance != null)
                {
                    int currentDay = GameDayManager.Instance.GetCurrentDay();
                    sceneControl.LoadBlogSceneForDay(currentDay);
                    Debug.Log($"点击博客应用图标，加载第 {currentDay} 天的博客场景");
                }
                else
                {
                    // 如果没有GameDayManager，使用默认方法
                    sceneControl.LoadBlogScene();
                }
                sceneControl.UnloadDeskScene();
            }
        }
    }
}
