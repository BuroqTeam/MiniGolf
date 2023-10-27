using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add to GolfBall. This class work for checking Obstacle and his child class. 
/// GolfBall obyektiga qo'shiladi. Ushbu classda Obstacle classining qaysi childi bilan collision yuz bergani aniqlanadi.
/// </summary>
public class ObstacleCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        var obstacle = other.GetComponent<Obstacle>();
        obstacle.StopBall(1);
        
    }
}
