using Dialogue;
using Phone;
using TMPro;
using UnityEngine;

namespace Common
{
    public class  SendMessageButton : MonoBehaviour
    {
        public bool isEndButton;

        public TextMeshProUGUI textMeshPro;

        public AcceptMessage interactObj;

        public bool isPhoneNumber;

        public string turnIndex;

        public void OnMouseDown()
        {
            Debug.Log("选择对话开始");
            Debug.Log(JsonUtility.ToJson(interactObj));
            if (interactObj is PhoneController)
            {
                interactObj.AcceptString(this, textMeshPro.text);
                interactObj = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueManager>();
                isPhoneNumber = true;
            }
            else
            {
                interactObj.AcceptString(this, turnIndex);
            }
        }
    }
}