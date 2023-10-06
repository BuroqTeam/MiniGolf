using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

namespace MiniGolf
{
    /// <summary>
    /// New Featuresdagi Hit nomli button uchun maxsus script.
    /// </summary>
    public class HitButton : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private SliderControl _sliderControl;
        [SerializeField] private FrontArrow _frontArrow;
        [SerializeField] private Button _hitButton;
        [SerializeField] private Ball _ball;

        bool _isFirstCliked = false;
        [HideInInspector] public bool _isInteractable;

        public void ClickHitButton()
        {
            if (_sliderControl._IsActive)
            {
                _frontArrow.AddingForceToBall(_slider.value);
                _sliderControl._IsActive = false;
                _isInteractable = false;
                _hitButton.interactable = false;

                StartCoroutine(ChangeClicked());
                _ball.IncrementHitScore();                
            }
            else if (!_sliderControl._IsActive)
            {
                _isFirstCliked = true;
                _sliderControl._IsActive = true;
            }
        }


        public void HitButtonInteractable(bool _isTrue)
        {
            if (!_isFirstCliked)
            {
                _isInteractable = _isTrue;
                _hitButton.interactable = _isTrue;
            }            
        }


        IEnumerator ChangeClicked()
        {
            yield return new WaitForSeconds(2f);
            _isFirstCliked = false;
            _sliderControl.SetInitialData();
        }

    }
}
