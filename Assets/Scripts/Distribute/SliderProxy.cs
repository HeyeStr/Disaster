using UnityEngine;
using UnityEngine.UI;

namespace Distribute
{
    public class SliderProxy : MonoBehaviour
    {
        public Slider sliderComponent;

        private void Awake()
        {
            sliderComponent = GetComponent<Slider>();
        }

        public int GetValue()
        {
            return (int)sliderComponent.value;
        }
    }
}