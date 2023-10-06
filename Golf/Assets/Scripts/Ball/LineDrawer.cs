using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGolf
{
    public class LineDrawer : MonoBehaviour
    {
        public Camera MainCamera;

        private LineRenderer _lineRenderer;
        private bool _isDrawingLine = false;

        Ball _ball;
        private readonly float distance = 0.013f;
        [HideInInspector] public readonly float maxLengthOfLine = 0.4f;
        private Vector3 limit = Vector3.zero;
        private float maxDistance = 0.39f;
        private int _addingForce;
        private bool _IsAddForce = false;
        private Vector3 _direction;

        void Start()
        {
            _ball = transform.parent.GetComponent<Ball>();
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.enabled = false;
        }

        void Update()
        {

            if (!_ball.IsBallMoving)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name.Equals("Ball"))
                    {
                        _isDrawingLine = true;
                        _lineRenderer.SetPosition(0, transform.position);
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

                        currentMousePosition.y = transform.position.y; // 0


                        float lengthLine = Vector3.Distance(currentMousePosition, _lineRenderer.GetPosition(0));

                        if (lengthLine >= 0.40f)
                        {
                            limit = currentMousePosition;
                            //_lineRenderer.SetPosition(1, currentMousePosition);
                            _lineRenderer.SetPosition(1, FindPointOnLine(_lineRenderer.GetPosition(0), currentMousePosition, 0.40f - distance));
                            _lineRenderer.enabled = true;
                        }
                        else
                        {
                            _lineRenderer.SetPosition(1, FindPointOnLine(currentMousePosition, _lineRenderer.GetPosition(0), distance));
                            _lineRenderer.enabled = true;
                        }

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
                    _direction = startPoint - endPoint;

                    float lineLength = Vector3.Distance(startPoint, endPoint);
                    _addingForce = (int)(lineLength / maxDistance * 800); // 1000
                    
                    // Normalize the direction vector if you want a unit vector.
                    _direction.Normalize();
                    _ball.BallHitSO.Raise();
                    _IsAddForce = true;
                    //Debug.Log(_addingForce + "  " + _direction);
                }
            }

        }


        private void FixedUpdate()
        {
            if (_IsAddForce)
            {
                _ball.gameObject.GetComponent<Rigidbody>().AddForce(_direction * _addingForce ); // _addingForce ning o'rni 1000 turgan edi. 
                _IsAddForce = false;
                _ball.IncrementHitScore();
            }
        }


        public void ChangeLineColor(Color newColor)// for color change
        {
            Material newMat = (_lineRenderer.material);
            newMat.color = newColor;
            _lineRenderer.material = newMat;
        }


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

