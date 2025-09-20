using UnityEngine;

public class BlogSwichCtl : MonoBehaviour
{
    public GameObject MonitorGameObject;
    
    void Start()
    {
        MonitorGameObject = GameObject.FindGameObjectWithTag("Monitor");
    }
    
    private void OnMouseDown()
    {
        // 检查是否点击到了其他UI元素（如按钮）
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePos);
        
        // 如果点击到了其他UI元素，不执行博客图片的点击逻辑
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
        
        Debug.Log("点击了博客图片，准备切换到博客详情场景");
        
        if (MonitorGameObject != null)
        {
            SceneControlMono sceneControl = MonitorGameObject.GetComponent<SceneControlMono>();
            if (sceneControl != null)
            {
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
