using UnityEngine;

public class BlogBackButton : MonoBehaviour
{
    private GameObject MonitorGameObject;
    private Transform buttonTransform;
    
    [Header("悬停效果设置")]
    public float hoverScale = 1.1f;
    public float animationSpeed = 10f;
    
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isHovering = false;
    
    void Start()
    {
        MonitorGameObject = GameObject.FindGameObjectWithTag("Monitor");
        buttonTransform = transform;
        originalScale = buttonTransform.localScale;
        targetScale = originalScale;
    }
    
    void Update()
    {
        buttonTransform.localScale = Vector3.Lerp(buttonTransform.localScale, targetScale, Time.deltaTime * animationSpeed);
    }
    
    void OnMouseEnter()
    {
        isHovering = true;
        targetScale = originalScale * hoverScale;
        Debug.Log("鼠标悬停到返回按钮上");
    }
    
    void OnMouseExit()
    {
        isHovering = false;
        targetScale = originalScale;
        Debug.Log("鼠标离开返回按钮");
    }
    
    private void OnMouseDown()
    {
        Debug.Log("点击了返回按钮，准备返回到博客列表场景");
        
        if (MonitorGameObject != null)
        {
            SceneControlMono sceneControl = MonitorGameObject.GetComponent<SceneControlMono>();
            if (sceneControl != null)
            {
                sceneControl.BackToBlogList();
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
