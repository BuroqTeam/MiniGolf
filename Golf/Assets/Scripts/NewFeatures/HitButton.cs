using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

namespace MiniGolf
{
    public class HitButton : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private SliderControl _sliderControl;
        [SerializeField] private FrontArrow _frontArrow;

        void Start()
        {

        }


        public void ClickHitButton()
        {
            //Debug.Log(_sliderControl._IsActive);
            //Debug.Log("_slider.value = " + _slider.value);
            if (_sliderControl._IsActive)
            {
                _frontArrow.AddingForceToBall(_slider.value);
                _sliderControl._IsActive = false;
            }
            else if (!_sliderControl._IsActive)
            {
                _sliderControl._IsActive = true;
            }
        }


    }
}
