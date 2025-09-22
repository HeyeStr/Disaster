using UnityEngine;

[CreateAssetMenu(fileName = "New Blog Content", menuName = "Blog/BlogContentData")]
public class BlogContentData : ScriptableObject
{
    [Header("博客基本信息")]
    public string blogId;            
    public string title;               
    [TextArea(3, 5)]
    public string content;
    public string BlogType = "Blog";   // 博客类型            
    
    [Header("视觉资源")]
    public Sprite blogImage;           
    public GameObject blogPrefab;      
    
    [Header("任务信息")]
    public int MissionIndex;         
    public int RequireFoodQuantity;    
    public int RequireLivingQuantity;  
    public int RequireMedicineQuantity; 
}
