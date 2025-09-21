using Common;
using UnityEngine;
using UnityEngine.Events;

namespace EventSo
{
    [CreateAssetMenu(menuName = "Events/MessageEvent")]
    public class TransmitMessageEventSo : ScriptableObject
    {
        public UnityAction<AssistanceMessage> Event;

        public void EventRise(AssistanceMessage message)
        {
            Event.Invoke(message);
        }
    }
}