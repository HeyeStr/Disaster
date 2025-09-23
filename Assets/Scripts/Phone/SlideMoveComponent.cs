using System.Collections;
using Common;
using UnityEngine;

namespace Phone
{
    public class SlideMoveComponent : MonoBehaviour
    {
        [SerializeField] private Vector3 hidePosition;

        [SerializeField] private Vector3 showPosition;

        [SerializeField] private string targetTag;

        [SerializeField] private float maxTime;
        
        public float timer;

        public bool isSliding;

        public GameObject backButton;

        public bool buttonClose;

        [Header("动画曲线")] [SerializeField] public CustomAnimationCurve slideCurve;

        private void OnEnable()
        {
            buttonClose = true;
            isSliding = false;
            backButton.SetActive(false);
            transform.position = hidePosition;
        }
        
        private void OnMouseDown()
        {
            if (!isSliding && buttonClose)
                StartCoroutine(SlideMove(buttonClose, hidePosition, showPosition));
        }

        // maxTime时间实现pos1到pos2平滑移动
        IEnumerator SlideMove(bool finalState, Vector3 start, Vector3 target)
        {
            isSliding = true;
            timer = 0;
            while (timer < maxTime)
            {
                timer += Time.deltaTime;
                float t = slideCurve.animationCurve.Evaluate(timer / maxTime);
                transform.position = Vector3.Lerp(start, target, t);
                yield return null;
            }

            isSliding = false;
            buttonClose = !buttonClose;
            if (finalState)
            {
                ActiveButton();
            }
        }

        // 提供外部方法实现平滑隐藏
        public void SlideHide()
        {
            StartCoroutine(SlideMove(false, showPosition, hidePosition));
        }

        private void ActiveButton()
        {
            backButton?.SetActive(true);
        }
    }
}