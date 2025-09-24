using UnityEngine;
using System.Collections;
using System.Threading;

public class DistributeDeleteButton : MonoBehaviour
{
    private GameObject MonitorGameObject;
    private Transform buttonTransform;
    
    [Header("悬停效果设置")]
    public float hoverScale = 1.1f;
    public float animationSpeed = 10f;
    
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isHovering = false;
    private GameObject distributeControlPage;

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
        Debug.Log("鼠标悬停到按钮上");
    }
    
    void OnMouseExit()
    {
        isHovering = false;
        targetScale = originalScale;
        Debug.Log("鼠标离开按钮");
    }
    
    private void OnMouseDown()
    {
        Debug.Log("BlogDeleteButton 按钮被点击！");
        
        
        if (MonitorGameObject != null)
        {
            distributeControlPage = GameObject.FindGameObjectWithTag("DistributePage");
            ResourcesManager resourcesManager = MonitorGameObject.GetComponent<ResourcesManager>();
            foreach (var bar in distributeControlPage.GetComponent<DistributeControlMono>().DistributeBars)
            {
                resourcesManager.LivingResource += bar.GetComponent<DistributeBarMono>().LivingResourceQuantity;
                resourcesManager.FoodResource += bar.GetComponent<DistributeBarMono>().FoodResourceQuantity;
                resourcesManager.MedicalResource += bar.GetComponent<DistributeBarMono>().MedicalResourceQuantity;
            }

            SceneControlMono sceneControl = MonitorGameObject.GetComponent<SceneControlMono>();
            if (sceneControl != null)
            {
                sceneControl.UnloadDistributeScene();
                sceneControl.loadDeskScene();
            }
        }
    }
}