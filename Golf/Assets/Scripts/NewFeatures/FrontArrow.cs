using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGolf
{
    /// <summary>
    /// NewFeatures bo'yicha qo'shilayotgan imkoniyatlar.
    /// Ballning oldida chiqadigan direction uchun bu script.
    /// </summary>
    public class FrontArrow : MonoBehaviour
    {        
        public Transform CameraTransform;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Ball _ball;
        [SerializeField] private Rigidbody _rigifbodyBall;
        [SerializeField] private HitButton _hitButton;
        [SerializeField] private Slider _slider;
        [SerializeField] private SliderControl _sliderControl;

        private Vector3 startPoint;
        private Vector3 _direction;
        private readonly float _distance = 0.4f;
        

        void Start()
        {

        }


        void Update()
        {
            if (!_ball.IsBallMoving)
            {
                if (Input.GetMouseButtonDown(0))
                {                    
                    _lineRenderer.positionCount = 2;
                    _lineRenderer.enabled = true;
                    startPoint = transform.position;
                    _lineRenderer.SetPosition(0, startPoint);
                }

                if (Input.GetMouseButton(0))
                {
                    Vector3 cameraPoint = CameraTransform.position;
                    //Vector3 startPoint = _lineRenderer.GetPosition(0);

                    cameraPoint = new(cameraPoint.x, startPoint.y, cameraPoint.z);

                    _direction = (startPoint - cameraPoint).normalized;

                    Vector3 endPoint = startPoint + _direction * _distance;
                    _lineRenderer.SetPosition(1, endPoint);                                       
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Vector3 startPoint = _lineRenderer.GetPosition(0);
                    Vector3 endPoint = _lineRenderer.GetPosition(1);

                    _direction = endPoint - startPoint;                    
                    _direction.Normalize();
                    //Debug.Log("FrontArrow _hitButton.interactable = " + _hitButton._isInteractable);
                    _hitButton.HitButtonInteractable(true);
                    
                    _ball.InitialPosBeforeHit = _ball.gameObject.transform.position;
                }
            }
        }


        public void AddingForceToBall(float slideNumber)
        {
            _ball.BallHitSO.Raise();
            _lineRenderer.enabled = false;
            _rigifbodyBall.AddForce(800 * slideNumber * _direction);
            //Debug.Log("slideNumber = " + slideNumber);
        }

                
        //void CalculateThirdPoint()
        //{
        //    Vector3 firstPoint = CameraTransform.position;
        //    Vector3 secondPoint = _lineRenderer.GetPosition(0);

        //    firstPoint = new(firstPoint.x, secondPoint.y, firstPoint.z);
        //    _direction = (secondPoint - firstPoint).normalized;

        //    Vector3 thirdPoint = secondPoint + _direction * _distance;
        //    _lineRenderer.SetPosition(1, thirdPoint);


        //    Quaternion cameraRotation = CameraTransform.rotation;
        //    Vector3 originalPos = new(transform.position.x, transform.position.y, transform.position.z + _distance);
        //    Vector3 rotatedPos = new Quaternion(0, cameraRotation.y, 0, cameraRotation.w) * originalPos;

        //    if (Vector3.Distance(rotatedPos, startPos) <= _distance)
        //    {
        //        _lineRenderer.SetPosition(1, rotatedPos);
        //    }
        //    else if (Vector3.Distance(rotatedPos, startPos) > _distance)
        //    {
        //        _lineRenderer.SetPosition(1, FindPointOnLine(startPos, rotatedPos, _distance));
        //    }

        //    Vector3 offset = Quaternion.Euler(0, cameraRotation.y, 0) * Vector3.forward * _distance;
        //    Vector3 endPoint = _lineRenderer.GetPosition(0) + offset;
        //    _lineRenderer.SetPosition(1, endPoint);
        //}


    }
}
