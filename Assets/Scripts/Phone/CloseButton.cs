using Manager;
using UnityEngine;

namespace Phone
{
    public class CloseButton : MonoBehaviour
    {
        public SlideMoveComponent slideMove;
        
        public PlayerInputManager playerInput;

        public string targetTag;

        private void Update()
        {
            if (!slideMove.isSliding && playerInput.ClickMouse)
            {
                RaycastHit2D hit = Physics2D.Raycast(playerInput.MousePosition, Vector2.zero);
                if (hit.collider && !string.IsNullOrEmpty(targetTag) && hit.collider.CompareTag(targetTag))
                {
                    slideMove.SlideHide();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}