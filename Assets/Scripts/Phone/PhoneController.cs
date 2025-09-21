using System;
using System.Collections;
using Common;
using EventSo;
using TMPro;
using UnityEngine;

namespace Phone
{
    public class PhoneController : MonoBehaviour
    {
        [Header("事件引用")]
        public TransmitMessageEventSo callEvent;

        [Header("UI引用")]
        public TMP_Text numberLabel;

        [Header("时间设置")]
        public float durTime;
        
        public AssistanceMessage assistanceMessage;

        private void Awake()
        {
            numberLabel.text = "";
        }

        private void OnEnable()
        {
            callEvent.Event += CallPhone;
        }

        private void OnDisable()
        {
            callEvent.Event -= CallPhone;
        }

        private void CallPhone(AssistanceMessage message)
        {
            assistanceMessage = message;
            Debug.Log(message.phoneNumber);
            StartCoroutine(CallPhoneWordByWord(message));
        }

        IEnumerator CallPhoneWordByWord(AssistanceMessage message)
        {
            numberLabel.text = "";
            foreach (var num in message.phoneNumber)
            {
                Debug.Log(num);
                numberLabel.text += num;
                yield return new WaitForSeconds(durTime);
            }
        }
    }
}