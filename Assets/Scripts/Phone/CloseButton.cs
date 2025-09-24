using UnityEngine;

namespace Phone
{
    public class CloseButton : MonoBehaviour
    {
        public PhoneController PhoneController;
        
        public SlideMoveComponent slideMove;

        private void Awake()
        {
            PhoneController = GetComponentInParent<PhoneController>();
        }

        public void OnMouseDown()
        {
            if (!slideMove.isSliding)
            {
                PhoneController.numberLabel.text = "";
                slideMove.SlideHide();
                gameObject.SetActive(false);
            }
        }
    }
}