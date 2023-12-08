using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MiniGolf
{
    public class Ball : MonoBehaviour
    {
       
        public int LevelNumber;     // 1, 2, 3
        public GameEvent BallHitSO;
        public Camera MainCamera;
        public bool IsBallClicked;
        public bool IsBallMoving;
        public Color GreenColor;
        public Color YellowColor;
        public Color RedColor;
        public TMP_Text DiamondScore;
        public TMP_Text HitScore;
        public TMP_Text Distance;
        public Image PowerBar;


        private TrailRenderer _trailRenderer;
        private MeshRenderer _meshRenderer;
        private Rigidbody _rigidBody;
        private float _initialFieldView;
        private Vector3 _previousClickPosition = new Vector3();
        private float _movementThreshold = 0.01f;
        private LineDrawer _colorfulLine;
        [SerializeField]
        private int _diamondScore = 0;
        [SerializeField]
        private int _hitScore;
        bool _isTrue = false;

        public Vector3 InitialPosBeforeHit;
        [HideInInspector] public bool _IsBallOut;
        

        private void Awake()
        {            
            _meshRenderer = GetComponent<MeshRenderer>();
            _trailRenderer = GetComponent<TrailRenderer>();
            _colorfulLine = transform.GetChild(0).gameObject.GetComponent<LineDrawer>();//F++
            _rigidBody = GetComponent<Rigidbody>();
            _initialFieldView = MainCamera.fieldOfView;
        }
        

        void Update()
        {
            FindDistance();

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
                        InitialPosBeforeHit = transform.position;  // F++
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

                    //if (distance < 9)
                    //{
                    //    _colorfulLine.ChangeLineColor(GreenColor);
                    //    UpdatePowerRadialBar(GreenColor, distance);
                    //}
                    //else if (distance < 17)
                    //{
                    //    _colorfulLine.ChangeLineColor(YellowColor);
                    //    UpdatePowerRadialBar(YellowColor, distance);
                    //}
                    //else if (distance < 23)
                    //{
                    //    _colorfulLine.ChangeLineColor(RedColor);
                    //    UpdatePowerRadialBar(RedColor, distance);
                    //}

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
                    //MainCamera.DOFieldOfView(_initialFieldView, 0.25f);
                    _isTrue = true;
                    IsBallClicked = false;

                    UpdatePowerRadialBar(Color.white, 0);

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
                StartCoroutine(ChangeCameraFields());
            }
        }
                
                
        IEnumerator ChangeCameraFields() // Kamera Fieldini o'zgartirib beruvchi metod.
        { 
            yield return new WaitForSeconds(0.35f);
            if (_isTrue)
            {
                yield return new WaitForSeconds(0.2f /*0.35f*/);
                if ((!IsBallMoving && !IsBallClicked) && _isTrue /*&& !_IsBallOut*/)
                {
                    _isTrue = false;
                    if (_IsBallOut)
                        yield return new WaitForSeconds(1.2f);
                                        
                    MainCamera.DOFieldOfView(_initialFieldView, 0.35f)
                        .SetEase(Ease.Linear);
                }
            }
        }


        public void AddForceOpposite()
        {
            StartCoroutine(SampleRoutine());
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

        
        public void ResetBall()  // Out eventida chaqirilgan
        {
            _IsBallOut = true;
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
            //IsBallClicked = !_isTrue;
        }

                
        public void BallReachFinishFlag(GameObject finishObject) // Buyerda Ballning finishga yetib kelganda nima sodir bo'lishi yoziladi.
        {            
            _rigidBody.isKinematic = true;
            Vector3 finishPos = finishObject.transform.GetChild(1).transform.position;            

            float distance = Vector3.Distance(finishPos, gameObject.transform.position);
            if (distance > 0.1f)
            {
                //Debug.Log(distance);
                transform.position = (finishPos + gameObject.transform.position) / 2;
            }
            
            gameObject.transform.DOMove(finishPos, 0.25f)
                .SetEase(Ease.InCirc)
                .SetDelay(0.25f);
            
            PlayerPrefs.SetInt("Level" + LevelNumber.ToString(), 1);
            //Debug.Log("Level" + PlayerPrefs.GetInt("Level" + LevelNumber.ToString()));
            StartCoroutine(BallInHole());
        }


        IEnumerator BallInHole()
        {
            yield return new WaitForSeconds(0.2f);
            _meshRenderer.enabled = false;
            Scene m_Scene = SceneManager.GetActiveScene();

            char chr = m_Scene.name[m_Scene.name.Length - 1];
            GameManager.Instance.UpdateResultInfo(chr.ToString(), HitScore.text, _diamondScore.ToString());
            //Debug.Log( "chr = " + chr + " HitScore = " + HitScore.text + " DiamondScore = " + _diamondScore.ToString());
        }

        public void IncrementCoinScore()
        {
            _diamondScore++;
            DiamondScore.text = _diamondScore.ToString();
        }

        public void IncrementHitScore()
        {
            _hitScore++;
            HitScore.text = _hitScore.ToString();
        }

        public void FindDistance()
        {
            float distance = Vector3.Distance(InitialPosBeforeHit, transform.position);
            distance = distance * 10;
            distance = (int)distance / 2; // add divsion 2
            Distance.text = distance.ToString() + "m";
        }

        void UpdatePowerRadialBar(Color color, float distance)
        {
            PowerBar.color = color;
            var val = ((int)distance * 10) / 22;
            if (val.Equals(0))
            {
                PowerBar.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            }
            else
            {
                PowerBar.transform.GetChild(0).GetComponent<TMP_Text>().text = val.ToString();
            }

            PowerBar.fillAmount = distance / 22;
        }

                
    }
}

