using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GolfBall_Smooth //F++
{
    public class GolfBall : MonoBehaviour
    {
        public enum TypeOfHits {WithLine, WithButton}
        public TypeOfHits CurrentHit;

        public Camera MainCamera;
        public GameEvent BallHitSO;
        public Image PowerBar;
        public string EqualName;
        public bool IsBallClicked = false;
        public bool IsBallMoving = false;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Collider _spheraCollider;
        
        private float forceMultiplier = 15.0f;
        private float minimalSpeed = 0.02f;
        
        private float _initialFieldView;
        private Vector3 _previousClickPosition = new Vector3();
        public Vector3 InitialPosBeforeHit;

        private void Awake()
        {
            _initialFieldView = MainCamera.fieldOfView;
        }


        void Update()
        {
            if (Input.GetMouseButtonDown(0))// Check for mouse click
            {    // Cast a ray from the camera to the mouse position
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Check if the ray hits a GameObject
                if (Physics.Raycast(ray, out hit))
                {   // The hit.collider.gameObject is the GameObject that was clicked
                    GameObject clickedObject = hit.collider.gameObject;

                    if (clickedObject.name.Equals(EqualName) && !IsBallMoving) // "GolfBall"
                    {
                        //Debug.Log("ishladi 2 true");
                        IsBallClicked = true;
                        _previousClickPosition = MainCamera.ScreenToViewportPoint(Input.mousePosition);
                        InitialPosBeforeHit = transform.position;
                    }
                    else
                    {
                        //Debug.Log("ishladi 2 false");
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
                    
                    //if (distance > 23)
                    //{
                    //    distance = 23;
                    //}
                    MainCamera.fieldOfView = Mathf.MoveTowards(MainCamera.fieldOfView, _initialFieldView + distance, 20 * Time.deltaTime);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (IsBallClicked && !IsBallMoving)
                {
                    Debug.Log("Input.GetMouseButton(0)");
                    IsBallClicked = false;
                    IsBallMoving = true;                    
                }
            }

            //SetBallMove();

            if (IsBallMoving)
            {
                SetBallMove();
            }
        }


        void SetBallMove()
        {   
            Vector3 velocity = _rigidBody.velocity;   // Get the velocity of the GameObject            
            float speed = velocity.magnitude;   // Calculate the speed by taking the magnitude of the velocity

            if (minimalSpeed < speed)
            {
                IsBallMoving = true;
            }
            else
            {
                //Debug.Log(" It is work ");
                IsBallMoving = false;
                //StartCoroutine(SampleRoutine());
            }
        }

        
        public void AddForceToBall(Vector3 forceDirection, float currentLength, float maxLength)
        {
            float percentage = currentLength / maxLength;
            _rigidBody.AddForce(forceDirection * forceMultiplier * percentage, ForceMode.Impulse);
            Vector3 velocity = _rigidBody.velocity;            
            float speed = _rigidBody.velocity.magnitude;
            //Debug.Log(velocity + " percentage = " + percentage);
            //Debug.Log(speed);
        }


        IEnumerator SampleRoutine()
        {
            float previousDrag = _rigidBody.drag;
            Vector3 previousVel = _rigidBody.velocity;
            _rigidBody.drag = 5000;

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            _rigidBody.drag = previousDrag;
            _rigidBody.AddForce(previousVel, ForceMode.Impulse);
            //_rigidBody.AddForce(-previousVel, ForceMode.Impulse);
        }

    }
}
