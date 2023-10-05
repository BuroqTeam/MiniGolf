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
        public Camera CurrentCamera;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Ball _ball;
        [SerializeField] private Rigidbody _rigifbodyBall;
        [SerializeField] private Button _hitButton;
        [SerializeField] private Slider _slider;

        private bool _isSomething = true;
        private Vector3 _direction;

        private float _lineLength = 0.4f;

        void Start()
        {

        }


        void Update()
        {
            if (!_ball.IsBallMoving)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _lineRenderer.enabled = true;
                    //Debug.Log(CurrentCamera.transform.rotation + " " + CurrentCamera.transform.rotation.y); 
                    _lineRenderer.SetPosition(0, transform.position);
                }

                if (Input.GetMouseButton(0))
                {
                    Quaternion cameraRotation = CurrentCamera.transform.rotation;
                    
                    Vector3 originalPos = new (transform.position.x, transform.position.y, transform.position.z + _lineLength);
                    Vector3 rotatedPos = new Quaternion(0, cameraRotation.y, 0, cameraRotation.w) * originalPos;
                    
                    _lineRenderer.SetPosition(1, rotatedPos);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Vector3 startPoint = _lineRenderer.GetPosition(0);
                    Vector3 endPoint = _lineRenderer.GetPosition(1);

                    _direction = startPoint - endPoint;
                    _direction.Normalize();

                    _hitButton.interactable = true;
                    //_lineRenderer.enabled = false;
                }
            }
        }


        public void AddingForceToBall(float slideNumber)
        {
            _rigifbodyBall.AddForce(_direction * 800 * slideNumber);
        }


    }
}
