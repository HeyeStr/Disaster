using UnityEngine;
using UnityEngine.Events;

namespace EventSo
{
    [CreateAssetMenu(menuName = "Events/VoidEvent")]
    public class VoidEventSo : ScriptableObject
    {
        public UnityAction Event;

        public void EventRise()
        {
            Event?.Invoke();
        }
    }
}