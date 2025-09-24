using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class FadeInControlMono : MonoBehaviour
{
    public float fadeDurTime;
    void Start()
    {
        fadeDurTime = 2;
        StartCoroutine(FadeIn(gameObject.GetComponent<Image>()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FadeIn(Image image)
    {
        if (!image)
        {
            yield break;
        }

        //Color originalColor = new Color(255, 255, 255, 255);
        //Color targetColor = new Color(0, 0, 0, 0);

        Color originalColor = Color.black ;
        Color targetColor = new Color(0, 0, 0, 0);
        image.color = originalColor;
        float timer = 0f;
        // µ­³ö¹ý³Ì
        while (timer < fadeDurTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDurTime);
            image.color = Color.Lerp(originalColor, targetColor, progress); //new Color(0, 0, 0, math.lerp(255, 0, progress));   // Color.Lerp(originalColor, targetColor, progress);

            yield return null;
        }
        image.color = targetColor;
        GameObject Monitor = GameObject.FindGameObjectWithTag("Monitor");
        SceneControlMono sceneControlMono= Monitor.GetComponent<SceneControlMono>();
        sceneControlMono.UnloadFadeInScene();

    }
}
