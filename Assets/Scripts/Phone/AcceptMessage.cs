using Common;
using UnityEngine;

namespace Phone
{
    public abstract class AcceptMessage : MonoBehaviour
    {
        public virtual void AcceptString(SendMessageButton button,string message)
        {
        }
    }
}