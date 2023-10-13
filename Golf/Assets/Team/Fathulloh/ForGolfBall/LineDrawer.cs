using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private GolfBall _golfBall;
        [SerializeField] private Camera MainCamera;

        private bool _IsAddForce;

        private bool isDrawingLine;
        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private Vector3 _direction;

        void Start()
        {

        }

        
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
                    isDrawingLine = true;
                    _startPoint = transform.position;
                    _lineRenderer.positionCount = 2;
                    _lineRenderer.SetPosition(0, _startPoint);
                    _lineRenderer.enabled = false;
                    Debug.Log("ButtonDown");
                }

                if (Input.GetMouseButton(0))
                {
                    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 currentMousePosition = hit.point;
                        _lineRenderer.enabled = true;
                        currentMousePosition.y = transform.position.y;
                        _endPoint = currentMousePosition;
                        _lineRenderer.SetPosition(1, currentMousePosition);
                    }
                    Debug.Log("Button");
                }

                if (Input.GetMouseButtonUp(0) && isDrawingLine)
                {
                    _lineRenderer.enabled = false;
                    isDrawingLine = false;
                    _lineRenderer.positionCount = 0;
                    _IsAddForce = true;

                    CallAddForce();
                    
                    Debug.Log("ButtonUp");
                }
            }

        }


        void CallAddForce()
        {
            if (_IsAddForce)
            {
                _IsAddForce = false;
                _direction = _startPoint - _endPoint;
                _direction.Normalize();

            }
        }


    }
}
