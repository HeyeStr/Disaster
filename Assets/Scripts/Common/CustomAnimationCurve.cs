using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "CustomAnimationCurve")]
    public class CustomAnimationCurve : ScriptableObject
    {
        [Header("动画曲线")]
        public AnimationCurve animationCurve;
    }
}