using UnityEngine;
using UnityEngine.Events;

namespace EventSo
{
    [CreateAssetMenu(menuName = "Events/MessageEvent")]
    public class SendMessageEventSo : ScriptableObject
    {
        public UnityAction<string> Event;

        public void EventRise(string message)
        {
            Event?.Invoke(message);
        }
    }
}