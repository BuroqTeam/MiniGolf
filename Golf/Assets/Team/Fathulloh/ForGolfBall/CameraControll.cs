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

        public Transform FinishFlag;
        public Transform Ball;
        private float smoothSpeed = 0.125f;

        /// <summary>
        /// Camera va ball orasida masofa vectorda. 
        /// </summary>
        [SerializeField] private Vector3 offset;
        private Vector3 _initialPos;
        private bool isCameraAnimationFinished = true;        
        private float _initialFieldView;

        public float distanceCameraAndBall;
        Vector3 standartOffset = new(0, 0.15f, -0.85f);
        public GameObject BallFollower;

        public Vector3 myNewOffset;
        [SerializeField] private Vector3 lineVector;

        [Range(0, 100)]
        public float variableInRange;
        public Vector3 MovingOffset;


        void Start()
        {            
            distanceCameraAndBall = Vector3.Distance(transform.position, Ball.position);
            _initialFieldView = _mainCamera.fieldOfView;            
            offset = transform.position - Ball.position;

            //StartCoroutine(CameraStartAnimation());
        }
        

        IEnumerator CameraStartAnimation()
        {
            Vector3 newPos = FinishFlag.position + offset;
            transform.position = new(newPos.x, 0.72f, newPos.z);
            _mainCamera.fieldOfView = 80;
            yield return new WaitForSeconds(1f);                       

            transform.DOMove(Ball.position + standartOffset /*initialPos*/, 1.5f)
                .SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.2f);
            _mainCamera.DOFieldOfView(_initialFieldView, 0.75f)
                        .SetEase(Ease.Linear);
            yield return new WaitForSeconds(1.8f);                        
            
            Debug.Log(" transform.position = " + transform.position + " transform.rotation = " + transform.rotation);
            yield return new WaitForSeconds(1f);
            isCameraAnimationFinished = true;
            yield return new WaitForSeconds(1f);
            
            Debug.Log(" transform.position = " + transform.position + " transform.rotation = " + transform.rotation);
            Debug.Log(" Ball.position + newOffset = " + (Ball.position + standartOffset));
        }


        private void LateUpdate()
        {
            if (isCameraAnimationFinished)
            {
                Way1();

                //Way2();
            }
        }

        
        void Way1()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //transform.position = Ball.position;
                //transform.Translate(standartOffset);
                distanceCameraAndBall = Vector3.Distance(BallFollower.transform.position, Ball.position);

                _initialPos = transform.position;
                _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
                Debug.Log("First");
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

            //NewSetCamera();
            SetCamera();

            //MethodTest1()
            //MethodTest2();
        }

        public float currentDistance;

        void NewSetCamera()
        {
            if (Vector3.Distance(BallFollower.transform.position, Ball.position) > distanceCameraAndBall || distanceCameraAndBall == 0)
            {
                Debug.Log("Distance is far");
                //BallFollower.transform.position = Ball.position;
                //BallFollower.transform.Translate(standartOffset);
                transform.position = Vector3.Lerp(transform.position, BallFollower.transform.position, 1 * Time.deltaTime);
                currentDistance = Vector3.Distance(transform.position, Ball.position);
            }
            else if (Vector3.Distance(transform.position, Ball.position) < distanceCameraAndBall)
            {
                Debug.Log("Distance is near");
                transform.position = Ball.position;
                transform.Translate(standartOffset);
            }
            else if (!_golfBall.IsBallClicked && !_golfBall.IsBallMoving)
            {
                transform.position = Ball.position;
                transform.Translate(standartOffset);
            }
        }


        void SetCamera()
        {
            transform.position = Ball.position;
            transform.Translate(standartOffset);
        }


        void Way2()
        {
            Vector3 desiredPosition = Ball.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = new Vector3(smoothedPosition.x, /*smoothedPosition.y +*/ transform.position.y, smoothedPosition.z);
            //transform.LookAt(Ball);
        }


        void MethodTest2()
        {
            if (_golfBall.IsBallClicked && (_lineDrawer._endPoint != Vector3.zero))
            {
                lineVector = _lineDrawer._endPoint - Ball.position;
                Vector3 newLine = new(0, 0, (Mathf.Abs(lineVector.x) + Mathf.Abs(lineVector.z)));
                myNewOffset = standartOffset - newLine;

                transform.position = Ball.position;
                transform.Translate(myNewOffset);
                distanceCameraAndBall = Vector3.Distance(Ball.position, transform.position);
            }
            else if (_golfBall.IsBallMoving && (myNewOffset != Vector3.zero))
            {
                //Vector3 newPointC = Ball.position + (myNewOffset - Ball.position).normalized * Distance1;
                //Vector3 newPointC = Ball.position + myNewOffset;
                //transform.position = Vector3.Lerp(transform.position, newPointC, 2 * Time.deltaTime);

                Vector3 targetPosition = Ball.position + Ball.forward * (-distanceCameraAndBall) + myNewOffset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, 2 * Time.deltaTime);
                //transform.LookAt(Ball);
                Debug.Log("Moving");
            }
            else if (!_golfBall.IsBallClicked && !_golfBall.IsBallMoving)
            {
                SetCamera();
            }
            else if (myNewOffset == Vector3.zero)
            {
                SetCamera();
                Debug.Log("Zero");
            }
        }

        void MethodTest1()
        {
            if (_golfBall.IsBallClicked && (_lineDrawer._endPoint != Vector3.zero))
            {
                Vector3 lineVector = _lineDrawer._endPoint - Ball.position;
                lineVector = new(lineVector.x, 0, lineVector.z);
                myNewOffset = Ball.position + lineVector + standartOffset;
                //myNewOffset = new(myNewOffset.x, _initialPos.y, myNewOffset.z);
                //transform.position = Ball.position;
                //transform.Translate(myNewOffset);
                transform.position = Vector3.Lerp(transform.position, myNewOffset, 1 * Time.deltaTime);
                distanceCameraAndBall = Vector3.Distance(Ball.position, myNewOffset);
                //Debug.Log("Clicking");
            }
            else if (_golfBall.IsBallMoving && (myNewOffset != Vector3.zero))
            {
                Vector3 newPointC = Ball.position + (myNewOffset - Ball.position).normalized * distanceCameraAndBall;
                transform.position = Vector3.Lerp(transform.position, newPointC, 2 * Time.deltaTime);
                //Debug.Log("Moving");
            }
            else if (!_golfBall.IsBallClicked && !_golfBall.IsBallMoving)
            {
                SetCamera();
            }
            else if (myNewOffset == Vector3.zero)
            {
                SetCamera();
                Debug.Log("Zero");
            }

        }

    }
}
