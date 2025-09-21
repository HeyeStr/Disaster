// 创建新文件：Assets/Scripts/Apps/Blog/BlogContentData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Blog Content", menuName = "Blog/Blog Content Data")]
public class BlogContentData : ScriptableObject
{
    [Header("博客基本信息")]
    public string blogId;           // 博客唯一ID
    public string title;            // 博客标题
    [TextArea(3, 5)]
    public string content;          // 博客内容
    
    [Header("视觉资源")]
    public Sprite blogImage;        // 博客图片
    public Sprite backgroundImage;  // 背景图片（可选）
    
    [Header("样式设置")]
    public Color titleColor = Color.black;
    public Color contentColor = Color.gray;
    public Font titleFont;
    public Font contentFont;
}