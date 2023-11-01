using UnityEngine;
using UnityEngine.UI;

namespace GolfBall_Smooth
{
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        //[SerializeField] private GolfBall _golfBall;
        [SerializeField] private BallMovement _ballMove;
        [SerializeField] private Camera MainCamera;
        [SerializeField] private Image PowerBar; // F--

        public Color GreenColor;
        public Color YellowColor;
        public Color RedColor;
        [HideInInspector] public Color CurrentColor;
        private bool _IsAddForce;
        private bool _isDrawingLine;
        /*[SerializeField] private*/public Vector3 _startPoint;
        public Vector3 _endPoint;

        private Vector3 _direction;
        private float _distance;
        [HideInInspector] public float _maxLength = 0.34f;               


        void Update()
        {
            MainMethod();
        }


        void MainMethod()
        {
            if (!_ballMove.IsBallMoving /*&& _golfBall.IsBallClicked*/)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name.Equals(_ballMove.EqualName))
                    {
                        _isDrawingLine = true;
                        _startPoint = transform.position;
                        _lineRenderer.enabled = false;
                        _lineRenderer.positionCount = 2;
                        _lineRenderer.SetPosition(0, _startPoint);
                        //Debug.Log("ButtonDown");
                    }
                }

                if (Input.GetMouseButton(0) && _isDrawingLine)
                {
                    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 currentMousePosition = hit.point;                        
                        currentMousePosition.y = transform.position.y;

                        _distance = Vector3.Distance(_startPoint, currentMousePosition);
                        if (_maxLength < _distance)
                        {
                            _endPoint = FindPointOnLine(_lineRenderer.GetPosition(0), currentMousePosition, _maxLength/* - 0.013f*/);                            
                        }
                        else if (_maxLength > _distance)
                        {
                            _endPoint = currentMousePosition;
                        }
                        _lineRenderer.enabled = true;
                        _lineRenderer.SetPosition(1, _endPoint);
                        _distance = Vector3.Distance(_startPoint, _endPoint);

                        CheckColor();
                    }
                }

                if (Input.GetMouseButtonUp(0) && _isDrawingLine)
                {
                    _lineRenderer.enabled = false;
                    _isDrawingLine = false;
                    _lineRenderer.positionCount = 0;
                    _IsAddForce = true;

                    _ballMove.BallHitSO.Raise();
                    //UpdatePowerRadialBar(Color.white, 0);
                    CallAddForce();
                    Debug.Log("ButtonUp");
                }
            }

        }

        
        void CheckColor()  // Check line length and choose color.
        {
            float mydistance = Vector3.Distance(_startPoint, _endPoint);
            mydistance *= 100;

            if (mydistance < 16)
            {
                ChangeLineColor(GreenColor);
                //UpdatePowerRadialBar(GreenColor, mydistance);
            }
            else if (mydistance < 25)
            {
                ChangeLineColor(YellowColor);
                //UpdatePowerRadialBar(YellowColor, mydistance);
            }
            else if (mydistance <= 34)
            {
                ChangeLineColor(RedColor);
                //UpdatePowerRadialBar(RedColor, mydistance);
            }

            if (mydistance > ((int)(_maxLength * 100)))
            {
                mydistance = (int)(_maxLength * 100);
            }
        }


        public void ChangeLineColor(Color newColor)// Line ning rangini o'zgartiradi. for color change
        {
            Material newMat = (_lineRenderer.material);
            newMat.color = newColor;
            CurrentColor = newColor;
            _lineRenderer.material = newMat;
        }        
        

        void CallAddForce() // Balldagi RigidBodyga AddForce ni beradi. 
        {
            if (_IsAddForce)
            {
                _IsAddForce = false;
                //_distance = Vector3.Distance(_startPoint, _endPoint);
                _direction = _startPoint - _endPoint;
                _direction.Normalize();

                _ballMove.AddForceToBall(_direction, _distance, _maxLength);
            }
        }


        /// <summary>
        /// A va B nuqtalar orasida joylashgan va A nuqtadan distance masofada yotgan nuqtaning kordinatasini aniqlab beradi.
        /// </summary>
        /// <param name="point1">Birinchi nuqta</param>
        /// <param name="point2">Ikkinchi nuqta</param>
        /// <param name="distance">masofa</param>
        /// <returns></returns>
        Vector3 FindPointOnLine(Vector3 point1, Vector3 point2, float distance)
        {
            float totalDistance = Vector3.Distance(point1, point2);
            float ratio = distance / totalDistance;

            float newX = point1.x + ratio * (point2.x - point1.x);
            float newY = point1.y;
            float newZ = point1.z + ratio * (point2.z - point1.z);

            return new Vector3(newX, newY, newZ);
        }


        //void UpdatePowerRadialBar(Color color, float distance2)
        //{
        //    PowerBar.color = color;
        //    float percentageOfBar = (distance2 / _maxLength) / 100;
        //    PowerBar.fillAmount = percentageOfBar;

        //    if (percentageOfBar.Equals(0))
        //    {
        //        PowerBar.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        //    }
        //    else
        //    {
        //        PowerBar.transform.GetChild(0).GetComponent<TMP_Text>().text = Mathf.CeilToInt(10 * percentageOfBar).ToString();
        //    }
        //}

    }
}
