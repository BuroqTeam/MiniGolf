using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth //F++
{
    public class CameraControll : MonoBehaviour
    {
        [SerializeField] private GolfBall _golfBall;
        [SerializeField] private Camera _mainCamera;

        private Vector3 _previousPosition; // initialPosition

        public Transform Ball;
        private float smoothSpeed = 0.125f;
        private Vector3 offset;


        void Start()
        {
            offset = transform.position - Ball.position;

        }

        
        private void LateUpdate()
        {
            //Way1();

            Way2();
        }


        void Way1()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                if (!_golfBall.IsBallClicked)
                {
                    Vector3 direction = _previousPosition - _mainCamera.ScreenToViewportPoint(Input.mousePosition);
                    transform.position = Ball.position;

                    // Rotate in Vertical Axis
                    transform.Rotate(Vector3.right, direction.y * 180);
                    float verticalAngle = transform.rotation.eulerAngles.x;
                    verticalAngle = Mathf.Clamp(verticalAngle, 5, 30);

                    // Rotate in Horizontal Axis
                    transform.Rotate(Vector3.up, direction.x * -180, Space.World);
                    float horizontalAngle = transform.rotation.eulerAngles.y;

                    transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

                    _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
                }
            }

            SetCamera();
        }


        void Way2()
        {
            Vector3 desiredPosition = Ball.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = new Vector3(smoothedPosition.x, /*smoothedPosition.y +*/ transform.position.y, smoothedPosition.z);
            //transform.LookAt(Ball);
            //transform.Translate(new Vector3(0, 0.15f, -0.85f));
        }


        public void SetCamera()
        {
            transform.position = Ball.position;
            transform.Translate(new Vector3(0, 0.15f, -0.85f));
        }


    }
}
