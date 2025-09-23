using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class DayData
{
    [Header("Day信息")]
    public int dayNumber;
    public string dayName;
    
    [Header("场景配置")]
    public string[] scenesToLoad;      // 需要加载的场景
    public string[] scenesToUnload;    // 需要卸载的场景
    
    [Header("组件替换配置")]
    public ComponentReplacement[] componentReplacements;
    
    [Header("资源配置")]
    public DayResourceConfig resourceConfig;
}

[System.Serializable]
public class ComponentReplacement
{
    public string targetObjectName;     // 目标GameObject名称
    public Component oldComponent;      // 要替换的组件
    public Component newComponent;      // 新的组件
    public bool replaceComponent;       // 是否替换组件
    public bool replaceGameObject;      // 是否替换整个GameObject
    public GameObject replacementPrefab; // 替换用的预制体
}

[System.Serializable]
public class DayResourceConfig
{
    public int initialLivingResource;
    public int initialFoodResource;
    public int initialMedicalResource;
}

public class DayManager : MonoBehaviour
{
    [Header("Day配置")]
    public DayData[] allDays;
    
    [Header("当前状态")]
    public int currentDay = 1;
    public bool isTransitioning = false;
    
    [Header("事件回调")]
    public System.Action<int> OnDayChanged;
    public System.Action<int> OnDayTransitionStart;
    public System.Action<int> OnDayTransitionComplete;
    
    [Header("引用")]
    public ResourcesManager resourcesManager;
    public SceneControlMono sceneController;
    
    private static DayManager _instance;
    public static DayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DayManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("DayManager");
                    _instance = go.AddComponent<DayManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        InitializeDay(currentDay);
    }
    
    /// <summary>
    /// 初始化指定的Day
    /// </summary>
    public void InitializeDay(int dayNumber)
    {
        DayData dayData = GetDayData(dayNumber);
        if (dayData != null)
        {
            currentDay = dayNumber;
            StartCoroutine(LoadDayCoroutine(dayData));
        }
        else
        {
            Debug.LogError($"找不到Day {dayNumber}的配置数据！");
        }
    }
    
    /// <summary>
    /// 进入下一天
    /// </summary>
    public void GoToNextDay()
    {
        if (isTransitioning) return;
        
        int nextDay = currentDay + 1;
        if (nextDay <= allDays.Length)
        {
            TransitionToDay(nextDay);
        }
        else
        {
            Debug.Log("已经是最后一天了！");
        }
    }
    
    /// <summary>
    /// 转换到指定的Day
    /// </summary>
    public void TransitionToDay(int targetDay)
    {
        if (isTransitioning) return;
        
        StartCoroutine(TransitionToDayCoroutine(targetDay));
    }
    
    /// <summary>
    /// Day1结算完成后调用，进入Day2
    /// </summary>
    public void OnDay1SettlementComplete()
    {
        Debug.Log("Day1结算完成，准备进入Day2");
        GoToNextDay();
    }
    
    private IEnumerator TransitionToDayCoroutine(int targetDay)
    {
        isTransitioning = true;
        OnDayTransitionStart?.Invoke(targetDay);
        
        DayData currentDayData = GetDayData(currentDay);
        DayData targetDayData = GetDayData(targetDay);
        
        if (targetDayData == null)
        {
            Debug.LogError($"找不到Day {targetDay}的配置数据！");
            isTransitioning = false;
            yield break;
        }
        
        // 1. 卸载当前Day的场景
        if (currentDayData != null)
        {
            yield return StartCoroutine(UnloadScenesCoroutine(currentDayData.scenesToUnload));
        }
        
        // 2. 替换组件
        yield return StartCoroutine(ReplaceComponentsCoroutine(targetDayData.componentReplacements));
        
        // 3. 加载新Day的场景
        yield return StartCoroutine(LoadScenesCoroutine(targetDayData.scenesToLoad));
        
        // 4. 更新资源
        UpdateResources(targetDayData.resourceConfig);
        
        // 5. 更新当前Day
        currentDay = targetDay;
        
        isTransitioning = false;
        OnDayChanged?.Invoke(currentDay);
        OnDayTransitionComplete?.Invoke(currentDay);
        
        Debug.Log($"成功转换到Day {currentDay}");
    }
    
    private IEnumerator LoadDayCoroutine(DayData dayData)
    {
        // 加载Day场景
        yield return StartCoroutine(LoadScenesCoroutine(dayData.scenesToLoad));
        
        // 应用组件替换
        yield return StartCoroutine(ReplaceComponentsCoroutine(dayData.componentReplacements));
        
        // 更新资源
        UpdateResources(dayData.resourceConfig);
        
        OnDayChanged?.Invoke(currentDay);
        Debug.Log($"Day {currentDay} 初始化完成");
    }
    
    private IEnumerator LoadScenesCoroutine(string[] scenes)
    {
        if (scenes == null || scenes.Length == 0) yield break;
        
        foreach (string sceneName in scenes)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                Debug.Log($"加载场景: {sceneName}");
                var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                yield return operation;
                
                if (operation.isDone)
                {
                    Debug.Log($"场景 {sceneName} 加载完成");
                }
            }
        }
    }
    
    private IEnumerator UnloadScenesCoroutine(string[] scenes)
    {
        if (scenes == null || scenes.Length == 0) yield break;
        
        foreach (string sceneName in scenes)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                var scene = SceneManager.GetSceneByName(sceneName);
                if (scene.IsValid() && scene.isLoaded)
                {
                    Debug.Log($"卸载场景: {sceneName}");
                    var operation = SceneManager.UnloadSceneAsync(sceneName);
                    yield return operation;
                    
                    if (operation.isDone)
                    {
                        Debug.Log($"场景 {sceneName} 卸载完成");
                    }
                }
            }
        }
    }
    
    private IEnumerator ReplaceComponentsCoroutine(ComponentReplacement[] replacements)
    {
        if (replacements == null || replacements.Length == 0) yield break;
        
        foreach (var replacement in replacements)
        {
            if (replacement.replaceGameObject && replacement.replacementPrefab != null)
            {
                // 替换整个GameObject
                GameObject target = GameObject.Find(replacement.targetObjectName);
                if (target != null)
                {
                    Vector3 position = target.transform.position;
                    Quaternion rotation = target.transform.rotation;
                    Transform parent = target.transform.parent;
                    
                    Destroy(target);
                    yield return null; // 等待一帧确保销毁完成
                    
                    GameObject newObj = Instantiate(replacement.replacementPrefab, position, rotation, parent);
                    newObj.name = replacement.targetObjectName;
                    
                    Debug.Log($"替换GameObject: {replacement.targetObjectName}");
                }
            }
            else if (replacement.replaceComponent && replacement.oldComponent != null && replacement.newComponent != null)
            {
                // 替换组件
                GameObject target = GameObject.Find(replacement.targetObjectName);
                if (target != null)
                {
                    Component oldComp = target.GetComponent(replacement.oldComponent.GetType());
                    if (oldComp != null)
                    {
                        DestroyImmediate(oldComp);
                        
                        // 添加新组件
                        Component newComp = target.AddComponent(replacement.newComponent.GetType());
                        
                        // 这里可以添加复制属性的逻辑
                        Debug.Log($"替换组件: {replacement.targetObjectName} 的 {replacement.oldComponent.GetType().Name}");
                    }
                }
            }
        }
    }
    
    private void UpdateResources(DayResourceConfig config)
    {
        if (resourcesManager != null && config != null)
        {
            resourcesManager.SetLivingResource(config.initialLivingResource);
            resourcesManager.SetFoodResource(config.initialFoodResource);
            resourcesManager.SetMedicalResource(config.initialMedicalResource);
            
            Debug.Log($"更新资源: 生活={config.initialLivingResource}, 食物={config.initialFoodResource}, 医疗={config.initialMedicalResource}");
        }
    }
    
    private DayData GetDayData(int dayNumber)
    {
        foreach (var dayData in allDays)
        {
            if (dayData.dayNumber == dayNumber)
            {
                return dayData;
            }
        }
        return null;
    }
    
    /// <summary>
    /// 获取当前Day数据
    /// </summary>
    public DayData GetCurrentDayData()
    {
        return GetDayData(currentDay);
    }
    
    /// <summary>
    /// 检查是否可以进入下一天
    /// </summary>
    public bool CanGoToNextDay()
    {
        return !isTransitioning && currentDay < allDays.Length;
    }
}