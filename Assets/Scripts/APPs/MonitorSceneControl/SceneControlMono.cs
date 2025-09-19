using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControlMono : MonoBehaviour                                       //控制显示器上不同层级对应场景Scene的打开关闭
{
    [Header("Control Scene")]
    [SerializeField] private SceneField Monitor;
    [SerializeField] private SceneField Blog;
    [SerializeField] private SceneField DistributePanal;
    [SerializeField] private SceneField DataSubscene;

    public bool test;
    void Start()
    {
        test = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!test)
        {
            SceneManager.LoadSceneAsync(Blog, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(Blog);
            test = true;
        }
    }
    public void LoadBlogScene()
    {
        SceneManager.LoadSceneAsync(Blog, LoadSceneMode.Additive);
    }
}