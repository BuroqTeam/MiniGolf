using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace GolfBall_Smooth
{
    /// <summary>
    /// Golfning harakatiga javobgar script
    /// </summary>
    public class BallMovement : MonoBehaviour
    {
        public enum TypeOfHits { WithLine, WithButton }
        public TypeOfHits CurrentHit;
        public int LevelNumber;
        public Camera MainCamera;
        public GameEvent BallHitSO;
        public BallDataSO BallData;

        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Collider _spheraCollider;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private TrailRenderer _trailRenderer;
        public GameObject UIBoard;
        [HideInInspector] public string EqualName;
        public bool IsBallClicked = false;
        public bool IsBallMoving = false;        
        public bool IsBallOut;
        public bool IsUIBoardActive;
        //private float forceMultiplier = 50.0f; // 500 drag 0.5f, mass 0.5f
        //private float minimalSpeed = 0.12f;
        //private float sizeEachCell = 0.25f;

        private float _initialFieldView;
        private Vector3 _previousClickPosition = new Vector3();
        public Vector3 InitialPosBeforeHit;  // kasr qismi uzun bo'lsa -3.154 shaklida ko'rinib qolayabdi lekin oxirida e-10 bor. 

        
        private void Awake()
        {
            EqualName = gameObject.name;
            _initialFieldView = MainCamera.fieldOfView;
        }
               

        void Update()
        {
            IsUIBoardActive = UIBoard.activeSelf;

            if (Input.GetMouseButtonDown(0) && !IsUIBoardActive)// Check for mouse click
            {   // Cast a ray from the camera to the mouse position
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
                        //Debug.Log("1");
                    }
                    else if (_initialFieldView + distance < MainCamera.fieldOfView)// Ball bosib line chizilganda cameraning yaqinlashishi
                    {
                        MainCamera.fieldOfView = Mathf.MoveTowards(MainCamera.fieldOfView, _initialFieldView + distance, 20 * Time.deltaTime);
                        //Debug.Log("2");
                    }

                    //MainCamera.fieldOfView = Mathf.MoveTowards(MainCamera.fieldOfView, _initialFieldView + distance, 20 * Time.deltaTime);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (IsBallClicked && !IsBallMoving)
                {
                    //Debug.Log("MainCamera.fieldOfView = " + MainCamera.fieldOfView);
                    //IsBallClicked = false;
                    StartCoroutine(IsForceAddedChangeValue());
                }
            }            

            //if (transform.position.z <= -1)
            //{
            //    SetBallMove(); // ResetBallPos()
            //}
        }


        IEnumerator IsForceAddedChangeValue()
        {
            yield return new WaitForSeconds(0.12f);
            IsBallClicked = false;
        }


        /// <summary>
        /// Cho'kishning kodi yoziladi buyerga.
        /// </summary>
        public void Sink()
        {
            // Ball is being sinked
        }

        
        public void AddForceToBall(Vector3 forceDirection, float currentLength, float maxLength)
        {
            float percentage = currentLength / maxLength;
            _rigidBody.AddForce(forceDirection * BallData.ForceMultiplier * percentage/*, ForceMode.Impulse*/);
        }

               
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
            _trailRenderer.enabled = _isTrue;
            _meshRenderer.enabled = _isTrue;
        }


        public void BallReachFinishFlag(GameObject finishObject)
        {
            _rigidBody.isKinematic = true;
            Vector3 finishPos = finishObject.transform.GetChild(1).transform.position;
            float distance = Vector3.Distance(finishPos, gameObject.transform.position);

            if (distance > 0.1f)
            {
                transform.position = (finishPos + gameObject.transform.position) / 2;
            }

            gameObject.transform.DOMove(finishPos, 0.25f)
                .SetEase(Ease.InCirc)
                .SetDelay(0.25f);

            StartCoroutine(BallInvisible());
            PlayerPrefs.SetInt("Solid" + LevelNumber.ToString(), 1);
        }


        IEnumerator BallInvisible()
        {
            yield return new WaitForSeconds(0.25f);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

    }
}
