using UnityEngine;
using UnityEngine.UI;

public class AutoScrollViewPadding : MonoBehaviour
{
    [Header("设置")]
    [Range(0.1f, 0.5f)]
    public float paddingRatio = 0.15f; // Padding占Viewport高度的比例
    
    private ScrollRect scrollRect;
    private VerticalLayoutGroup layoutGroup;
    private RectTransform viewportRect;
    
    void Start()
    {
        // 获取组件引用
        scrollRect = GetComponentInParent<ScrollRect>();
        layoutGroup = GetComponent<VerticalLayoutGroup>();
        viewportRect = scrollRect.viewport;
        
        // 初始计算
        UpdatePadding();
    }
    
    void Update()
    {
        // 可选：实时更新（如果需要响应屏幕旋转等）
        UpdatePadding();
    }
    
    void UpdatePadding()
    {
        if (viewportRect != null && layoutGroup != null)
        {
            float viewportHeight = viewportRect.rect.height;
            int paddingValue = Mathf.RoundToInt(viewportHeight * paddingRatio);
            
            layoutGroup.padding.top = paddingValue;
            layoutGroup.padding.bottom = paddingValue;
        }
    }
}