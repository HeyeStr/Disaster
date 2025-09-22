using System;
using System.Collections;
using EventSo;
using Manager;
using UnityEngine;

namespace Phone
{
    public class CallButton : MonoBehaviour {
        
        public SendMessageEventSo dialogueEvent;
        
        public PhoneController phoneController;
        
        public float callWaitTime;

        public void OnMouseDown()
        {
            StartCoroutine(StartDialogue());
        }
        
        IEnumerator StartDialogue()
        {
            Debug.Log("开始事件");
            yield return new WaitForSeconds(callWaitTime);
            dialogueEvent.EventRise(phoneController.phoneNumber);
        }
    }
}