using UnityEngine;
using UnityEngine.UI;

public class BlogSwichCtl : MonoBehaviour
{
    public GameObject MonitorGameObject;
    
    void Start()
    {
        MonitorGameObject = GameObject.FindGameObjectWithTag("Monitor");
        
        // 确保Image组件可以接收射线检测
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = true;
        }
        
        // 确保有Collider2D组件用于点击检测
        Collider2D collider = GetComponent<Collider2D>();
        BoxCollider2D boxCollider = null;
        
        if (collider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
        }
        else
        {
            // 如果是Circle Collider，改为Box Collider
            if (collider is CircleCollider2D)
            {
                DestroyImmediate(collider);
                boxCollider = gameObject.AddComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;
            }
            else if (collider is BoxCollider2D)
            {
                boxCollider = collider as BoxCollider2D;
            }
        }
        

        if (boxCollider != null)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                Vector2 size = rectTransform.sizeDelta;
                Vector3 scale = rectTransform.localScale;
                boxCollider.size = new Vector2(size.x * scale.x, size.y * scale.y);
            }
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
