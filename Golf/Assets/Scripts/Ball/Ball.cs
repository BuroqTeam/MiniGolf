using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace MiniGolf
{
    public class Ball : MonoBehaviour
    {
        public GameEvent BallHitSO;
        public Camera MainCamera;
        public bool IsBallClicked;
        public bool IsBallMoving;
        public Color GreenColor;
        public Color YellowColor;
        public Color RedColor;


        private Rigidbody _rigidBody;
        private float _initialFieldView;
        private Vector3 _previousClickPosition = new Vector3();
        private float _movementThreshold = 0.01f;
        private LineDrawer _colorfulLine;

        private void Awake()
        {
            _colorfulLine = transform.GetChild(0).gameObject.GetComponent<LineDrawer>();//F++
            _rigidBody = GetComponent<Rigidbody>();
            _initialFieldView = MainCamera.fieldOfView;
            transform.position = new Vector3(0, GetComponent<Renderer>().bounds.size.y * 0.5f, 0);
        }


        void Update()
        {
            // Check for touch input
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Cast a ray from the camera to the touch position
                Ray ray = MainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                // Check if the ray hits a GameObject
                if (Physics.Raycast(ray, out hit))
                {
                    // The hit.collider.gameObject is the GameObject that was touched
                    GameObject touchedObject = hit.collider.gameObject;

                    if (touchedObject.name.Equals("Ball"))
                    {
                        IsBallClicked = true;
                    }
                }
            }

            // Check for mouse click
            if (Input.GetMouseButtonDown(0))
            {
                // Cast a ray from the camera to the mouse position
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Check if the ray hits a GameObject
                if (Physics.Raycast(ray, out hit))
                {
                    // The hit.collider.gameObject is the GameObject that was clicked
                    GameObject clickedObject = hit.collider.gameObject;


                    if (clickedObject.name.Equals("Ball") && !IsBallMoving)
                    {
                        IsBallClicked = true;
                        _previousClickPosition = MainCamera.ScreenToViewportPoint(Input.mousePosition);
                    }
                    else
                    {
                        IsBallClicked = false;
                    }
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (IsBallClicked && !IsBallMoving)
                {
                    float distance = Vector3.Distance(_previousClickPosition, MainCamera.ScreenToViewportPoint(Input.mousePosition));
                    distance = distance * 100;

                    // shu yerga arrow color change yoziladi

                    if (distance < 9)
                    {
                        _colorfulLine.ChangeLineColor(GreenColor);
                    }
                    else if (distance < 17)
                    {
                        _colorfulLine.ChangeLineColor(YellowColor);
                    }
                    else if (distance < 23)
                    {
                        _colorfulLine.ChangeLineColor(RedColor);
                    }

                    if (distance > 23)
                    {
                        distance = 23;
                    }

                    MainCamera.fieldOfView = Mathf.MoveTowards(MainCamera.fieldOfView, _initialFieldView + distance, 20 * Time.deltaTime);

                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (IsBallClicked && !IsBallMoving)
                {
                    MainCamera.DOFieldOfView(_initialFieldView, 0.25f);
                    IsBallClicked = false;
                }
            }
            SetBallMove();
        }


        void SetBallMove()
        {
            // Get the velocity of the GameObject
            Vector3 velocity = _rigidBody.velocity;

            // Calculate the speed by taking the magnitude of the velocity
            float speed = velocity.magnitude;

            // Check if the speed exceeds the movement threshold
            if (speed > _movementThreshold)
            {
                // The GameObject is considered to be moving

                IsBallMoving = true;
            }
            else
            {
                // The GameObject is not moving

                IsBallMoving = false;
            }
        }







    }

}
