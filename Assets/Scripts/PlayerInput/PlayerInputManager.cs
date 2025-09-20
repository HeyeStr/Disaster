using UnityEngine;

namespace PlayerInput
{
    public class PlayerInputManager : MonoBehaviour
    {
        public Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        public bool ClickMouse => Input.GetMouseButtonDown(0);
    }
}