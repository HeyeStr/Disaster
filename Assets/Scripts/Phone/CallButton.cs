using System.Collections;
using Common;
using EventSo;
using Manager;
using UnityEngine;

namespace Phone
{
    public class CallButton : MonoBehaviour {
        
        public PlayerInputManager playerInput;

        public string targetTag;
        
        public TransmitMessageEventSo dialogueEvent;
        
        public PhoneController phoneController;
        
        public float callWaitTime;
        private void Update()
        {
            if (playerInput.ClickMouse)
            {
                RaycastHit2D hit = Physics2D.Raycast(playerInput.MousePosition, Vector2.zero);
                if (hit.collider && !string.IsNullOrEmpty(targetTag) && hit.collider.CompareTag(targetTag))
                {
                    StartCoroutine(StartDialogue());
                }
            }

            IEnumerator StartDialogue()
            {
                yield return new WaitForSeconds(callWaitTime);
                dialogueEvent.EventRise(phoneController.assistanceMessage);
            }
        }
    }
}