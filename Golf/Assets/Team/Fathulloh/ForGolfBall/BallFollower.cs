using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class BallFollower : MonoBehaviour
    {
        [SerializeField] private GolfBall _golfBall;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _ballTransform;
        private Vector3 StandartOffset = new(0, 0.15f, -0.85f);

        private Vector3 _previousPosition;

        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))  // It work when dragging
            {
                _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
                //Debug.Log("First");
            }

            if (Input.GetMouseButton(0))      // It work for rotation change
            {
                if (!_golfBall.IsBallClicked)
                {
                    //Debug.Log("Second");
                    Vector3 direction = _previousPosition - _mainCamera.ScreenToViewportPoint(Input.mousePosition);

                    transform.position = _ballTransform.position;

                    // Rotate in Vertical Axis
                    transform.Rotate(Vector3.right, direction.y * 180);
                    float verticalAngle = transform.rotation.eulerAngles.x;
                    verticalAngle = Mathf.Clamp(verticalAngle, 5, 30);

                    // Rotate in Horizontal Axis
                    transform.Rotate(Vector3.up, direction.x * -180, Space.World);
                    float horizontalAngle = transform.rotation.eulerAngles.y;

                    transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

                    // Move to near Ball
                    //SetFollowerPos();
                    _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
                }
            }

            SetFollowerPos();
        }


        void SetFollowerPos()
        {
            transform.position = _ballTransform.transform.position;
            transform.Translate(StandartOffset);
        }

    }
}
