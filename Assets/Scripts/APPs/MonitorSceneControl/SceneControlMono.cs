using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControlMono : MonoBehaviour                                       //������ʾ���ϲ�ͬ�㼶��Ӧ����Scene�Ĵ򿪹ر�
{
    [Header("Control Scene")]
    [SerializeField] private SceneField Monitor;
    [SerializeField] private SceneField BlogScene;
    [SerializeField] private SceneField DistributePanalScene;
    [SerializeField] private SceneField DeskScene;
    [SerializeField] private SceneField BlogDetailScene;
    [SerializeField] private SceneField FailScene;

    public bool test;
    void Start()
    {
        SceneManager.LoadSceneAsync(DeskScene, LoadSceneMode.Additive);
        test = true;
        if (SceneManager.GetSceneByName(FailScene.SceneName).isLoaded)
        {
            UnloadFailScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void LoadBlogScene()
    {
        LoadBlogSceneForDay(1); 
    }
    
    // 加载指定天数的博客场景
    public void LoadBlogSceneForDay(int day)
    {
        if (GameDayManager.Instance != null)
        {
            string sceneName = GameDayManager.Instance.GetBlogSceneName(day);
            LoadBlogSceneByName(sceneName);
        }
        else
        {
            Debug.LogWarning("GameDayManager.Instance为空，使用默认博客场景");
            LoadBlogSceneByName("BlogScene");
        }
    }
    

    /// 加载当前天数的博客场景

    public void LoadCurrentDayBlogScene()
    {
        if (GameDayManager.Instance != null)
        {
            string sceneName = GameDayManager.Instance.GetCurrentBlogSceneName();
            LoadBlogSceneByName(sceneName);
        }
        else
        {
            Debug.LogWarning("GameDayManager.Instance为空，使用默认博客场景");
            LoadBlogSceneByName("BlogScene");
        }
    }
    

    // 根据场景名称加载博客场景

    private void LoadBlogSceneByName(string sceneName)
    {
        Debug.Log($"LoadBlogScene: {sceneName}");
        try
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"加载{sceneName}失败: {e.Message}");
        }
    }
    
    public void UnloadBlogScene()
    {
        Debug.Log("UnloadBlogScene - 卸载所有博客场景");
        
        // 卸载所有可能的博客场景
        string[] blogSceneNames = {
            "BlogScene",
            "BlogScene_Day1",
            "BlogScene_Day2", 
            "BlogScene_Day3",
            "BlogScene_Day4",
            "BlogScene_Day5",
            "BlogScene_Day6",
            "BlogScene_Day7"
        };
        
        foreach (string sceneName in blogSceneNames)
        {
            UnloadSceneByName(sceneName);
        }
    }
    

    private void UnloadSceneByName(string sceneName)
    {
        try
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            if (scene.IsValid() && scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneName);
                Debug.Log($"{sceneName} 卸载成功");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"卸载{sceneName}时出现异常: {e.Message}");
        }
    }
    public void UnloadDeskScene()
    {
        Debug.Log("UnloadDeskScene");
        try
        {
            // 检查场景是否存在且已加载
            var scene = SceneManager.GetSceneByName("DeskScene");
            if (scene.IsValid() && scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync("DeskScene");
                Debug.Log("DeskScene卸载成功");
            }
            else
            {
                Debug.Log("DeskScene未加载，跳过卸载");
            }
        }
        
        catch (System.Exception e)
        {
            Debug.LogWarning($"卸载DeskScene时出现异常，跳过执行: {e.Message}");
        }
    }
    public void loadDeskScene()
    {
        Debug.Log("loadDeskScene");
        SceneManager.LoadSceneAsync(DeskScene, LoadSceneMode.Additive);
    }
    public void UnloadFailScene()
    {
        Debug.Log("UnloadFailScene");
        SceneManager.UnloadSceneAsync(FailScene);
    }
    public void loadFailScene()
    {
        Debug.Log("loadFailScene");
        SceneManager.LoadSceneAsync(FailScene, LoadSceneMode.Additive);
    }
    public void UnloadDistributeScene()
    {
        Debug.Log("UnloadDistributeScene");
        SceneManager.UnloadSceneAsync(DistributePanalScene);
    }
    public void LoadDistributeScene()
    {
        Debug.Log("LoadDistributeScene");
        SceneManager.LoadSceneAsync(DistributePanalScene, LoadSceneMode.Additive);
    }

    public void LoadBlogDetailScene()
    {
        Debug.Log("LoadBlogDetailScene");
        try
        {

            UnloadBlogScene();
            StartCoroutine(LoadBlogDetailSceneCoroutine());
        }
        catch (System.Exception e)
        {
            Debug.LogError($"加载BlogDetailScene失败: {e.Message}");
        }
    }
    
    private IEnumerator LoadBlogDetailSceneCoroutine()
    {
        yield return null;
        var operation = SceneManager.LoadSceneAsync("BlogDetailScene", LoadSceneMode.Additive);
        yield return operation;
        
        if (operation.isDone)
        {
            Debug.Log("BlogDetailScene加载完成");
        }
    }

    public void UnloadBlogDetailScene()
    {
        Debug.Log("UnloadBlogDetailScene");
        try
        {

            var scene = SceneManager.GetSceneByName("BlogDetailScene");
            if (scene.IsValid() && scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync("BlogDetailScene");
            }
            else
            {
                Debug.LogWarning("BlogDetailScene未加载或无效，跳过卸载");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"卸载BlogDetailScene失败: {e.Message}");
        }
    }

    public void BackToBlogList()
    {
        Debug.Log("BackToBlogList - 从详情返回到博客浏览界面");
        UnloadBlogDetailScene();
        StartCoroutine(BackToBlogListCoroutine());
    }
    
    private IEnumerator BackToBlogListCoroutine()
    {
        yield return null;
        if (GameDayManager.Instance != null)
        {
            string currentBlogScene = GameDayManager.Instance.GetCurrentBlogSceneName();
            var operation = SceneManager.LoadSceneAsync(currentBlogScene, LoadSceneMode.Additive);
            yield return operation;
            
            if (operation.isDone)
            {
                Debug.Log($"{currentBlogScene}加载完成");
            }
        }
        else
        {
            var operation = SceneManager.LoadSceneAsync("BlogScene", LoadSceneMode.Additive);
            yield return operation;
            
            if (operation.isDone)
            {
                Debug.Log("BlogScene加载完成");
            }
        }
    }
    
    public void BackToInitialScene()
    {
        Debug.Log("BackToInitialScene - 返回到最初界面");
        UnloadBlogDetailScene();
        UnloadBlogScene();
        StartCoroutine(BackToInitialSceneCoroutine());
    }
    
    private IEnumerator BackToInitialSceneCoroutine()
    {
        yield return null;
        var operation = SceneManager.LoadSceneAsync(DeskScene, LoadSceneMode.Additive);
        yield return operation;
        
        if (operation.isDone)
        {
            Debug.Log("DeskScene加载完成");
        }
    }

}