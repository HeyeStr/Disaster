using UnityEngine;


public class ToDoList : MonoBehaviour
{
        
    private bool moved = false;
    private Vector3 leftPos;   // 初始位置
    private Vector3 centerPos; // 屏幕中心对应的 World 位置

    private bool StarttoMove;
    private Vector3 targetPos; // 新增目标位置变量

    void Start()
    {

        StarttoMove = false;
        leftPos = transform.position;
        // 把屏幕中心转世界坐标
        centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        centerPos.z = leftPos.z; // 保持原深度
    }
    void Update()
     {
        if (StarttoMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                transform.position = targetPos;
                StarttoMove = false;
            }
        }
    }

    void OnMouseDown()   
    {
        
        if (!moved)
            targetPos = new Vector3(-5f, 0f, 0.53f);
        else
            targetPos = leftPos;
        StarttoMove = true;
        moved = !moved;
    }
}
