using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedScenemanagerMono : MonoBehaviour
{
    [Header("Control Scene")]
    [SerializeField] private SceneField MainScene;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReloadMainScene()
    {
        SceneManager.UnloadSceneAsync(MainScene);
        SceneManager.LoadSceneAsync(MainScene, LoadSceneMode.Additive);
    }
}
