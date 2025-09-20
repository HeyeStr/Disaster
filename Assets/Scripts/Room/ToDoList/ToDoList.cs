using UnityEngine;
using Manager;
using UnityEngine.UI;

public class ToDoList : MonoBehaviour
{
    private bool moved = false;
    private Vector3 leftPos;   // 初始位置
    private Vector3 centerPos; // 屏幕中心对应的 World 位置
    private bool StarttoMove;
    private Vector3 targetPos; // 目标位置

    private PlayerInputManager inputManager; // 新增输入管理器引用
    
    [SerializeField]
    private Button closeButton; // 关闭按钮引用
    [SerializeField]
    private Button previousButton; // 上一页按钮
    [SerializeField]
    private Button nextButton;     // 下一页按钮

    private int currentPage = 0;   // 当前页码
    private int totalPages = 3;    // 总页数

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

        // 设置关闭按钮的点击事件
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnCloseButtonClick);
            // 初始时隐藏关闭按钮
            closeButton.gameObject.SetActive(false);
        }

        // 设置翻页按钮的点击事件
        if (previousButton != null)
        {
            previousButton.onClick.AddListener(OnPreviousButtonClick);
            previousButton.gameObject.SetActive(false);
        }

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextButtonClick);
            nextButton.gameObject.SetActive(false);
        }
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
                
                // 根据移动状态显示或隐藏关闭按钮
                if (closeButton != null)
                {
                    closeButton.gameObject.SetActive(moved);
                }
            }
        }
    }

    // 将原来的 OnMouseDown 逻辑移到这个方法中
    private void HandleClick()
    {
        if (!moved)
        {
            targetPos = new Vector3(-7.1f, 0f, 0.53f);
            moved = true;
            // 展开时显示所有按钮
            if (closeButton != null)
            {
                closeButton.gameObject.SetActive(true);
            }
            if (previousButton != null)
            {
                previousButton.gameObject.SetActive(true);
            }
            if (nextButton != null)
            {
                nextButton.gameObject.SetActive(true);
            }
            UpdatePageButtonsState();
        }
        StarttoMove = true;
    }

    private void OnCloseButtonClick()
    {
        if (moved)
        {
            targetPos = leftPos;
            moved = false;
            StarttoMove = true;
            // 关闭时隐藏所有按钮
            closeButton.gameObject.SetActive(false);
            previousButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }

    private void OnPreviousButtonClick()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePageContent();
            UpdatePageButtonsState();
        }
    }

    private void OnNextButtonClick()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            UpdatePageContent();
            UpdatePageButtonsState();
        }
    }

    private void UpdatePageButtonsState()
    {
        // 更新按钮状态
        if (previousButton != null)
        {
            previousButton.interactable = currentPage > 0;
        }
        if (nextButton != null)
        {
            nextButton.interactable = currentPage < totalPages - 1;
        }
    }

    private void UpdatePageContent()
    {
        // 这里添加更新页面内容的逻辑
        // 例如：显示不同的任务列表、更新UI等
        Debug.Log($"切换到第 {currentPage + 1} 页");
    }
}
