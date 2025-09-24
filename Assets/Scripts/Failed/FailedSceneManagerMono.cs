using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedScenemanagerMono : MonoBehaviour
{
    [Header("Control Scene")]
    [SerializeField] private SceneField StartScene;
    [SerializeField] private SceneField MainScene;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReloadStartScene()
    {
        SceneManager.UnloadSceneAsync(MainScene);
        SceneManager.LoadSceneAsync(StartScene, LoadSceneMode.Additive);
    }
}
