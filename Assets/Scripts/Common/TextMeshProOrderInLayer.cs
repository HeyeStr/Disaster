using UnityEngine;

namespace Common
{
    /**
     * 文字层级控制
     */
    public class TextMeshProOrderInLayer : MonoBehaviour
    {
        [Header("渲染层级设置")]
        [SerializeField] private string sortingLayerName;
        [SerializeField] private int orderInLayer;

        public Renderer textRenderer;

        // 在编辑器中实时更新
        void OnValidate()
        {
            InitializeComponents();
            ApplySortingSettings();
        }

        void InitializeComponents()
        {
            if (textRenderer == null)
                textRenderer = GetComponent<Renderer>();
        }
        
        void ApplySortingSettings()
        {
            if (textRenderer != null)
            {
                textRenderer.sortingLayerName = sortingLayerName;
                textRenderer.sortingOrder = orderInLayer;
            }
        }
    }
}