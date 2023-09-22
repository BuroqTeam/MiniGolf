using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace MiniGolf
{
    public class CameraController : MonoBehaviour
    {

        public Transform Ball;

        private Vector3 _offset;
        private Vector3 _previousPosition;
        private Camera _mainCamera;

        Vector3 lastPos;

        private void Awake()
        {
            _mainCamera = GetComponent<Camera>();
        }

        private void Start()
        {
            SetCamerePosition();
            _offset = transform.position - Ball.transform.position;
            transform.GetChild(0).transform.position = Vector3.Lerp(transform.position, Ball.position, 0.8f);
        }


        // Update is called once per frame
        void Update()
        {
            SetCamerePosition();

            if (Input.GetMouseButtonDown(0))
            {
                _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                if (!Ball.GetComponent<Ball>().IsBallClicked)
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

                    //transform.eulerAngles = new Vector3(verticalAngle, horizontalAngle, 0);
                    transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

                    // Move to near Ball
                    SetCamerePosition();
                    _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);

                    lastPos = gameObject.transform.position;
                }
            }

        }


        bool _IsSimpleCameraMove = true;

        void SetCamerePosition()
        {
            if (Ball.GetComponent<Ball>()._IsBallOut && _IsSimpleCameraMove)
            {
                //Ball.GetComponent<Ball>()._IsBallOut = false;
                _IsSimpleCameraMove = false;
                StartCoroutine(CameraMove());
            }
            else if (_IsSimpleCameraMove)
            {
                transform.position = Ball.position;
                transform.Translate(new Vector3(0, 0.15f, -0.85f));
            }
            
            //ransform.position = Ball.position;
            //transform.Translate(new Vector3(0, 0.15f, -0.85f));
        }


        IEnumerator CameraMove() // Ball tashqariga chiqib ketganda camerani avvalgi pozitsiyasiga qaytaradigan metod.
        {
            transform.DOMove(lastPos, 1);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            
            yield return new WaitForSeconds(1.2f);          // Buyerdagi vaqt Ball.cs ChangeCameraFields() metodini ichidagi uchinchi wait for second vaqti bilan teng bo'lsin.
            Ball.GetComponent<Ball>()._IsBallOut = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            _IsSimpleCameraMove = true;
        }


    }

}

