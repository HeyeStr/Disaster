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

            if (prefabContainer != null)
            {
                // 获取perfabs的父对象和位置
                Transform parent = prefabContainer.parent;
                Vector3 position = prefabContainer.position;
                Quaternion rotation = prefabContainer.rotation;
                Vector3 scale = prefabContainer.localScale;

                Destroy(prefabContainer.gameObject);

                GameObject prefabInstance = Instantiate(data.blogPrefab, parent);
                prefabInstance.transform.position = position;
                prefabInstance.transform.rotation = rotation;
                prefabInstance.transform.localScale = scale;

                prefabContainer = prefabInstance.transform;
                
                Debug.Log($"已替换perfabs对象为博客预制体: {data.title}");
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