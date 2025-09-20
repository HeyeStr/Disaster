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

    public bool test;
    void Start()
    {
        SceneManager.LoadSceneAsync(DeskScene, LoadSceneMode.Additive);
        test = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!test)
        {
            SceneManager.LoadSceneAsync(BlogScene, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(BlogScene);
            test = true;
        }
    }
    public void LoadBlogScene()
    {
        Debug.Log("LoadBlogScene");
        SceneManager.LoadSceneAsync(BlogScene, LoadSceneMode.Additive);
    }
    
    public void UnloadBlogScene()
    {
        Debug.Log("UnloadBlogScene");
        try
        {
            // 检查场景是否存在且已加载
            var scene = SceneManager.GetSceneByName("BlogScene");
            if (scene.IsValid() && scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync("BlogScene");
            }
            else
            {
                Debug.LogWarning("BlogScene未加载或无效，跳过卸载");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"卸载BlogScene失败: {e.Message}");
        }
    }
    public void UnloadDeskScene()
    {
        Debug.Log("UnloadDeskScene");
        SceneManager.UnloadSceneAsync(DeskScene);
    }
    public void loadDeskScene()
    {
        Debug.Log("loadDeskScene");
        SceneManager.LoadSceneAsync(DeskScene, LoadSceneMode.Additive);
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
            SceneManager.LoadSceneAsync("BlogDetailScene", LoadSceneMode.Additive);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"加载BlogDetailScene失败: {e.Message}");
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
        
        LoadBlogScene();
        UnloadBlogDetailScene();
        


    }
    
    public void BackToInitialScene()
    {
        Debug.Log("BackToInitialScene - 返回到最初界面");

        loadDeskScene();
        UnloadBlogDetailScene();
        UnloadBlogScene();
        
        
    }

}