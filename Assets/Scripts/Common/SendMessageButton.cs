using Phone;
using TMPro;
using UnityEngine;

namespace Common
{
    public class SendMessageButton : MonoBehaviour
    {
        public TextMeshPro textMeshPro;

        public AcceptMessage interactObj;
        
        private void Awake()
        {
        }

        private void OnMouseDown()
        {
            Debug.Log("按下按键");
            interactObj.AcceptString(textMeshPro.text);
        }
    }
}