using System.Collections;
using Common;
using Dialogue;
using TMPro;
using UnityEngine;

namespace Phone
{
    public class PhoneController : AcceptMessage
    {
        [Header("UI引用")]
        public TMP_Text numberLabel;

        [Header("时间设置")]
        public float durTime;
        
        public string phoneNumber;
        
        public DialogueManager dialogueManager;
        
        public AudioSource audioSource;
        
        public SlideMoveComponent slideMove;
        
        private void Awake()
        {
            dialogueManager = GameObject.FindGameObjectWithTag("Dialogue")?.GetComponent<DialogueManager>();
            slideMove = GetComponent<SlideMoveComponent>();
            audioSource = GetComponent<AudioSource>();
            numberLabel = GetComponentInChildren<TMP_Text>();
            numberLabel.text = "";
        }

        IEnumerator CallPhoneWordByWord(string message)
        {
            numberLabel.text = "";
            foreach (var num in message)
            {
                if (slideMove.startClose)
                    yield break;
                numberLabel.text += num;
                yield return new WaitForSeconds(durTime);
            }

            phoneNumber = message;
        }

        public override void AcceptString(SendMessageButton button, string message)
        {
            Debug.Log("电话号码为" + message);
            if (!slideMove.buttonClose)
                StartCoroutine(CallPhoneWordByWord(message));
        }
    }
}