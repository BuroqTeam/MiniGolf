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

        private TrailRenderer _trailRenderer;
        private Rigidbody _rigidBody;
        private float _initialFieldView;
        private Vector3 _previousClickPosition = new Vector3();
        private float _movementThreshold = 0.01f;
        private LineDrawer _colorfulLine;

        public Vector3 InitialPosBeforeHit;

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
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
                        InitialPosBeforeHit = gameObject.transform.position;// F++
                        //Debug.Log("gameObject.transform.position = " + gameObject.transform.position);
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


        public void AddForceOpposite()
        {
            //_rigidBody.AddForce(-_rigidBody.velocity, ForceMode.Impulse);
            StartCoroutine(SampleRoutine());
        }


        IEnumerator SampleRoutine()
        {
            float previousDrag = _rigidBody.drag;
            Vector3 previousVel = _rigidBody.velocity;
            _rigidBody.drag = 5000;

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            //Debug.Log(" +  " + previousVel + " " + _rigidBody.angularVelocity);
            
            _rigidBody.drag = previousDrag;
            _rigidBody.AddForce(previousVel, ForceMode.Impulse);
            //_rigidBody.AddForce(-previousVel, ForceMode.Impulse);
        }

        public bool _IsResetBall = true;
        public void ResetBall()
        {
            //StartCoroutine(ResetBallWithDelay());
            if (_IsResetBall)
            {
                _IsResetBall = false;
                StartCoroutine(ResetBallWithDelay());
            }

        }


        IEnumerator ResetBallWithDelay()
        {
            float delaySeconds = 0.4f;
            //yield return new WaitForSeconds(delaySeconds / 2);
            //_rigidBody.isKinematic = true;
            _trailRenderer.enabled = false;

            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;

            yield return new WaitForSeconds(delaySeconds);
            transform.position = InitialPosBeforeHit;

            _trailRenderer.enabled = true;
            //_rigidBody.isKinematic = false;
            _IsResetBall = true;

            if (InitialPosBeforeHit != transform.position)
            {
                Debug.Log("Ishlamadi!");
            }
            else
            {
                Debug.Log(" == Ishladi! == " + transform.position);
            }
        }


    }

}

