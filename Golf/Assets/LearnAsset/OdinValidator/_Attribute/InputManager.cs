using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class InputManager : MonoBehaviour
{
    [Header("Rigidbody")]
    [NeedsComponent(typeof(Rigidbody))]
    public GameObject player;

    [Header("PlayerMove")]
    [NeedsComponent(typeof(PlayerMove))]
    public GameObject secondPlayer;

}
