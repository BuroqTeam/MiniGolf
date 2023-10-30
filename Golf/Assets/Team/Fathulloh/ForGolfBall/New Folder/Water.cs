using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class Water : Obstacle
    {
        public GameEvent WaterSO;
        public GameObject WaterParticle;


        public override void StopBall(float amount)
        {
            WaterSO.Raise();
            // Instantiate(WaterParticle);
            Debug.Log("Water Collistion Action");
            GameManager.Instance.BallMove.Sink();
        }
    }
}
