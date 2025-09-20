using PlayerInput;
using UnityEngine;

namespace Common
{
    public class MoveToTargetPos : MonoBehaviour
    {
        [SerializeField] private Vector3 startPosition;
        
        [SerializeField] private Vector3 targetPosition;
        
        [SerializeField] private float duration;
        
        [SerializeField] private string targetTag;
        
        public PlayerInputManager playerInput;

        // Update is called once per frame
        void Update()
        {
            ClickMoveObject();
        }

        private void ClickMoveObject()
        {
            if (playerInput.ClickMouse)
            {
                RaycastHit2D hit = Physics2D.Raycast(playerInput.MousePosition, Vector2.zero);
                if (hit.collider && !string.IsNullOrEmpty(targetTag) && hit.collider.CompareTag(targetTag))
                {
                    transform.position = Vector3.Lerp(startPosition, targetPosition, duration * Time.deltaTime);
                }
            }
        }
    }
}
