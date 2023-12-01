using MiniGolf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    /// <summary>
    /// Show the start point of line Renderer.
    /// </summary>
    public class MouseCircle : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private BallMovement _ballMovement;
        private SpriteRenderer _spriteRenderer;

        private LineRenderer _lineRenderer;
        public bool _isDrawingLine;
        //float colorfulLineDistance = 0.0145f;


        void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _lineRenderer = _ballMovement.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
        }


        void LateUpdate()
        {
            if (!_ballMovement.IsBallMoving && !_ballMovement.IsUIBoardActive)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Ball"))
                    {
                        transform.position = _ballMovement.transform.position;
                        _isDrawingLine = true;
                        _spriteRenderer.enabled = false;
                    }
                }

                if (Input.GetMouseButton(0) && _isDrawingLine)
                {
                    Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 currentMousePosition = hit.point;
                        currentMousePosition.y = _ballMovement.transform.position.y;

                        float lengthLine = Vector3.Distance(currentMousePosition, _lineRenderer.GetPosition(0));
                        
                        if (lengthLine >= 0.35f)
                        {
                            transform.position = FindPointOnLine(_ballMovement.transform.position, currentMousePosition, 0.36f);
                        }
                        else
                        {
                            Vector3 mousePoint = FindPointOnLine(_ballMovement.transform.position, currentMousePosition, lengthLine + 0.015f);
                            transform.position = mousePoint /*currentMousePosition*/;
                        }

                        _spriteRenderer.enabled = true;
                        _spriteRenderer.color = _lineRenderer.material.color;
                    }
                }

                if (Input.GetMouseButtonUp(0) && _isDrawingLine)
                {
                    _isDrawingLine = false;
                    _spriteRenderer.enabled = false;
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
