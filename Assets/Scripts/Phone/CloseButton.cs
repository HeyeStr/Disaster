using UnityEngine;

namespace Phone
{
    public class CloseButton : MonoBehaviour
    {
        public PhoneController phoneController;
        
        public SlideMoveComponent slideMove;

        private void Awake()
        {
            phoneController = GetComponentInParent<PhoneController>();
        }

        private void OnMouseDown()
        {
            ClosePhone();
        }

        public void ClosePhone()
        {
            if (!slideMove.isSliding)
            {
                phoneController.numberLabel.text = "";
                slideMove.startClose = true;
                slideMove.SlideHide();
                gameObject.SetActive(false);
            }
        }
    }
}