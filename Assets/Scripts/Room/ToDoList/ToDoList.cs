using UnityEngine;
using Manager;

public class ToDoList : MonoBehaviour
{
    private bool moved = false;
    private Vector3 leftPos;   // 初始位置
    private Vector3 centerPos; // 屏幕中心对应的 World 位置
    private bool StarttoMove;
    private Vector3 targetPos; // 目标位置

    private PlayerInputManager inputManager; // 新增输入管理器引用

    void Start()
    {
        // 获取或添加输入管理器
        inputManager = FindObjectOfType<PlayerInputManager>();
        if (inputManager == null)
        {
            var go = new GameObject("InputManager");
            inputManager = go.AddComponent<PlayerInputManager>();
        }

        StarttoMove = false;
        leftPos = transform.position;
        // 把屏幕中心转世界坐标
        centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        centerPos.z = leftPos.z;
    }

    void Update()
    {
        // 检查鼠标点击
        if (inputManager.ClickMouse)
        {
            Vector2 mousePos = inputManager.MousePosition;
            // 检查是否点击到了当前对象
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                HandleClick();
            }
        }

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

    // 将原来的 OnMouseDown 逻辑移到这个方法中
    private void HandleClick()
    {
        if (!moved)
            targetPos = new Vector3(-7.1f, 0f, 0.53f);
        else
            targetPos = leftPos;
        
        StarttoMove = true;
        moved = !moved;
    }
}
