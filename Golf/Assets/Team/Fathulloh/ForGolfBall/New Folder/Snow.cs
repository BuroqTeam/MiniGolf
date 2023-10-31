using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class Snow : Obstacle
    {
        public GameEvent SnowSO;
        public GameObject SnowParticle;

        public override void StopBall(float amount)
        {
            SnowSO.Raise();
            //Instantiate(SnowParticle);
            Debug.Log("Snow Collistion Action");
            //GameManager.Instance.BallMove.WalkInSand(); //Golf Ball snow yoki sandga duch kelsa uning tezligi kamayishi kerak. 
        }

    }
}
