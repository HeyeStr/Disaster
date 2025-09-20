using UnityEngine;

namespace Manager
{
    public class PlayerInputManager : MonoBehaviour
    {
        public Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        public bool ClickMouse => Input.GetMouseButtonDown(0);

        public bool ClickDialogue => Input.GetKeyDown(KeyCode.Space);
    }
}