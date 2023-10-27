using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Obstacle
{
    public GameEvent WaterSO;
    public GameObject WaterParticle;


    public override void StopBall(float amount)
    {
        
        WaterSO.Raise();
        // Instantiate(WaterParticle);
        Debug.Log("Water Collistion Action");
        //GameManager.Instace.BallMove.Sink();
                       
    }
}
