using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BlogDetailDisplay : MonoBehaviour
{
    [Header("UI组件")]
    public Image blogImage;  
    public Text titleText;  
    public Text contentText;  
    
    [Header("动态文字设置")]
    public Transform textSpawnPosition;  
    public GameObject textPrefab;        
    

    private static HashSet<string> collectedTexts = new HashSet<string>();
    
    void Start()
    {
        LoadBlogContent();
    }
    
    void LoadBlogContent()
    {
        if (BlogContentManager.Instance == null)
        {
            Debug.LogWarning("BlogContentManager.Instance为空！");
            return;
        }
        
        BlogContentData data = BlogContentManager.Instance.GetCurrentBlog();
        if (data == null)
        {
            Debug.LogWarning("BlogContentData为空！");
            return;
        }
        
        // 更新图片
        if (blogImage != null)
        {
            blogImage.sprite = data.blogImage;
        }
        else
        {
            Debug.LogWarning("blogImage 为空！");
        }
        
        // 更新标题
        if (titleText != null)
        {
            titleText.text = data.title;
        }
        
        // 更新内容
        if (contentText != null)
        {
            contentText.text = data.content;
        }
        
        // 生成特定博客文字
        GenerateBlogSpecificText(data);
    }
    
    // 生成特定博客文字
    void GenerateBlogSpecificText(BlogContentData data)
    {
        string textKey = data.blogId;
        
        // 检查是否已经收集过这个文字
        if (collectedTexts.Contains(textKey))
        {
            return;
        }
        
        // 检查是否配置了可点击文字
        if (data.hasClickableText && !string.IsNullOrEmpty(data.clickableText))
        {
            CreateClickableText(data);
        }
    }
    
    // 创建可点击的文字
    void CreateClickableText(BlogContentData data)
    {
        // 使用SpawnPos位置
        Transform spawnPos = textSpawnPosition != null ? textSpawnPosition : transform;
        
        // 创建文字GameObject
        GameObject textObj = new GameObject($"ClickableText_{data.blogId}");
        textObj.transform.SetParent(spawnPos, false);
        
        // 添加TextMeshPro组件
        TMPro.TextMeshProUGUI textComponent = textObj.AddComponent<TMPro.TextMeshProUGUI>();
        textComponent.text = data.clickableText;
        textComponent.fontSize = Mathf.Max(data.textFontSize, 0.1f);
        textComponent.color = data.textColor;
        textComponent.alignment = TMPro.TextAlignmentOptions.Center;
        textComponent.enableWordWrapping = false;
        textComponent.enableAutoSizing = false;
        
        // 设置RectTransform
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = data.textSpawnPosition;
        rectTransform.sizeDelta = data.textSize;
        
        // 小字体缩放
        if (data.textFontSize < 10f)
        {
            float scaleFactor = data.textFontSize / 10f;
            textObj.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }
        
        // 添加脚本
        HighlightInformationMono highlightScript = textObj.AddComponent<HighlightInformationMono>();
        highlightScript.StringInformation = data.clickableText;
        highlightScript.missionName = data.missionName;
        highlightScript.missionIndex = data.missionIndex;
        
        TextCollector collector = textObj.AddComponent<TextCollector>();
        collector.textKey = data.blogId;
        collector.blogDetailDisplay = this;
    }
    
    

    public void MarkTextAsCollected(string textKey)
    {
        collectedTexts.Add(textKey);
    }

    public static void ResetCollectedTexts()
    {
        collectedTexts.Clear();
    }
}

// 文字收集标记
public class TextCollector : MonoBehaviour
{
    public string textKey;
    public BlogDetailDisplay blogDetailDisplay;
    private bool hasBeenClicked = false;
    
    void Start()
    {
        // 监听HighlightInformationMono的点击事件
        HighlightInformationMono highlightScript = GetComponent<HighlightInformationMono>();
        if (highlightScript != null)
        {
            StartCoroutine(CheckIfTextWasClicked());
        }
    }
    
    void Update()
    {
        // 直接监听HighlightInformationMono的HighLightStringStarttoMove变化
        if (!hasBeenClicked)
        {
            HighlightInformationMono highlightScript = GetComponent<HighlightInformationMono>();
            if (highlightScript != null && highlightScript.HighLightStringStarttoMove)
            {
                OnTextClicked();
            }
        }
    }
    
    // 被HighlightInformationMono直接调用的方法
    public void OnTextClicked()
    {
        hasBeenClicked = true;
        // 立即标记为已收集
        if (blogDetailDisplay != null)
        {
            blogDetailDisplay.MarkTextAsCollected(textKey);
        }
    }
    
    System.Collections.IEnumerator CheckIfTextWasClicked()
    {
        Vector3 startPos = transform.position;
        float checkTime = 0f;
        float maxCheckTime = 10f; 
        
        while (checkTime < maxCheckTime && !hasBeenClicked)
        {
            yield return new WaitForSeconds(0.1f);
            checkTime += 0.1f;

            if (Vector3.Distance(transform.position, startPos) > 0.01f)
            {
                hasBeenClicked = true;
                // 立即标记为已收集
                if (blogDetailDisplay != null)
                {
                    blogDetailDisplay.MarkTextAsCollected(textKey);
                }
                break;
            }
        }
    }
    
    void OnDestroy()
    {
        // 如果文字被点击了，标记为已收集
        if (hasBeenClicked && blogDetailDisplay != null)
        {
            blogDetailDisplay.MarkTextAsCollected(textKey);
        }
    }
}
