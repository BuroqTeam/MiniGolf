using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : Obstacle
{

    public GameEvent SandSO;
    public GameObject SandParticle;

    public override void StopBall(float amount)
    {
        
        SandSO.Raise();
        Instantiate(SandParticle);
        Debug.Log("Sand Collistion Action");
        // GameManager.Instace.BallMove.WalkInSand();
    }


    //public override void StopBall(float amount)
    //{

    //    SandSO.Raise();
    //    Instantiate(SandParticle);
    //    Debug.Log("Snoq Collistion Action");
    //    // GameManager.Instace.BallMove.WalkInSnow();
    //}

    //public override void StopBall(float amount)
    //{

    //    SandSO.Raise();        
    //    Debug.Log("Out Collistion Action");
    //    //GameManager.Instace.BallMove.Sink(); // BallMove nomli scriptning ichida Sink() metodi bor. Uni GameManager orqali chaqirib ishlatayabmiz. 
    //}

}
