using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class add to Wall and Land objects. Called only one time when hitted with ball object.
/// </summary>
public class BallTrigger : MonoBehaviour
{
    public GameEvent EventSO;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            EventSO.Raise();
        }
    }
}
