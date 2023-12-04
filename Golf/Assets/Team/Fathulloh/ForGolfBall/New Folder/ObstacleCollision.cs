using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Add to GolfBall. This class work for checking Obstacle and his child class. 
/// GolfBall obyektiga qo'shiladi. Ushbu classda Obstacle classining qaysi childi bilan collision yuz bergani aniqlanadi.
/// </summary>
public class ObstacleCollision : MonoBehaviour
{
    public GameEvent WallEventSO;
    public GameEvent LandEventSO;

    private void OnTriggerEnter(Collider other)
    {        
        var obstacle = other.GetComponent<Obstacle>();
        //obstacle.StopBall(1);
        if (obstacle != null)
        {
            obstacle.StopBall(1);
        }
        //else if (other.gameObject.CompareTag("Wall"))
        //{
        //    Debug.Log("Wall is work");
        //    WallEventSO.Raise();
        //}
        //else if (other.gameObject.CompareTag("Land"))
        //{
        //    LandEventSO.Raise();
        //}
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall is work");
            WallEventSO.Raise();
        }
        else if (collision.gameObject.CompareTag("Land"))
        {
            LandEventSO.Raise();
        }
    }

}
