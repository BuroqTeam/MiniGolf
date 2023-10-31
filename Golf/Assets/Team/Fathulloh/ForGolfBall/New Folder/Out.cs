using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class Out : Obstacle
    {
        public GameEvent OutSO;
        public GameObject OutParticle; // outda hechqanday particle yo'q


        public override void StopBall(float amount)
        {
            OutSO.Raise();
            // Instantiate(OutParticle);
            Debug.Log("Out Collistion Action");
            //GameManager.Instance.BallMove.Sink();
        }
    }
}
