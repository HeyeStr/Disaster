using UnityEngine;
using Manager;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Common;

public class ToDoList : MonoBehaviour
{
    public  bool moved = false;
    private Vector3 leftPos;   // 初始位置
    private Vector3 centerPos; // 屏幕中心对应的 World 位置
    public bool StarttoMove;
    private Vector3 targetPos; // 目标位置
    
    private PlayerInputManager inputManager; // 新增输入管理器引用
    
    [SerializeField]
    private GameObject closeButton; // 关闭按钮引用
    [SerializeField]
    private GameObject previousButton; // 上一页按钮
    [SerializeField]
    private GameObject nextButton;     // 下一页按钮

    public  int currentPage = 0;   // 当前页码
    public  int totalPages = 0;    // 总页数

    public GameObject TextPrefab;
    public GameObject TextCanvasPrefab;

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
            // 初始时隐藏关闭按钮
            closeButton.SetActive(false);
        }

        // 设置翻页按钮的点击事件
        if (previousButton != null)
        {
            previousButton.SetActive(false);
        }

        if (nextButton != null)
        {
            nextButton.SetActive(false);
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
                if (targetPos.x > -8f) {
                    transform.GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    transform.GetComponent<BoxCollider2D>().enabled = true;
                }
                transform.position = targetPos;
                StarttoMove = false;
                // 根据移动状态显示或隐藏关闭按钮
                if (closeButton != null)
                {
                    closeButton.SetActive(moved);
                }
            }
        }
    }

    // 将原来的 OnMouseDown 逻辑移到这个方法中
    public  void HandleClick()
    {
        if (!moved)
        {
            targetPos = new Vector3(-7.1f, 0f, 0.53f);
            moved = true;
            // 展开时显示所有按钮
            if (closeButton != null)
            {
                closeButton.SetActive(true);
            }
            if (previousButton != null)
            {
                previousButton.SetActive(true);
            }
            if (nextButton != null)
            {
                nextButton.SetActive(true);
            }
            UpdatePageButtonsState();
        }
        StarttoMove = true;
    }

    public  void OnCloseButtonClick()
    {
        if (moved)
        {

            
            targetPos = leftPos;
            moved = false;
            StarttoMove = true;
            // 关闭时隐藏所有按钮
            closeButton.SetActive(false);
            previousButton.SetActive(false);
            nextButton.SetActive(false);
        }
    }

    public  void OnPreviousButtonClick()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePageContent();
        }
    }

    public  void OnNextButtonClick()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            UpdatePageContent();
        }
    }

    private void UpdatePageButtonsState()
    {
        // 更新按钮状态
        if (previousButton != null)
        {
            previousButton.SetActive(currentPage > 0 && moved);
        }
        if (nextButton != null)
        {
            nextButton.SetActive(currentPage < totalPages - 1 && moved);
        }
    }
  

    public  void UpdatePageContent()
    {
        List<Mission> missions = gameObject.GetComponent<TaskToDoListTextMono>().Missions;

        
        GameObject[] TextCanvas =GameObject.FindGameObjectsWithTag("TextCanvas");

        for(int i = 0; i < TextCanvas.Length; i++)
        {
            Destroy(TextCanvas[i]);
        }
        GameObject NewTextCanvas =GameObject.Instantiate(TextCanvasPrefab);
        NewTextCanvas.tag = "TextCanvas";
        Debug.Log("Instantiate");
        NewTextCanvas.transform.position = new Vector3(transform.position.x, 0, 0);
        NewTextCanvas.transform.parent= transform;
        Transform TaskHeadTexttransform= NewTextCanvas.transform.Find("TaskHeadText");
        
        GameObject Monitor = GameObject.FindGameObjectWithTag("Monitor");
        MonitorMonoBehaviour monitorMonoBehaviour = Monitor.GetComponent<MonitorMonoBehaviour>();
        for(int i =0;i< missions[currentPage].Informations.Count; i++)
        {
            GameObject NewText_Information = GameObject.Instantiate(TextPrefab);
            NewText_Information.transform.parent = NewTextCanvas.transform;
            NewText_Information.transform.position = new Vector3(transform.position.x, 2.5f - i * 0.5f, 0);
            NewText_Information.GetComponent<TextMeshProUGUI>().text = missions[currentPage].Informations[i].information;
            SendMessageButton sendMessageButton= NewText_Information.AddComponent<SendMessageButton>();
            sendMessageButton.canSelect = true;
            sendMessageButton.textMeshPro = NewText_Information.GetComponent<TextMeshProUGUI>();
            sendMessageButton.interactObj = missions[currentPage].Informations[i].IsTelephone ? monitorMonoBehaviour.PhoneObject : monitorMonoBehaviour.Dialogue;
        }

        UpdatePageButtonsState();

        Debug.Log($"切换到第 {currentPage + 1} 页");
    }


    public void DisplayTaskPage(int missionIndex)
    {
        int missionpage= gameObject.GetComponent<TaskToDoListTextMono>().GetTaskPage(missionIndex);
        currentPage = missionpage;
        UpdatePageContent();
        
    }
   
}