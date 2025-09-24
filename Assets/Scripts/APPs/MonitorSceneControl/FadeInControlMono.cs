using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInControlMono : MonoBehaviour
{
    public float fadeDurTime;
    public GameObject Page;
    void Start()
    {
        fadeDurTime = 50;
        StartCoroutine(FadeIn(Page.GetComponent<Image>()));
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

        Color originalColor = new Color(0, 0, 0, 255);
        Color targetColor = new Color(0, 0, 0, 0);

        image.color = originalColor;
        float timer = 0f;
        // µ­³ö¹ý³Ì
        while (timer < fadeDurTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDurTime);
            image.color = Color.Lerp(originalColor, targetColor, progress);
            Debug.Log(image.color.a + "image.color");
            yield return null;
        }
        image.color = targetColor;
        GameObject Monitor = GameObject.FindGameObjectWithTag("Monitor");
        SceneControlMono sceneControlMono= Monitor.GetComponent<SceneControlMono>();
        sceneControlMono.UnloadFadeInScene();

    }
}
