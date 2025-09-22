// 创建新文件：Assets/Scripts/Apps/Blog/BlogContentData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission Content", menuName = "Missions/MissionContentData")]
public class MissionContentData : ScriptableObject
{
    [Header("任务基本信息")]
    public int MissionIndex;           // 博客唯一ID



    [Header("任务正确资源信息")]
    public int RequireFoodQuantity;
    public int RequireLivingQuantity;
    public int RequireMedicineQuantity;


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
    
    [Header("可点击文字设置")]
    public bool hasClickableText = false;           // 是否生成可点击文字
    public string clickableText = "";               // 可点击文字内容
    public Vector2 textSpawnPosition = Vector2.zero; // 文字生成位置
    public float textFontSize = 16f;                // 文字字体大小
    public Color textColor = Color.black;           // 文字颜色
    public Vector2 textSize = new Vector2(200, 30); // 文字框大小
    public string missionName = "";                 // 任务名称
    public int missionIndex = 0;                    // 任务索引
}