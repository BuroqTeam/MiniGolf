using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class ScrollControll : MonoBehaviour
    {
        public Camera MainCamera;
        //[SerializeField] private GolfBall _golfBall;
        [SerializeField] private BallMovement _ballMove;
        private float _initialFieldView;
        [HideInInspector] public float MinFielOfView = 60;
        [HideInInspector] public float MaxFieldOfView = 85;

        void Start()
        {
            _initialFieldView = MainCamera.fieldOfView;
        }

        
        void Update()
        {
            Vector3 scrollDelta = Input.mouseScrollDelta;

            if (!_ballMove.IsBallClicked)
            {
                if (scrollDelta != Vector3.zero)
                {
                    if (scrollDelta.y > 0) // Scroll up
                    {
                        if (_initialFieldView >= 67.66f)
                        {
                            _initialFieldView -= 7.66f;
                            MainCamera.DOFieldOfView(_initialFieldView, 0.7f);
                            Debug.Log("Decrease.");
                        }                        
                    }
                    else if (scrollDelta.y < 0) // Scroll down
                    {
                        if (_initialFieldView <= 75.34f)
                        {
                            _initialFieldView += 7.66f;
                            MainCamera.DOFieldOfView(_initialFieldView, 0.7f);
                            Debug.Log("Increase.");
                        }
                    }
                }
            }
        }

        
    }
}
