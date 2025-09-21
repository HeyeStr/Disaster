using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlogDetailDisplay : MonoBehaviour
{
    [Header("UI组件")]
    public Image blogImage;  
    public Text titleText;    // 标题文本
    public Text contentText;  // 内容文本
    
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
}
