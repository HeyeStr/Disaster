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
        SceneManager.UnloadSceneAsync(BlogScene);
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
            SceneManager.UnloadSceneAsync("BlogDetailScene");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"卸载BlogDetailScene失败: {e.Message}");
        }
    }

    public void BackToBlogList()
    {
        Debug.Log("BackToBlogList");

        UnloadBlogDetailScene();

        LoadBlogScene();
    }

}