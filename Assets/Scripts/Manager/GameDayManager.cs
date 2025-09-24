using UnityEngine;
using System.Collections;

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

            if (IsBlogSceneLoaded())
            {
                LoadCurrentDayBlogScene();
                Debug.Log($"已切换到第 {currentDay} 天，并自动加载对应博客场景");
            }
            else
            {
                Debug.Log($"已切换到第 {currentDay} 天，点击博客应用图标查看对应内容");
            }
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
            if (IsBlogSceneLoaded())
            {
                LoadCurrentDayBlogScene();
                Debug.Log($"已切换到第 {currentDay} 天，并自动加载对应博客场景");
            }
            else
            {
                Debug.Log($"已切换到第 {currentDay} 天，点击博客应用图标查看对应内容");
            }
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
    

    private bool IsBlogSceneLoaded()
    {
        for (int i = 0; i < blogSceneNames.Length; i++)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(blogSceneNames[i]);
            if (scene.IsValid() && scene.isLoaded)
            {
                return true;
            }
        }
        return false;
    }
    

    private IEnumerator LoadBlogSceneAfterUnload(SceneControlMono sceneControl, int day)
    {
        yield return null;
        sceneControl.LoadBlogSceneForDay(day);
    }

    public void ForceNextDayWithScene()
    {
        if (currentDay < maxDays)
        {
            SetCurrentDay(currentDay + 1);
            LoadCurrentDayBlogScene();
        }
        else
        {
            Debug.Log("已达到最大天数");
        }
    }

    public void LoadCurrentDayBlogScene()
    {
        GameObject monitor = GameObject.FindGameObjectWithTag("Monitor");
        if (monitor != null)
        {
            SceneControlMono sceneControl = monitor.GetComponent<SceneControlMono>();
            if (sceneControl != null)
            {    
                sceneControl.UnloadBlogScene();
                StartCoroutine(LoadBlogSceneAfterUnload(sceneControl, currentDay));
                Debug.Log($"加载第 {currentDay} 天的博客场景");
            }
        }
        else
        {
            Debug.LogWarning("Monitor对象未找到，无法加载博客场景");
        }
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
    
    [ContextMenu("强制进入下一天并加载场景")]
    public void DebugForceNextDay()
    {
        ForceNextDayWithScene();
    }
}