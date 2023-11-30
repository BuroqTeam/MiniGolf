using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace GolfBall_Smooth //F++
{
    public class CameraControll : MonoBehaviour // I don't use this.
    {
        //[SerializeField] private GolfBall _golfBall;
        [SerializeField] private BallMovement _ballMove;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LineDrawer _lineDrawer;

        private Vector3 _previousPosition; // initialPosition

        public Transform FinishFlagTransform;
        public Transform GolfBallTransform;
        
        float durration = 1.25f;

        /// <summary>
        /// Camera va ball orasida masofa vectorda. 
        /// </summary>
        [SerializeField] private Vector3 offset;
        private Vector3 _initialPos;
        private bool _isCameraAnimationFinished = true;
        private float _initialFieldView;

        public float DistanceCameraAndBall;
        Vector3 standartOffset = new(0, 0.15f, -0.85f);
        public GameObject BallFollower;

        
        [Range(0, 100)]
        public float variableInRange;
        Vector3 MovingOffset = new(0, 0.18f, -1);


        void Start()
        {            
            DistanceCameraAndBall = Vector3.Distance(transform.position, GolfBallTransform.position);
            _initialFieldView = _mainCamera.fieldOfView;            
            offset = transform.position - GolfBallTransform.position;

            //StartCoroutine(CameraStartAnimation());
        }
        

        IEnumerator CameraStartAnimation()
        {
            Vector3 newPos = FinishFlagTransform.position + offset;
            transform.position = new(newPos.x, 0.72f, newPos.z);
            _mainCamera.fieldOfView = 80;
            yield return new WaitForSeconds(1f);                       

            transform.DOMove(GolfBallTransform.position + standartOffset /*initialPos*/, 1.5f)
                .SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.2f);
            _mainCamera.DOFieldOfView(_initialFieldView, 0.75f)
                        .SetEase(Ease.Linear);
            yield return new WaitForSeconds(1.8f);                        
            
            Debug.Log(" transform.position = " + transform.position + " transform.rotation = " + transform.rotation);
            yield return new WaitForSeconds(1f);
            _isCameraAnimationFinished = true;
            yield return new WaitForSeconds(1f);
            
            Debug.Log(" transform.position = " + transform.position + " transform.rotation = " + transform.rotation);
        }


        private void LateUpdate()
        {
            if (_isCameraAnimationFinished)
            {
                CameraMoveAndRotate();                
            }
        }

        
        void CameraMoveAndRotate()
        {
            //CameraMovement1();  // Another way.
            //CameraMovement2();  // Ball follower object
            //CameraMovement3();  // With Lerp
            CameraMovement4(); // with waiting durration;

            if (Input.GetMouseButtonDown(0))  // It work when dragging
            {                
                DistanceCameraAndBall = Vector3.Distance(BallFollower.transform.position, GolfBallTransform.position);

                _initialPos = transform.position;
                _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
                //Debug.Log("First");
            }

            if (Input.GetMouseButton(0))      // It work for rotation change
            {
                if (!_ballMove.IsBallClicked)
                {
                    //Debug.Log("Second");
                    Vector3 direction = _previousPosition - _mainCamera.ScreenToViewportPoint(Input.mousePosition);

                    transform.position = GolfBallTransform.position;
                    
                    // Rotate in Vertical Axis
                    transform.Rotate(Vector3.right, direction.y * 180);
                    float verticalAngle = transform.rotation.eulerAngles.x;
                    verticalAngle = Mathf.Clamp(verticalAngle, 5, 30);

                    // Rotate in Horizontal Axis
                    transform.Rotate(Vector3.up, direction.x * -180, Space.World);
                    float horizontalAngle = transform.rotation.eulerAngles.y;

                    transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

                    // Move to near Ball
                    SetCamera();
                    _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
                    //lastPos = gameObject.transform.position;
                }
            }            
        }


        void SetCamera()
        {
            transform.position = GolfBallTransform.position;
            transform.Translate(standartOffset);
        }


        bool IsNeedResetOffset = false;
        void CameraMovement1() // Camera move with Lerp and dotween.
        {
            if (_ballMove.IsBallMoving)
            {
                //Debug.Log("Distance is far");
                transform.position = Vector3.Lerp(transform.position, BallFollower.transform.position, 1.5f * Time.deltaTime);

                IsNeedResetOffset = true;
            }
            else if (!_ballMove.IsBallMoving && IsNeedResetOffset && !_ballMove.IsBallOut)
            {
                //Debug.Log("Distance is near");
                IsNeedResetOffset = false;
                transform.DOMove(BallFollower.transform.position, durration);
            }
            else if (_ballMove.IsBallOut)
            {                
                if (transform.position != BallFollower.transform.position) 
                {
                    IsNeedResetOffset = false;
                    _ballMove.IsBallOut = false;
                    transform.DOMove(BallFollower.transform.position, durration);
                }
                Debug.Log(" isBallOut = " + _ballMove.IsBallOut);
            }
            
        }


        bool _isMovingWithNewDistance = false;
        Vector3 distance;
        bool isFirstTime = true;
        void CameraMovement2()
        {
            if (_ballMove.IsBallMoving)
            {
                Vector3 cameraPos = transform.position;
                Vector3 ballPos = GolfBallTransform.position;
                cameraPos = new Vector3(cameraPos.x, 0, cameraPos.z);
                ballPos = new Vector3(ballPos.x, 0, ballPos.z);

                if (Vector3.Distance(cameraPos, ballPos) > 1)
                {
                    _isMovingWithNewDistance = true;
                    //Debug.Log("First If = ");
                }

                if (_isMovingWithNewDistance)
                {
                    if (isFirstTime)
                    {
                        isFirstTime = false;
                        distance = (ballPos - cameraPos).normalized;
                        //Debug.Log(ballPos - cameraPos);
                    }

                    if (distance != Vector3.zero)
                    {
                        transform.Translate(distance);
                        Debug.Log(transform.position);
                    }

                    //transform.position = GolfBallTransform.position;
                    //transform.Translate(MovingOffset);
                    //Debug.Log("Second if");
                }
            }
            else if (!_ballMove.IsBallMoving && _isMovingWithNewDistance)
            {
                _isMovingWithNewDistance = false;
                Vector3 currentPos = transform.position;

                transform.position = GolfBallTransform.position;
                transform.Translate(standartOffset);
                Vector3 translatePos = transform.position;

                transform.position = currentPos;
                transform.DOMove(translatePos, 1f);

                if (GolfBallTransform.position.x != 0 && GolfBallTransform.position.z != 0)
                {
                    Debug.Log("Ishladi = " + GolfBallTransform.position);
                }
            }

            //if (_golfBall.IsBallMoving)
            //{
            //    transform.position = Vector3.Lerp(transform.position, BallFollower.transform.position, 1 * Time.deltaTime);
            //    IsNeedResetOffset = true;
            //}
            //else if (!_golfBall.IsBallMoving && IsNeedResetOffset && !_golfBall.IsBallOut)
            //{
            //    IsNeedResetOffset = false;
            //    transform.DOMove(BallFollower.transform.position, durration);
            //}
            //else if (_golfBall.IsBallOut)
            //{
            //    if (transform.position != BallFollower.transform.position)
            //    {
            //        IsNeedResetOffset = false;
            //        _golfBall.IsBallOut = false;
            //        transform.DOMove(BallFollower.transform.position, durration);
            //    }
            //}
        }
        

        private float smoothSpeed = 0.75f;        
        void CameraMovement3()
        {
            if (_ballMove.IsBallMoving || (Vector3.Distance(offset, GolfBallTransform.position) <= Vector3.Distance(transform.position, GolfBallTransform.position))) 
            {
                // Calculate the desired camera position based on the target's position and the offset
                Vector3 desiredPosition = GolfBallTransform.position + offset;

                // Smoothly move the camera to the desired position
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;

                // Make the camera look at the target
                //transform.LookAt(GolfBallTransform);
            }

        }


        // distanceCameraAndBall;
        float waitingDurration = 1f;
        float timer;
        private bool isFollowing;

        void CameraMovement4()
        {
            if (!isFollowing)
            {
                timer += Time.deltaTime;
                if (timer >= waitingDurration)
                {
                    isFollowing = true;
                    timer = 0;
                }
            }
            if (isFollowing)
            {
                // Calculate the new camera position while maintaining the initial distance
                Vector3 targetPosition = GolfBallTransform.position - (GolfBallTransform.position - transform.position).normalized * DistanceCameraAndBall;

                // Smoothly move the camera towards the target position
                transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, _initialPos.y, targetPosition.z), Time.deltaTime);
            }

        }


        //void Way2()
        //{
        //    Vector3 desiredPosition = Ball.position + offset;
        //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //    transform.position = new Vector3(smoothedPosition.x, /*smoothedPosition.y +*/ transform.position.y, smoothedPosition.z);
        //    //transform.LookAt(Ball);
        //}

        //void MethodTest2()
        //{
        //    if (_golfBall.IsBallClicked && (_lineDrawer._endPoint != Vector3.zero))
        //    {
        //        lineVector = _lineDrawer._endPoint - Ball.position;
        //        Vector3 newLine = new(0, 0, (Mathf.Abs(lineVector.x) + Mathf.Abs(lineVector.z)));
        //        myNewOffset = standartOffset - newLine;

        //        transform.position = Ball.position;
        //        transform.Translate(myNewOffset);
        //        distanceCameraAndBall = Vector3.Distance(Ball.position, transform.position);
        //    }
        //    else if (_golfBall.IsBallMoving && (myNewOffset != Vector3.zero))
        //    {
        //        Vector3 targetPosition = Ball.position + Ball.forward * (-distanceCameraAndBall) + myNewOffset;
        //        transform.position = Vector3.Lerp(transform.position, targetPosition, 2 * Time.deltaTime);
        //        //transform.LookAt(Ball);
        //        Debug.Log("Moving");
        //    }
        //    else if (!_golfBall.IsBallClicked && !_golfBall.IsBallMoving)
        //    {
        //        SetCamera();
        //    }
        //    else if (myNewOffset == Vector3.zero)
        //    {
        //        SetCamera();
        //        Debug.Log("Zero");
        //    }
        //}


    }
}
