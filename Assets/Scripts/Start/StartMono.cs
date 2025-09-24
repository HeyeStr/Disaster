using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Distribute;
using UnityEngine.SceneManagement;
using Unity.Collections;
using Unity.Mathematics;

public class StartMono : MonoBehaviour
{
    [SerializeField] private SceneField MainScene;
    [SerializeField] private SceneField StartScene;
    [SerializeField] private SceneField FailScene;
    [SerializeField] private SceneField SuccessScene;
    public GameObject ImageStart;
    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public float fadeDurTime;
    public float stayTime;
    void Start()
    {
        fadeDurTime = 2f;
        stayTime = 2f;
        if (SceneManager.GetSceneByName(FailScene.SceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(FailScene);
        }
        if (SceneManager.GetSceneByName(SuccessScene.SceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(SuccessScene);
        }
        SceneManager.LoadSceneAsync(MainScene, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        StartCoroutine(FadeOutSuccess());
        
    }
    public  IEnumerator FadeOutSuccess()
    {
        
        float timer = 0f;
        Image Image_Start = ImageStart.GetComponent<Image>();
        Color originalColor = Image_Start.color;
        Color targetColor = new Color(255,255,255,0);
        while (timer < fadeDurTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDurTime);
            Image_Start.color = Color.Lerp(originalColor, targetColor, progress);

            yield return null;
        }
        timer = 0f;
        while (timer < stayTime)
        {
            timer += Time.deltaTime;

            yield return null;
        }
        timer = 0f;
        Image Image_1 = Image1.GetComponent<Image>();
        originalColor= Image_1.color;
        while (timer < fadeDurTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDurTime);
            Image_1.color = Color.Lerp(originalColor, targetColor, progress);

            yield return null;
        }
        timer = 0f;
        while (timer < stayTime)
        {
            timer += Time.deltaTime;

            yield return null;
        }
        timer = 0f;
        Image Image_2 = Image2.GetComponent<Image>();
        originalColor = Image_2.color;
        while (timer < fadeDurTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDurTime);
            Image_2.color = Color.Lerp(originalColor, targetColor, progress);

            yield return null;
        }
        timer = 0f;

        while (timer < stayTime)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        timer = 0f;
        Image Image_3 = Image3.GetComponent<Image>();
        originalColor = Image_3.color;
        while (timer < fadeDurTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDurTime);
            Image_3.color = Color.Lerp(originalColor, targetColor, progress);

            yield return null;
        }

        
        SceneManager.UnloadSceneAsync(StartScene);


    }
}
