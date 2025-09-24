using System.Collections;
using UnityEngine;

namespace Phone
{
    public class CallButton : MonoBehaviour
    {
        public PhoneController phoneController;

        public float callWaitTime;

        private void Awake()
        {
            phoneController = GetComponentInParent<PhoneController>();
        }

        public void OnMouseDown()
        {
            if (string.IsNullOrEmpty(phoneController.phoneNumber))
                return;
            // TODO:开始播放拨号音效
            phoneController.audioSource.Play();
            StartCoroutine(StartDialogue());
        }

        IEnumerator StartDialogue()
        {
            Debug.Log("开始事件");
            yield return new WaitForSeconds(callWaitTime);
            // TODO: 结束拨号音效
            phoneController.audioSource.Stop();
            if (!phoneController.dialogueManager.isDialogue)
                phoneController.dialogueManager.InitDialogue(phoneController.phoneNumber);
        }
    }
}