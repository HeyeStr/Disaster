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
        //if (Input.GetMouseButtonDown(0)) // ���������
        //{
        //    // �����λ��ת��Ϊ��������
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    // �������߼����ײ
        //    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //    if (hit.collider != null)
        //    {
        //        Debug.Log("�����: " + hit.collider.gameObject.name);

        //        // ��������������ض�����Ĵ����߼�
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


