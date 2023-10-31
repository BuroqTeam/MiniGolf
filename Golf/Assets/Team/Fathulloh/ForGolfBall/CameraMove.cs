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
        [SerializeField] private GolfBall _golfBall;
        [SerializeField] private Camera _mainCamera;

        private Vector3 _previousPosition;
        //private Vector3 _offset;
        private float _initialFieldOfView;

        private readonly float _fieldDurration = 0.5f;

        private void Start()
        {
            _initialFieldOfView = _mainCamera.fieldOfView;
            SetCamerePosition();
            //_offset = transform.position - BallTransform.position;
            //transform.GetChild(0).transform.position = Vector3.Lerp(transform.position, BallTransform.position, 0.8f);
        }


        // Update is called once per frame
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
            }

            if (Input.GetMouseButton(0))
            {
                if (!_golfBall.IsBallClicked)
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

                    //lastPos = gameObject.transform.position;
                }
            }
        }



        bool _IsSimpleCameraMove = true;
        bool _isFinish = false;
        void SetCamerePosition()
        {
            if (_golfBall.IsBallMoving || (_mainCamera.fieldOfView == _initialFieldOfView))
            {
                transform.position = BallTransform.position;
                transform.Translate(new Vector3(0, 0.15f, -0.85f));
                _isFinish = true;
                //Debug.Log(1);
            }
            else if (!_golfBall.IsBallClicked && !_golfBall.IsBallMoving && _isFinish /*_mainCamera.fieldOfView != _initialFieldOfView*/)
            {
                _mainCamera.DOFieldOfView(_initialFieldOfView, _fieldDurration);
                _isFinish = false;
                //Debug.Log(2);
            }
            else
                _isFinish= false;
            //transform.position = BallTransform.position;
            //transform.Translate(new Vector3(0, 0.15f, -0.85f));

            //if (_golfBall._IsBallOut && _IsSimpleCameraMove)
            //{
            //    //Ball.GetComponent<Ball>()._IsBallOut = false;
            //    _IsSimpleCameraMove = false;
            //    StartCoroutine(CameraMove());
            //}
            //else if (_IsSimpleCameraMove)
            //{
            //    transform.position = _golfBall.position;
            //    transform.Translate(new Vector3(0, 0.15f, -0.85f));
            //}            
        }

        //IEnumerator CameraMove() // Ball tashqariga chiqib ketganda camerani avvalgi pozitsiyasiga qaytaradigan metod.
        //{
        //    transform.DOMove(lastPos, 1);
        //    gameObject.transform.GetChild(0).gameObject.SetActive(false);

        //    yield return new WaitForSeconds(1.2f);          // Buyerdagi vaqt Ball.cs ChangeCameraFields() metodini ichidagi uchinchi wait for second vaqti bilan teng bo'lsin.
        //    Ball.GetComponent<Ball>()._IsBallOut = false;
        //    gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //    _IsSimpleCameraMove = true;
        //}

        private float timer = 0.0f;
        private float waitTime = 3.0f;
        void LetCameraMove()
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                Debug.Log("Waited for 3 seconds.");
                transform.position = BallTransform.position;
                transform.Translate(new Vector3(0, 0.15f, -0.85f));
                _isFinish = true;
            }
            
        }

    }
}
