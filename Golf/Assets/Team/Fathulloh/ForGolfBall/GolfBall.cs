using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class GolfBall : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _spheraCollider;

        private bool isMoving = false;
        public float forceMultiplier = 10.0f;

        void Start()
        {

        }

        
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isMoving)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        // Calculate the direction to apply the force
                        Vector3 forceDirection = transform.forward;

                        // Apply force to the golf ball
                        _rigidbody.AddForce(forceDirection * forceMultiplier, ForceMode.Impulse);

                        // Set the flag to indicate that the ball is moving
                        isMoving = true;
                    }
                }
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Hit");
        }


    }
}
