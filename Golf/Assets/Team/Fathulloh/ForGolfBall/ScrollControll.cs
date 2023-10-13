using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class ScrollControll : MonoBehaviour
    {
        public Camera MainCamera;
        [SerializeField] private GolfBall _golfBall;
        private float _initialFieldView;


        void Start()
        {
            _initialFieldView = MainCamera.fieldOfView;
        }

        
        void Update()
        {
            Vector3 scrollDelta = Input.mouseScrollDelta;

            if (!_golfBall.IsBallClicked)
            {
                if (scrollDelta != Vector3.zero)
                {
                    if (scrollDelta.y > 0) // Scroll up
                    {
                        if (_initialFieldView >= 67.66f)
                        {
                            _initialFieldView -= 7.66f;
                            MainCamera.DOFieldOfView(_initialFieldView, 0.7f);
                        }                        
                    }
                    else if (scrollDelta.y < 0) // Scroll down
                    {
                        if (_initialFieldView <= 75.34f)
                        {
                            _initialFieldView += 7.66f;
                            MainCamera.DOFieldOfView(_initialFieldView, 0.7f);
                        }
                    }
                }
            }
        }

        void someMethod()
        {
            //Vector2 scrollDelta = Input.mouseScrollDelta;
            //if (!BallObj.IsBallClicked)
            //{
            //    // Check if there was any scroll input
            //    if (scrollDelta != Vector2.zero)
            //    {
            //        // Scroll up
            //        if (scrollDelta.y > 0)
            //        {
            //            if (_initialFieldView >= 67.66f)
            //            {
            //                _initialFieldView -= 7.66f;
            //                MainCamera.DOFieldOfView(_initialFieldView, 0.7f);
            //            }
            //        }
            //        // Scroll down
            //        else if (scrollDelta.y < 0)
            //        {
            //            if (_initialFieldView <= 75.34f)
            //            {
            //                _initialFieldView += 7.66f;
            //                MainCamera.DOFieldOfView(_initialFieldView, 0.7f);
            //            }
            //        }
            //    }
            //}
        }


    }
}
