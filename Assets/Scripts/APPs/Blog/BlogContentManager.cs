using UnityEngine;

public class BlogContentManager : MonoBehaviour
{
    public static BlogContentManager Instance;
    
    [Header("当前博客内容")]
    public BlogContentData currentBlogData;
    
    [Header("博客内容列表")]
    public BlogContentData[] allBlogContents;
    
    void Awake()
    {
        Instance = this;
    }
    
    public void SetCurrentBlog(string blogId)
    {
        foreach (var blog in allBlogContents)
        {
            if (blog.blogId == blogId)
            {
                currentBlogData = blog;
                Debug.Log($"切换到博客: {blog.title}");
                return;
            }
        }
        Debug.LogWarning($"未找到博客ID: {blogId}");
    }
    
    public BlogContentData GetCurrentBlog()
    {
        return currentBlogData;
    }
}
