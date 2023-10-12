using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth //F++
{
    public class CameraControl : MonoBehaviour
    {
        public Transform Ball;
        public Camera MainCamera;

        private Vector3 _previousPosition;

        void Start()
        {

        }

        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _previousPosition = MainCamera.ScreenToViewportPoint(Input.mousePosition);
            }



        }


    }
}
