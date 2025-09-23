using System.Collections;
using Common;
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

        private void Awake()
        {
            numberLabel.text = "";
        }

        IEnumerator CallPhoneWordByWord(string message)
        {
            numberLabel.text = "";
            foreach (var num in message)
            {
                numberLabel.text += num;
                yield return new WaitForSeconds(durTime);
            }

            phoneNumber = message;
        }

        public override void AcceptString(SendMessageButton button, string message)
        {
            StartCoroutine(CallPhoneWordByWord(message));
            button.canSelect = false;
        }
    }
}