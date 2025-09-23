using Phone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonitorMonoBehaviour : MonoBehaviour
{
    public bool Desk;
    public bool DistributePanal;
    public SceneControlMono sceneControlMono;
    public AcceptMessage PhoneObject;
    public AcceptMessage Dialogue;

    void Start()
    {

        sceneControlMono=GetComponent<SceneControlMono>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) // 检测左键点击
        //{
        //    // 将鼠标位置转换为世界坐标
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    // 发射射线检测碰撞
        //    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //    if (hit.collider != null)
        //    {
        //        Debug.Log("点击了: " + hit.collider.gameObject.name);

        //        // 可以在这里添加特定物体的处理逻辑
        //        if (hit.collider.CompareTag("BlogApp"))
        //        {
                    
        //            sceneControlMono.LoadBlogScene();
        //            sceneControlMono.UnloadDeskScene();
        //             Vector3 vector3 = Vector3.zero;
                    
        //        }
        //    }
        //}
    }
}


