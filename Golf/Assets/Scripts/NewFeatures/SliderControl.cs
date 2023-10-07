using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MiniGolf
{
    /// <summary>
    /// Koptok tezligini ko'rsatuvchi slider.
    /// </summary>
    public class SliderControl : MonoBehaviour
    {
        public Slider CurrentSlider;
        [SerializeField] private Image sliderImage;
        [SerializeField] private TMP_Text sliderText;
        private readonly float maxSliderAmount = 100.0f;

        Color redColor = new (1, 0.17f, 0.05f);
        Color yellowColor = new (1, 0.97f, 0.17f);
        Color greenColor = new (0.25f, 1, 0.27f);
        
        [HideInInspector] public bool _IsActive = false;
        [SerializeField] private float _speed;
        int percentNumber;

        

        private void Update()
        {
            if (_IsActive)
            {
                SliderChange();
            }
        }
        

        private void SliderChange()
        {
            if (CurrentSlider.value <= 0)
            {
                //Debug.Log(addingNum + " + + ");
                _speed = Mathf.Abs(_speed);
            }
            else if (CurrentSlider.value >= 1)
            {
                //Debug.Log(addingNum + " - - ");
                _speed = (-1) * _speed;                
            }

            CurrentSlider.value += (float)_speed / 100;
            //CurrentSlider.value = Mathf.Lerp(0, 1, Time.deltaTime * 1f);
            float localValue = CurrentSlider.value;
            percentNumber = (int)(maxSliderAmount * localValue);
            sliderText.text = percentNumber.ToString();

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


        public void SetInitialData()
        {
            CurrentSlider.value = 0;
            sliderText.text = "0";
            sliderText.color = Color.white;
        }


    }
}
