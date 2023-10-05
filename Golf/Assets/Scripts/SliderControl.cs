using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MiniGolf
{
    public class SliderControl : MonoBehaviour
    {
        public Slider CurrentSlider;
        [SerializeField] private Image sliderImage;
        [SerializeField] private TMP_Text sliderText;
        private float maxSliderAmount = 100.0f;

        Color redColor = new (1, 0.17f, 0.05f);
        Color yellowColor = new (1, 0.97f, 0.17f);
        Color greenColor = new (0.25f, 1, 0.27f);

        
        [HideInInspector] public bool _IsActive = false;
        int addingNum = 1;

        private void Update()
        {
            if (_IsActive)
            {
                SliderChange();
            }
        }
                

        private void SliderChange()
        {
            if (CurrentSlider.value == 0)
            {
                Debug.Log(1);
                addingNum = 2;
            }
            else if (CurrentSlider.value == 1)
            {
                addingNum = -2;
                Debug.Log(-1);
            }

            CurrentSlider.value += (float)addingNum / 100;
            float localValue = CurrentSlider.value;
            sliderText.text = ((int)(maxSliderAmount * localValue)).ToString() + "%";

            if (localValue <= 0.42f)
            {
                sliderText.color = greenColor;
                sliderImage.color = greenColor;
            }
            else if (localValue <= 0.70f)
            {
                sliderText.color = yellowColor;
                sliderImage.color = yellowColor;
            }
            else if (localValue > 0.70f)
            {
                sliderText.color = redColor;
                sliderImage.color = redColor;
            }
        }

        public void ClickHitButton()
        {
            if (_IsActive)
            {

                _IsActive = false;
            }
            else
            {
                _IsActive = true;
            }
        }

        //public void SliderChange2(float value)
        //{
        //    float localValue = value * maxSliderAmount;
        //    sliderText.text = localValue.ToString("0") + "%";

        //    if (localValue<=45)
        //    {
        //        sliderText.color = greenColor;
        //        sliderImage.color = greenColor;
        //    }
        //    else if (localValue <= 70)
        //    {
        //        sliderText.color = yellowColor;
        //        sliderImage.color = yellowColor;
        //    }
        //    else if (localValue > 70)
        //    {
        //        sliderText.color = redColor;
        //        sliderImage.color = redColor;
        //    }
        //}

    }
}
