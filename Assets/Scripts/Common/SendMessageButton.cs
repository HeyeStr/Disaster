using Phone;
using TMPro;
using UnityEngine;

namespace Common
{
    public class SendMessageButton : MonoBehaviour
    {
        public bool isEndButton;
        
        public TextMeshProUGUI textMeshPro;

        public AcceptMessage interactObj;
        
        public bool canSelect = true;


        public string turnIndex;
        
        private void Awake()
        {
            canSelect = true;
        }

        private void OnMouseDown()
        {
            Debug.Log("选择对话开始");
            if (canSelect)
            {
                Debug.Log(JsonUtility.ToJson(interactObj));
                if (interactObj is PhoneController)
                {
                    interactObj.AcceptString(this, textMeshPro.text);
                }
                else {
                    interactObj.AcceptString(this, turnIndex);
                }
            }
        }
    }
}