using UnityEngine;
using UnityEngine.UI;

public class BlogDetailDisplay : MonoBehaviour
{
    [Header("UI组件")]
    public Image blogImage;  
    public Text titleText;  
    public Text contentText;  
    
    [Header("预制体显示")]
    public Transform prefabContainer;  // 预制体容器
    
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

        // 更新图片或预制体
        if (data.blogPrefab != null)
        {
            // 如果有预制体，实例化预制体
            if (prefabContainer != null)
            {
                // 清除之前的预制体
                foreach (Transform child in prefabContainer)
                {
                    Destroy(child.gameObject);
                }
                
                // 实例化新预制体
                GameObject prefabInstance = Instantiate(data.blogPrefab, prefabContainer);
                Debug.Log($"已实例化博客预制体: {data.title}");
            }
        }
        else if (blogImage != null && data.blogImage != null)
        {
            // 如果没有预制体但有图片，使用图片
            blogImage.sprite = data.blogImage;
            Debug.Log($"已更新博客图片: {data.title}");
        }

        // 处理任务相关逻辑
        if (data.BlogType == "Mission")
        {
            BlogContentManager.Instance.MissionDisplay_TodoList(data.blogId);
        }

        // 更新标题
        if (titleText != null)
        {
            titleText.text = data.title;
            Debug.Log($"标题已更新: {data.title}");
        }

        // 更新内容
        if (contentText != null)
        {
            contentText.text = data.content;
            Debug.Log($"内容已更新: {data.content}");
        }
        
        Debug.Log($"博客内容已更新: {data.title}");
    }
}