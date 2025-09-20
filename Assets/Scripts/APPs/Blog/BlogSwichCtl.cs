using UnityEngine;

public class BlogSwichCtl : MonoBehaviour
{
    public GameObject MonitorGameObject;
    
    void Start()
    {
        MonitorGameObject = GameObject.FindGameObjectWithTag("Monitor");
        
        // 确保有BoxCollider2D组件用于点击检测
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        
        // 设置为trigger
        boxCollider.isTrigger = true;
        
        // 自动调整碰撞体大小以匹配图片大小
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2 size = rectTransform.sizeDelta;
            Vector3 scale = rectTransform.localScale;
            boxCollider.size = new Vector2(size.x * scale.x, size.y * scale.y);
        }
    }
    
    private void OnMouseDown()
    {
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
            Debug.LogWarning("Monitor对象未找到！请确保场景中有带有'Monitor'标签的对象");
        }
    }
}
