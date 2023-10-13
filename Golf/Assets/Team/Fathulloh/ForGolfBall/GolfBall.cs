using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth //F++
{
    public class GolfBall : MonoBehaviour
    {
        public enum TypeOfHits {WithLine, WithButton}
        public TypeOfHits CurrentHit;

        public Camera MainCamera;
        public GameEvent BallHitSO;
        [HideInInspector] public bool IsBallClicked;
        [HideInInspector] public bool IsBallMoving = false;

        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Collider _spheraCollider;
        
        private float forceMultiplier = 15.0f;        

        private float minimalSpeed = 0.01f;
        int iNumber = 1;

        void Start()
        {

        }
        
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !IsBallMoving)
            {
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        // Calculate the direction to apply the force
                        Vector3 forceDirection = transform.forward;
                        //Vector3 newDirection = 2 * transform.position - MainCamera.transform.position;                        
                        //newDirection = new Vector3(newDirection.x, 0, newDirection.z);
                        //newDirection.Normalize();

                        // Apply force to the golf ball
                        //_rigidBody.AddForce(forceDirection * forceMultiplier, ForceMode.Impulse);

                        // Set the flag to indicate that the ball is moving
                        IsBallMoving = true;                        
                        BallHitSO.Raise();

                        Debug.Log(iNumber);
                        iNumber += 1;
                    }
                }
            }

            if (IsBallMoving)
            {
                SetBallMove();
            }
        }


        void SetBallMove()
        {
            // Get the velocity of the GameObject
            Vector3 velocity = _rigidBody.velocity;
            // Calculate the speed by taking the magnitude of the velocity
            float speed = velocity.magnitude;

            if (minimalSpeed < speed)
            {
                //isBallMoving = true;
            }
            else
            {
                IsBallMoving = false;
            }

        }


        //private void OnCollisionEnter(Collision collision)
        //{

        //}


    }
}
