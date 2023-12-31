using MiniGolf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    /// <summary>
    /// Bollni oldidagi Arrow va animatsiyali Arrow uchun Line Scripti.
    /// </summary>
    public class LineAnim : MonoBehaviour
    {
        public enum LineType
        {
            FrontArrow,
            AnimationArrow
        }

        public LineType CurrentLine;
        public Camera MainCamera;
        public BallDataSO BallData;
        private LineRenderer _lineRenderer;
        private BallMovement _ballMovement;

        private float _maxLengthOfLine;
        private bool _isDrawingLine = false;        
        private Vector3 CurrentPos;
        private Vector3 Point0;
        readonly float distance = 0.025f;        


        void Start()
        {
            _maxLengthOfLine = BallData.MaximalLengthOfLine;
            _ballMovement = transform.parent.GetComponent<BallMovement>();
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.enabled = false;
        }

        void Update()
        {

            if (!_ballMovement.IsBallMoving)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Ball"))
                    {
                        CurrentPos = transform.position;//F++
                        _isDrawingLine = true;
                        Point0 = new Vector3(CurrentPos.x, CurrentPos.y + 0.0025f, CurrentPos.z);
                        _lineRenderer.SetPosition(0, Point0);
                        _lineRenderer.enabled = false;
                    }
                }

                if (Input.GetMouseButton(0) && _isDrawingLine)
                {
                    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 currentMousePosition = hit.point;

                        currentMousePosition.y = CurrentPos.y + 0.0025f;
                        if (CurrentLine == LineType.AnimationArrow)
                        {
                            //_lineRenderer.SetPosition(1, FindPointOnLine(currentMousePosition, Point0, distance));//o�chiriladi.
                            float lengthLine = Vector3.Distance(currentMousePosition, _lineRenderer.GetPosition(0));

                            if (lengthLine >= _maxLengthOfLine + 0.02f) // Sichqoncha agar uzoqda bo�lsa eng yaqin distansiyadagi nuqtani olib beradi.
                            {
                                _lineRenderer.SetPosition(1, FindPointOnLine(_lineRenderer.GetPosition(0), currentMousePosition, _maxLengthOfLine + 0.02f - distance));
                                _lineRenderer.enabled = true;
                            }
                            else
                            {
                                _lineRenderer.SetPosition(1, FindPointOnLine(currentMousePosition, _lineRenderer.GetPosition(0), distance));
                                _lineRenderer.enabled = true;
                            }
                        }
                        else if (CurrentLine == LineType.FrontArrow)
                        {
                            //_lineRenderer.SetPosition(1, new Vector3(2 * CurrentPos.x - currentMousePosition.x, currentMousePosition.y, 2 * CurrentPos.z - currentMousePosition.z)); //o�chiriladi.                        
                            float lengthLine = Vector3.Distance(currentMousePosition, _lineRenderer.GetPosition(0));

                            if (lengthLine >= _maxLengthOfLine + 0.02f)
                            {
                                Vector3 reverseDotPos = new Vector3(2 * CurrentPos.x - currentMousePosition.x, currentMousePosition.y, 2 * CurrentPos.z - currentMousePosition.z);
                                _lineRenderer.SetPosition(1, FindPointOnLine(_lineRenderer.GetPosition(0), reverseDotPos, _maxLengthOfLine + 0.02f));
                                _lineRenderer.enabled = true;
                            }
                            else
                            {
                                Vector3 reverseDotPos = new Vector3(2 * CurrentPos.x - currentMousePosition.x, currentMousePosition.y, 2 * CurrentPos.z - currentMousePosition.z);
                                _lineRenderer.SetPosition(1, reverseDotPos);
                                _lineRenderer.enabled = true;
                            }
                        }

                        _lineRenderer.enabled = true;
                    }
                }

                if (Input.GetMouseButtonUp(0) && _isDrawingLine)
                {
                    _lineRenderer.SetPosition(0, transform.position);
                    _lineRenderer.enabled = false;
                    _isDrawingLine = false;

                    Vector3 startPoint = _lineRenderer.GetPosition(0);
                    Vector3 endPoint = _lineRenderer.GetPosition(1);

                    // Calculate the direction vector.
                    Vector3 direction = startPoint - endPoint;

                    // Normalize the direction vector if you want a unit vector.
                    direction.Normalize();
                }

            }
        }


        /// <summary>
        /// Ikkita nuqta berilgan. Birinchi va ikkinchi nuqtalar orasida joylashgan va birinchi nuqtadan x masofada joylashgan uchinchi nuqtani topish.
        /// </summary>
        /// <param name="point1">Birinchi nuqtaning kordinatasi</param>
        /// <param name="point2">Ikkinchi nuqtaning kordinatasi</param>
        /// <param name="distance">Birinchi nuqtadan maksimal masofa</param>
        /// <returns></returns>
        Vector3 FindPointOnLine(Vector3 point1, Vector3 point2, float distance)
        {
            float totalDistance = Vector3.Distance(point1, point2);
            float ratio = distance / totalDistance;

            float newX = point1.x + ratio * (point2.x - point1.x);
            float newY = point1.y /*+ ratio * (point2.y - point1.y)*/;
            float newZ = point1.z + ratio * (point2.z - point1.z);

            return new Vector3(newX, newY, newZ);
        }
    }
}
