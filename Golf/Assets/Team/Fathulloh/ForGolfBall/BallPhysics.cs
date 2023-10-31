using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class BallPhysics : MonoBehaviour
    {
        //[SerializeField] private GolfBall _golfBall;
        [SerializeField] private BallMovement _ballMove;
        [SerializeField] private Rigidbody _rigidbody;
        private float _initialMass;
        private float _initialDrag;

        public BallDataSO BallData;

        public Vector3 velocity;
        public float speed = 0;
        public float maxSpeed;
        public string stopping;


        private void Awake()
        {
            _initialMass = _rigidbody.mass;
            _initialDrag = _rigidbody.drag;
        }


        private void Update()
        {
            ChangeBallPhysics();
        }


        /// <summary>
        /// Agar ballning tezligi belgilangan eng kichik qiymatdan pastga tushib ketsa tezroq sekinlashishi va to'xtashi uchun "drag ning qiymati oshiriladi.
        /// </summary>
        public void ChangeBallPhysics()
        {
            velocity = _rigidbody.velocity;   // Get the velocity of the GameObject            
            speed = velocity.magnitude;       // Calculate the speed by taking the magnitude of the velocity

            if (maxSpeed == 0 || maxSpeed < speed)
            {
                maxSpeed = speed;
            }

            if (BallData.MinimalSpeed < speed)
            {
                _ballMove.IsBallMoving = true;
                stopping = "Yurish";
            }
            else if (BallData.MinimalSpeed > speed && speed != 0)
            {
                _rigidbody.drag += 0.5f;
                Debug.Log(_rigidbody.drag);
            }
            else if (speed == 0)
            {
                _ballMove.IsBallMoving = false;
                _rigidbody.drag = _initialDrag;
                stopping = "To'xtash";
            }
        }

        
    }
}
