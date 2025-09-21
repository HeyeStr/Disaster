using UnityEngine;
using UnityEngine.UI;

public class BlogDetailDisplay : MonoBehaviour
{
    [Header("UI组件")]
    public Image blogImage;  
    
    void Start()
    {
        LoadBlogContent();
    }
    
    void LoadBlogContent()
    {
        if (BlogContentManager.Instance != null)
        {
            BlogContentData data = BlogContentManager.Instance.GetCurrentBlog();
            if (data != null)
            {
                if (blogImage != null)
                {
                    blogImage.sprite = data.blogImage;
                    Debug.Log($"博客图片已更新: {data.title}");
                }
            }
        }
    }
}
