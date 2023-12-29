using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Validator fourth video.
/// </summary>
public class InputManager : MonoBehaviour
{
    [NeedsComponent(typeof(Rigidbody))]// Berilgan komponenta obyektda mavjud bo'lmasa Error chiqaradi.
    public GameObject player;


    [NeedsComponent(typeof(SomeStrings))]
    public GameObject secondPlayer;

}
