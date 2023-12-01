using DG.Tweening;
using MiniGolf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class CameraMove : MonoBehaviour
    {
        public Transform BallTransform;
        public Transform FinishTransform;
        [SerializeField] private BallMovement _ballMove;
        [SerializeField] private Camera _mainCamera;

        private Vector3 _previousPosition;
        //private Vector3 _offset;
        private float _initialFieldOfView;
        private Vector3 _standartDistance = new Vector3(0, 0.15f, -0.85f);

        private readonly float _fieldDurration = 0.5f;
        private Vector3 _lastPos;

        private void Start()
        {
            _initialFieldOfView = _mainCamera.fieldOfView;
            SetCamerePosition();
            //_offset = transform.position - BallTransform.position;
            transform.GetChild(0).transform.position = Vector3.Lerp(transform.position, BallTransform.position, 0.8f);
        }

                
        void LateUpdate()
        {
            CameraMovement();
        }


        void CameraMovement()
        {
            SetCamerePosition();

            if (Input.GetMouseButtonDown(0))
            {
                _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);                
                _lastPos = gameObject.transform.position;
                //Debug.Log(" _previousPosition = " + _previousPosition + " gameObject.transform.position = " + gameObject.transform.position);
            }

            if (Input.GetMouseButton(0))
            {
                if (!_ballMove.IsBallClicked && (!_ballMove.IsBallOut) && (_isCameraCanMove))
                {
                    Vector3 direction = _previousPosition - _mainCamera.ScreenToViewportPoint(Input.mousePosition);

                    transform.position = BallTransform.position;
                    
                    // Rotate in Vertical Axis
                    transform.Rotate(Vector3.right, direction.y * 180);
                    float verticalAngle = transform.rotation.eulerAngles.x;
                    verticalAngle = Mathf.Clamp(verticalAngle, 5, 30);

                    // Rotate in Horizontal Axis
                    transform.Rotate(Vector3.up, direction.x * -180, Space.World);
                    float horizontalAngle = transform.rotation.eulerAngles.y;

                    //transform.eulerAngles = new Vector3(verticalAngle, horizontalAngle, 0);
                    transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

                    // Move to near Ball
                    SetCamerePosition();
                    _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);                    
                }
            }
        }


        //bool _IsSimpleCameraMove = true;
        bool _isCameraCanMove = true;    // Cameraning fieldi o'zgarayotgan vaqtda Camera rotationini o'zgartirmaslikni taminlaydi. 
        bool _isFinish = false;
        bool _isReturning = true;

        void SetCamerePosition()
        {
            if (_ballMove.IsBallOut)
            {
                if (_isReturning)
                {
                    _isReturning = false;
                    StartCoroutine(ReturnInitialPos());
                }
            }
            else if (_ballMove.IsBallMoving || (_mainCamera.fieldOfView == _initialFieldOfView))
            {
                transform.position = BallTransform.position;
                transform.Translate(_standartDistance);
                _isFinish = true;
                //Debug.Log(1);
            }
            else if (!_ballMove.IsBallClicked && !_ballMove.IsBallMoving && _isFinish /*_mainCamera.fieldOfView != _initialFieldOfView*/)
            {
                _mainCamera.DOFieldOfView(_initialFieldOfView, _fieldDurration);
                _isFinish = false;
                _isCameraCanMove = false;
                StartCoroutine(LetCameraMove());
                //Debug.Log(2);
            }
            else
                _isFinish= false;

        }


        IEnumerator ReturnInitialPos() // Ball tashqariga chiqib ketganda camerani avvalgi pozitsiyasiga qaytaradigan metod.
        {
            transform.DOMove(_lastPos, 1);
            //gameObject.transform.GetChild(0).gameObject.SetActive(false);

            yield return new WaitForSeconds(1.2f);          // Buyerdagi vaqt Ball.cs ChangeCameraFields() metodini ichidagi uchinchi wait for second vaqti bilan teng bo'lsin.            
            _ballMove.IsBallOut = false;
            //gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //_IsSimpleCameraMove = true;
            _isReturning = true;
            Debug.Log("Return Initial Pos");
        }


        IEnumerator LetCameraMove()
        {
            yield return new WaitForSeconds(_fieldDurration);
            _isCameraCanMove = true;
        }

    }
}
