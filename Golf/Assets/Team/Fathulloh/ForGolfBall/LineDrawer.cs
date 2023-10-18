using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GolfBall_Smooth
{
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private GolfBall _golfBall;
        [SerializeField] private Camera MainCamera;
        [SerializeField] private Image PowerBar;

        public Color GreenColor;
        public Color YellowColor;
        public Color RedColor;

        private bool _IsAddForce;

        private bool _isDrawingLine;
        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private Vector3 _direction;
        private float _maxLength = 0.4f;
        public float _distance;


        void Update()
        {
            MainMethod();
        }

        void MainMethod()
        {
            if (!_golfBall.IsBallMoving /*&& _golfBall.IsBallClicked*/)
            {
                if (Input.GetMouseButtonDown(0)) // 
                {
                    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name.Equals(_golfBall.EqualName))
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
                            _endPoint = FindPointOnLine(_lineRenderer.GetPosition(0), currentMousePosition, 0.40f/* - 0.013f*/);
                            //Debug.Log("Uzun = " + _distance);
                        }
                        else if (_maxLength > _distance)
                        {
                            _endPoint = currentMousePosition;
                            //Debug.Log("Kalta = " + _distance);
                        }
                        _lineRenderer.enabled = true;
                        _lineRenderer.SetPosition(1, _endPoint);

                        //_endPoint = currentMousePosition;
                        //_lineRenderer.SetPosition(1, currentMousePosition);
                        //_distance = Vector3.Distance(_startPoint, _endPoint);

                        RadialBarDrawer();
                    }
                }

                if (Input.GetMouseButtonUp(0) && _isDrawingLine)
                {
                    _lineRenderer.enabled = false;
                    _isDrawingLine = false;
                    _lineRenderer.positionCount = 0;
                    _IsAddForce = true;

                    UpdatePowerRadialBar(Color.white, 0);
                    CallAddForce();
                    //Debug.Log("ButtonUp");
                }
            }

        }        


        void RadialBarDrawer()  // RadialBar ni yangilaydi
        {
            float distance = Vector3.Distance(_startPoint, _endPoint);
            distance = distance * 100;

            if (distance < 12)
            {
                ChangeLineColor(GreenColor);
                UpdatePowerRadialBar(GreenColor, distance);
            }
            else if (distance < 18)
            {
                ChangeLineColor(YellowColor);
                UpdatePowerRadialBar(YellowColor, distance);
            }
            else if (distance < 23)
            {
                ChangeLineColor(RedColor);
                UpdatePowerRadialBar(RedColor, distance);
            }

            //if (distance > 23)
            //{
            //    distance = 23;
            //}
        }


        public void ChangeLineColor(Color newColor)// Line ning rangini o'zgartiradi. for color change
        {
            Material newMat = (_lineRenderer.material);
            newMat.color = newColor;
            _lineRenderer.material = newMat;
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

            Debug.Log(" " + Vector3.Distance(_startPoint, _endPoint));
            //Debug.Log("distance = " + distance);
            PowerBar.fillAmount = distance / 22;
        }


        void CallAddForce() // Balldagi RigidBodyga AddForce ni beradi. 
        {
            if (_IsAddForce)
            {
                _IsAddForce = false;
                //_distance = Vector3.Distance(_startPoint, _endPoint);
                _direction = _startPoint - _endPoint;
                _direction.Normalize();

                _golfBall.AddForceToBall(_direction, _distance, _maxLength);
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

    }
}
