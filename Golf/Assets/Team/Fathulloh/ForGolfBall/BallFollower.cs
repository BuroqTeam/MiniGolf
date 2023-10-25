using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class BallFollower : MonoBehaviour
    {
        public Transform GolfBall;
        private Vector3 StandartOffset = new(0, 0.15f, -0.85f);

        void Start()
        {
            
        }

        
        void Update()
        {
            SetFollowerPos();
        }


        void SetFollowerPos()
        {
            transform.position = GolfBall.transform.position;
            transform.Translate(StandartOffset);
        }

    }
}
