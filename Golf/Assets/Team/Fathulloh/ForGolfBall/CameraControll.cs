using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace GolfBall_Smooth //F++
{
    public class CameraControll : MonoBehaviour
    {
        [SerializeField] private GolfBall _golfBall;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LineDrawer _lineDrawer;

        private Vector3 _previousPosition; // initialPosition

        public Transform FinishFlagTransform;
        public Transform GolfBallTransform;
        private float smoothSpeed = 0.125f;
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
        public Vector3 MovingOffset = new(0, 0.2f, -1);


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
            if (Input.GetMouseButtonDown(0))  // It work when dragging
            {                
                DistanceCameraAndBall = Vector3.Distance(BallFollower.transform.position, GolfBallTransform.position);

                _initialPos = transform.position;
                _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
                //Debug.Log("First");
            }

            if (Input.GetMouseButton(0))      // It work for rotation change
            {
                if (!_golfBall.IsBallClicked)
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

            CameraMovement1();
            //CameraMovement2();
        }

        bool _isMovingWithNewDistance = false;
        void CameraMovement2()
        {
            if (_golfBall.IsBallMoving)
            {
                Vector3 cameraPos = transform.position;
                Vector3 ballPos = GolfBallTransform.position;
                cameraPos = new Vector3(cameraPos.x, 0, cameraPos.z);
                ballPos = new Vector3(ballPos.x, 0, ballPos.z);
                
                if (Vector3.Distance(cameraPos, ballPos) > 1) 
                {
                    _isMovingWithNewDistance = true;                    
                    Debug.Log("First If");
                }

                if (_isMovingWithNewDistance) 
                {
                    transform.position = GolfBallTransform.position;
                    transform.Translate(MovingOffset);
                    Debug.Log("Second if");
                }
                //Debug.Log(Vector3.Distance(cameraPos, ballPos));
            }
            else if (!_golfBall.IsBallMoving)
            {
                //Transform newTrans = GolfBallTransform;
                transform.position = GolfBallTransform.position;
                transform.Translate(standartOffset);
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

        bool IsNeedResetOffset = false;
        void CameraMovement1() // Camera move with Lerp and dotween.
        {
            if (_golfBall.IsBallMoving)
            {
                //Debug.Log("Distance is far");
                transform.position = Vector3.Lerp(transform.position, BallFollower.transform.position, 1 * Time.deltaTime);

                IsNeedResetOffset = true;
            }
            else if (!_golfBall.IsBallMoving && IsNeedResetOffset && !_golfBall.IsBallOut)
            {
                //Debug.Log("Distance is near");
                IsNeedResetOffset = false;
                transform.DOMove(BallFollower.transform.position, durration);
            }
            else if (_golfBall.IsBallOut)
            {                
                if (transform.position != BallFollower.transform.position) 
                {
                    IsNeedResetOffset = false;
                    _golfBall.IsBallOut = false;
                    transform.DOMove(BallFollower.transform.position, durration);
                }
                Debug.Log(" isBallOut = " + _golfBall.IsBallOut);
            }
            
        }


        void SetCamera()
        {
            transform.position = GolfBallTransform.position;
            transform.Translate(standartOffset);
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
