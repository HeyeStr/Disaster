using Phone;
using TMPro;
using UnityEngine;

namespace Common
{
    public class SendMessageButton : MonoBehaviour
    {
        public TextMeshPro textMeshPro;

        public AcceptMessage interactObj;
        
        public bool canSelect;
        private void Awake()
        {
            canSelect = true;
        }

        private void OnMouseDown()
        {
            Debug.Log("选择对话开始");
            if (canSelect)
            {
                interactObj.AcceptString(this, textMeshPro.text);
            }
        }
    }
}