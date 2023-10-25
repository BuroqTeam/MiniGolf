using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GolfBall_Smooth //F++
{
    /// <summary>
    /// Ushbu script GolfBall ga qo'shiladi.
    /// </summary>
    public class GolfBall : MonoBehaviour
    {
        public enum TypeOfHits {WithLine, WithButton}
        public TypeOfHits CurrentHit;

        public Camera MainCamera;
        public GameEvent BallHitSO;

        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Collider _spheraCollider;
        [SerializeField] private MeshRenderer _meshRenderer;

        [HideInInspector] public string EqualName;
        public bool IsBallClicked = false;
        public bool IsBallMoving = false;
        private bool IsBallOut;
        
        public TMP_Text DistanceTMP;
        private float forceMultiplier = 50.0f; // 500 drag 0.5f, mass 0.5f
        private float minimalSpeed = 0.12f;
        
        private float _initialFieldView;
        private Vector3 _previousClickPosition = new Vector3();
        public Vector3 InitialPosBeforeHit;  // kasr qismi uzun bo'lsa -3.154 shaklida ko'rinib qolayabdi lekin oxirida e-10 bor. 

        private float _initialDrag;

        private void Awake()
        {
            EqualName = gameObject.name;
            _initialFieldView = MainCamera.fieldOfView;
            _initialDrag = _rigidBody.drag;
        }


        void Update()
        {
            if (InitialPosBeforeHit != Vector3.zero)
            {
                FindDistance();
            }            

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
                        IsBallClicked = true;
                        _previousClickPosition = MainCamera.ScreenToViewportPoint(Input.mousePosition);
                        InitialPosBeforeHit = gameObject.transform.position;
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
                    distance *= 100;

                    if (MainCamera.fieldOfView < MainCamera.GetComponent<ScrollControll>().MaxFieldOfView) // Ball bosib line chizilganda cameraning uzoqlashishi 
                    {
                        MainCamera.fieldOfView = Mathf.MoveTowards(MainCamera.fieldOfView, _initialFieldView + distance, 20 * Time.deltaTime);
                    }
                    else if (_initialFieldView + distance < MainCamera.fieldOfView)// Ball bosib line chizilganda cameraning yaqinlashishi
                    {
                        MainCamera.fieldOfView = Mathf.MoveTowards(MainCamera.fieldOfView, _initialFieldView + distance, 20 * Time.deltaTime);
                    }

                    //MainCamera.fieldOfView = Mathf.MoveTowards(MainCamera.fieldOfView, _initialFieldView + distance, 20 * Time.deltaTime);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (IsBallClicked && !IsBallMoving)
                {
                    Debug.Log("MainCamera.fieldOfView = " + MainCamera.fieldOfView);
                    //Debug.Log("Input.GetMouseButton(0)");
                    IsBallClicked = false;                    
                }
            }

            SetBallMove();
        }

        public Vector3 velocity;
        public float speed = 0;
        public float maxSpeed;
        public string stopping;

        void SetBallMove()
        {   
            velocity = _rigidBody.velocity;   // Get the velocity of the GameObject            
            speed = velocity.magnitude;       // Calculate the speed by taking the magnitude of the velocity

            if (maxSpeed == 0 || maxSpeed < speed)
            {
                maxSpeed = speed;
            }

            if (minimalSpeed < speed)
            {
                IsBallMoving = true;
                stopping = "Yurish";
            }
            else if (minimalSpeed > speed && speed != 0)
            {
                _rigidBody.drag += 0.5f;
                //Debug.Log(_rigidBody.drag);
                //StartCoroutine(SampleRoutine());
            }
            else if (speed == 0)
            {
                IsBallMoving = false;
                _rigidBody.drag = _initialDrag;
                stopping = "To'xtash";
            }
            
        }

        
        void FindDistance()
        {
            float distance = Vector3.Distance(InitialPosBeforeHit, transform.position);
            distance = Mathf.CeilToInt(Vector3.Distance(InitialPosBeforeHit, transform.position) / 0.25f);
            DistanceTMP.text = distance.ToString() + "m";            
        }

        
        public void AddForceToBall(Vector3 forceDirection, float currentLength, float maxLength)
        {
            float percentage = currentLength / maxLength;
            _rigidBody.AddForce(forceDirection * forceMultiplier * percentage/*, ForceMode.Impulse*/);
            //Vector3 velocity = _rigidBody.velocity;
            float speed = _rigidBody.velocity.magnitude;
            Debug.Log("speed = " + speed);
        }

        
        //IEnumerator SampleRoutine()
        //{
        //    float previousDrag = _rigidBody.drag;
        //    Vector3 previousVel = _rigidBody.velocity;
        //    _rigidBody.drag = 5000;

        //    yield return new WaitForFixedUpdate();
        //    yield return new WaitForFixedUpdate();

        //    _rigidBody.drag = previousDrag;
        //    _rigidBody.AddForce(previousVel, ForceMode.Impulse);
        //}


        /// <summary>
        /// Ballni boshlang'ich pozitsiyaga qaytaruvchi kod. 
        /// </summary>
        public void ResetBall()
        {
            IsBallOut = true;
            StartCoroutine(ResetBallWithDelay());
        }


        IEnumerator ResetBallWithDelay()
        {
            SwitchBallComponents(false);
            transform.position = InitialPosBeforeHit;

            yield return new WaitForSeconds(0.5f);
            SwitchBallComponents(true);

            if (InitialPosBeforeHit != transform.position)
            {
                Debug.Log("Ishlamadi");
            }
        }


        void SwitchBallComponents(bool _isTrue)
        {
            Collider[] colliders = GetComponents<Collider>();
            foreach (Collider coll in colliders)
                coll.enabled = _isTrue;

            _rigidBody.isKinematic = !_isTrue;
            //_trailRenderer.enabled = _isTrue;
            _meshRenderer.enabled = _isTrue;
        }


    }
}
