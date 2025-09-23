using UnityEngine;
using UnityEngine.UI;

public class BlogDaySwitcher : MonoBehaviour
{
    [Header("天数切换按钮")]
    public Button[] dayButtons; // 第1-7天的按钮
    
    [Header("显示信息")]
    public Text currentDayText;
    
    [Header("场景控制器")]
    private SceneControlMono sceneController;
    
    void Start()
    {
        GameObject monitor = GameObject.FindGameObjectWithTag("Monitor");
        if (monitor != null)
        {
            sceneController = monitor.GetComponent<SceneControlMono>();
        }

        SetupDayButtons();
        UpdateDisplay();
        
        if (GameDayManager.Instance != null)
        {
            GameDayManager.Instance.OnDayChanged += OnDayChanged;
        }
    }
    
    void OnDestroy()
    {
        if (GameDayManager.Instance != null)
        {
            GameDayManager.Instance.OnDayChanged -= OnDayChanged;
        }
    }

    void SetupDayButtons()
    {
        if (dayButtons != null)
        {
            for (int i = 0; i < dayButtons.Length; i++)
            {
                int day = i + 1; 
                if (dayButtons[i] != null)
                {
                    dayButtons[i].onClick.AddListener(() => SwitchToDay(day));
                }
            }
        }
    }

    public void SwitchToDay(int day)
    {
        if (GameDayManager.Instance != null)
        {
            GameDayManager.Instance.SetCurrentDay(day);
        }
        
        if (sceneController != null)
        {
            sceneController.UnloadBlogScene();
            sceneController.LoadBlogSceneForDay(day);
        }
        
        UpdateDisplay();
    }

    private void OnDayChanged(int newDay)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (currentDayText != null && GameDayManager.Instance != null)
        {
            int currentDay = GameDayManager.Instance.GetCurrentDay();
            currentDayText.text = $"第 {currentDay} 天";
        }
    }
    

    [ContextMenu("切换到第1天")]
    public void DebugSwitchToDay1() { SwitchToDay(1); }
    
    [ContextMenu("切换到第2天")]
    public void DebugSwitchToDay2() { SwitchToDay(2); }
    
    [ContextMenu("切换到第3天")]
    public void DebugSwitchToDay3() { SwitchToDay(3); }
    
    [ContextMenu("切换到第4天")]
    public void DebugSwitchToDay4() { SwitchToDay(4); }
    
    [ContextMenu("切换到第5天")]
    public void DebugSwitchToDay5() { SwitchToDay(5); }
    
    [ContextMenu("切换到第6天")]
    public void DebugSwitchToDay6() { SwitchToDay(6); }
    
    [ContextMenu("切换到第7天")]
    public void DebugSwitchToDay7() { SwitchToDay(7); }
}
