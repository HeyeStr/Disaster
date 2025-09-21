using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BlogDetailDisplay : MonoBehaviour
{
    [Header("UI组件")]
    public Image blogImage;  
    public Text titleText;    // 标题文本
    public Text contentText;  // 内容文本
    
    [Header("动态文字设置")]
    public Transform textSpawnPosition;  // 文字生成位置（可选，会使用BlogContentData中的位置）
    public GameObject textPrefab;        // 文字预制体（可选）
    
    // 存储已收集的文字，避免重复生成
    private static HashSet<string> collectedTexts = new HashSet<string>();
    
    void Start()
    {
        LoadBlogContent();
    }
    
    void LoadBlogContent()
    {
        Debug.Log("LoadBlogContent 开始执行");
        
        if (BlogContentManager.Instance != null)
        {
            Debug.Log("BlogContentManager.Instance 存在");
            BlogContentData data = BlogContentManager.Instance.GetCurrentBlog();
            if (data != null)
            {
                Debug.Log($"获取到博客数据: {data.title}");
                
                // 更新图片
                if (blogImage != null)
                {
                    blogImage.sprite = data.blogImage;
                    Debug.Log("图片已更新");
                }
                else
                {
                    Debug.LogWarning("blogImage 为空！");
                }
                
                // 更新标题
                if (titleText != null)
                {
                    titleText.text = data.title;
                    Debug.Log($"标题已更新: {data.title}");
                }
                else
                {
                    Debug.Log("titleText 未配置，跳过标题更新");
                }
                
                // 更新内容（可选）
                if (contentText != null)
                {
                    contentText.text = data.content;
                    Debug.Log($"内容已更新: {data.content}");
                }
                else
                {
                    Debug.Log("contentText 未配置，跳过内容更新");
                }
                
                // 生成特定博客文字
                GenerateBlogSpecificText(data);
                
                Debug.Log($"博客内容已更新: {data.title}");
            }
            else
            {
                Debug.LogWarning("BlogContentData为空！");
            }
        }
        else
        {
            Debug.LogWarning("BlogContentManager.Instance为空！");
        }
    }
    
    // 生成特定博客文字
    void GenerateBlogSpecificText(BlogContentData data)
    {
        string textKey = data.blogId;
        
        Debug.Log($"=== 开始生成博客文字 ===");
        Debug.Log($"博客ID: {textKey}");
        Debug.Log($"Has Clickable Text: {data.hasClickableText}");
        Debug.Log($"Clickable Text: '{data.clickableText}'");
        Debug.Log($"Text Spawn Position: {data.textSpawnPosition}");
        Debug.Log($"Text Font Size: {data.textFontSize}");
        Debug.Log($"Text Size: {data.textSize}");
        Debug.Log($"Text Color: {data.textColor}");
        
        // 检查是否已经收集过这个文字
        if (collectedTexts.Contains(textKey))
        {
            Debug.Log($"文字 {textKey} 已经被收集，跳过生成");
            return;
        }
        
        // 检查是否配置了可点击文字
        if (data.hasClickableText && !string.IsNullOrEmpty(data.clickableText))
        {
            Debug.Log($"开始创建可点击文字...");
            CreateClickableText(data);
            Debug.Log($"为博客 {data.blogId} 生成了文字: {data.clickableText}");
        }
        else
        {
            Debug.LogWarning($"博客 {data.blogId} 未配置可点击文字 - hasClickableText: {data.hasClickableText}, clickableText: '{data.clickableText}'");
        }
    }
    
    // 创建可点击的文字
    void CreateClickableText(BlogContentData data)
    {
        // 先使用SpawnPos位置，确保文字能显示
        Transform spawnPos = textSpawnPosition != null ? textSpawnPosition : transform;
        
        // 创建文字GameObject
        GameObject textObj = new GameObject($"ClickableText_{data.blogId}");
        textObj.transform.SetParent(spawnPos, false);
        
        // 添加TextMeshPro组件（更好的字体大小控制）
        TMPro.TextMeshProUGUI textComponent = textObj.AddComponent<TMPro.TextMeshProUGUI>();
        textComponent.text = data.clickableText;
        
        // 强制设置字体大小，即使很小也要生效
        textComponent.fontSize = Mathf.Max(data.textFontSize, 0.1f); // 最小0.1
        textComponent.color = data.textColor;
        textComponent.alignment = TMPro.TextAlignmentOptions.Center;
        textComponent.enableWordWrapping = false;
        
        // 强制设置Auto Size为false，确保使用我们设置的大小
        textComponent.enableAutoSizing = false;
        
        // 设置RectTransform
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        
        // 使用BlogContentData中的位置设置
        rectTransform.anchoredPosition = data.textSpawnPosition;
        
        // 使用BlogContentData中的文字大小设置
        rectTransform.sizeDelta = data.textSize;
        
        // 强制设置文字大小（通过缩放RectTransform）
        // 如果字体大小小于10，就使用缩放来减小
        if (data.textFontSize < 10f)
        {
            float scaleFactor = data.textFontSize / 10f; // 以10为基准进行缩放
            textObj.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            Debug.Log($"应用缩放因子: {scaleFactor} (字体大小: {data.textFontSize})");
        }
        
        // 确保文字在正确的位置
        textObj.transform.localPosition = Vector3.zero;
        
        // 添加HighlightInformationMono脚本
        HighlightInformationMono highlightScript = textObj.AddComponent<HighlightInformationMono>();
        highlightScript.StringInformation = data.clickableText;
        highlightScript.missionName = data.missionName;
        highlightScript.missionIndex = data.missionIndex;
        
        // 添加收集标记脚本（在HighlightInformationMono销毁前标记为已收集）
        TextCollector collector = textObj.AddComponent<TextCollector>();
        collector.textKey = data.blogId;
        collector.blogDetailDisplay = this;
        
        Debug.Log($"创建了可点击文字: {data.clickableText}");
        Debug.Log($"文字位置: {textObj.transform.position}");
        Debug.Log($"文字本地位置: {textObj.transform.localPosition}");
        Debug.Log($"文字RectTransform位置: {rectTransform.anchoredPosition}");
        Debug.Log($"文字大小: {rectTransform.sizeDelta}");
        Debug.Log($"文字颜色: {textComponent.color}");
        Debug.Log($"文字字体大小: {textComponent.fontSize} (TextMeshPro)");
        Debug.Log($"=== 配置验证 ===");
        Debug.Log($"配置的字体大小: {data.textFontSize}");
        Debug.Log($"配置的文字大小: {data.textSize}");
        Debug.Log($"配置的位置: {data.textSpawnPosition}");
        Debug.Log($"配置的颜色: {data.textColor}");
    }
    
    // 查找或创建Overlay Canvas
    Transform FindOrCreateOverlayCanvas()
    {
        // 先查找是否已存在Overlay Canvas
        Canvas overlayCanvas = GameObject.Find("TextOverlayCanvas")?.GetComponent<Canvas>();
        
        if (overlayCanvas == null)
        {
            // 创建新的Overlay Canvas
            GameObject canvasObj = new GameObject("TextOverlayCanvas");
            overlayCanvas = canvasObj.AddComponent<Canvas>();
            overlayCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            overlayCanvas.sortingOrder = 10; // 确保在最上层
            
            // 添加Canvas Scaler
            var scaler = canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            
            // 添加Graphic Raycaster
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            
            Debug.Log("创建了Overlay Canvas用于文字显示");
        }
        
        return overlayCanvas.transform;
    }
    
    // 计算文字在屏幕上的位置
    Vector2 CalculateTextPosition()
    {
        // 获取Scroll View中的图片位置
        ScrollRect scrollRect = FindObjectOfType<ScrollRect>();
        if (scrollRect != null && scrollRect.content != null)
        {
            // 找到图片
            Image blogImage = scrollRect.content.GetComponentInChildren<Image>();
            if (blogImage != null)
            {
                // 将图片的世界坐标转换为屏幕坐标
                Vector3 imageWorldPos = blogImage.transform.position;
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, imageWorldPos);
                
                // 转换为Canvas坐标
                Canvas canvas = FindOrCreateOverlayCanvas().GetComponent<Canvas>();
                Vector2 canvasPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform, 
                    screenPos, 
                    canvas.worldCamera, 
                    out canvasPos
                );
                
                // 在图片中心稍微偏移
                canvasPos += new Vector2(0, 50); // 在图片上方50像素
                
                Debug.Log($"计算文字位置: {canvasPos}");
                return canvasPos;
            }
        }
        
        // 如果找不到图片，使用默认位置
        return new Vector2(0, 100);
    }
    
    // 查找Scroll View的Content
    Transform FindScrollViewContent()
    {
        // 查找Scroll View
        ScrollRect scrollRect = FindObjectOfType<ScrollRect>();
        if (scrollRect != null && scrollRect.content != null)
        {
            Debug.Log("找到Scroll View Content，文字将生成在其中");
            return scrollRect.content;
        }
        
        Debug.Log("未找到Scroll View Content，使用默认位置");
        return null;
    }
    
    // 标记文字为已收集
    public void MarkTextAsCollected(string textKey)
    {
        collectedTexts.Add(textKey);
        Debug.Log($"文字 {textKey} 已标记为收集");
    }
    
    // 重置收集状态（用于测试）
    public static void ResetCollectedTexts()
    {
        collectedTexts.Clear();
        Debug.Log("已重置收集状态");
    }
}

// 文字收集标记脚本（只有在文字被点击飞入ToDoList时才标记为已收集）
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
            // 使用协程检测文字是否开始移动（表示被点击了）
            StartCoroutine(CheckIfTextWasClicked());
        }
    }
    
    System.Collections.IEnumerator CheckIfTextWasClicked()
    {
        Vector3 startPos = transform.position;
        float checkTime = 0f;
        float maxCheckTime = 5f; // 最多检查5秒
        
        while (checkTime < maxCheckTime && !hasBeenClicked)
        {
            yield return new WaitForSeconds(0.1f);
            checkTime += 0.1f;
            
            // 如果文字移动了，说明HighlightInformationMono开始工作了
            if (Vector3.Distance(transform.position, startPos) > 0.1f)
            {
                hasBeenClicked = true;
                Debug.Log($"检测到文字 {textKey} 被点击并开始移动");
                
                // 等待文字飞入ToDoList完成
                yield return new WaitForSeconds(2f);
                
                // 标记为已收集
                if (blogDetailDisplay != null)
                {
                    blogDetailDisplay.MarkTextAsCollected(textKey);
                }
                break;
            }
        }
        
        // 如果5秒内没有移动，说明没有被点击，不标记为收集
        if (!hasBeenClicked)
        {
            Debug.Log($"文字 {textKey} 在5秒内未被点击，不标记为收集");
        }
    }
    
    void OnDestroy()
    {
        // 只有在文字被点击后才标记为收集
        if (hasBeenClicked && blogDetailDisplay != null)
        {
            blogDetailDisplay.MarkTextAsCollected(textKey);
        }
    }
}
