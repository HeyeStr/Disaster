using UnityEngine;

public class BlogSwichCtl : MonoBehaviour
{
    [Header("博客内容设置")]
    public string blogId = "white_blog";
    
    private GameObject MonitorGameObject;
    
    void Start()
    {
        MonitorGameObject = GameObject.FindGameObjectWithTag("Monitor");
    }
    
    private void OnMouseDown()
    {
        // 检查是否点击到了其他UI元素
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePos);
        

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject && 
                (col.GetComponent<BlogDeleteButton>() != null || 
                 col.GetComponent<BlogBackButton>() != null ||
                 col.GetComponent<BlogDetailDeleteButton>() != null))
            {
                Debug.Log("点击到了其他UI元素，跳过博客图片点击");
                return;
            }
        }
        
        Debug.Log($"点击了博客图片，博客ID: {blogId}");
        
        if (MonitorGameObject != null)
        {
            SceneControlMono sceneControl = MonitorGameObject.GetComponent<SceneControlMono>();
            if (sceneControl != null)
            {
                // 设置当前博客内容
                if (BlogContentManager.Instance != null)
                {
                    BlogContentManager.Instance.SetCurrentBlog(blogId);
                }
                
                sceneControl.LoadBlogDetailScene();
            }
            else
            {
                Debug.LogWarning("Monitor对象上没有SceneControlMono组件！");
            }
        }
        else
        {
            Debug.LogWarning("Monitor对象未找到！");
        }
    }
}