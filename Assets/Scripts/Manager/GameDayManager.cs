using UnityEngine;
//负责场景切换
public class GameDayManager : MonoBehaviour
{
    public static GameDayManager Instance;
    
    [Header("游戏天数设置")]
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int maxDays = 7;
    [Header("博客场景名称")]
    [SerializeField] private string[] blogSceneNames = {
        "BlogScene_Day1",
        "BlogScene_Day2", 
        "BlogScene_Day3",
        "BlogScene_Day4",
        "BlogScene_Day5",
        "BlogScene_Day6",
        "BlogScene_Day7"
    };
    
    [Header("天数事件")]
    public System.Action<int> OnDayChanged;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        SetCurrentDay(1);
    }
    
    public int GetCurrentDay()
    {
        return currentDay;
    }

    public void SetCurrentDay(int day)
    {
        if (day < 1 || day > maxDays)
        {
            Debug.LogWarning($"天数 {day} 超出范围 (1-{maxDays})");
            return;
        } 
        int previousDay = currentDay;
        currentDay = day;
        Debug.Log($"天数从第 {previousDay} 天切换到第 {currentDay} 天");
        OnDayChanged?.Invoke(currentDay);
    }
 
    public void NextDay()
    {
        if (currentDay < maxDays)
        {
            SetCurrentDay(currentDay + 1);
        }
        else
        {
            Debug.Log("已达到最大天数");
        }
    }
 
    public void PreviousDay()
    {
        if (currentDay > 1)
        {
            SetCurrentDay(currentDay - 1);
        }
        else
        {
            Debug.Log("已经是第一天");
        }
    }
    
    public string GetCurrentBlogSceneName()
    {
        if (currentDay >= 1 && currentDay <= blogSceneNames.Length)
        {
            return blogSceneNames[currentDay - 1];
        }
        return "BlogScene"; 
    }

    public string GetBlogSceneName(int day)
    {
        if (day >= 1 && day <= blogSceneNames.Length)
        {
            return blogSceneNames[day - 1];
        }
        return "BlogScene"; 
    }
    
    
    [ContextMenu("进入下一天")]
    public void DebugNextDay()
    {
        NextDay();
    }
    
    [ContextMenu("回到上一天")]
    public void DebugPreviousDay()
    {
        PreviousDay();
    }
}