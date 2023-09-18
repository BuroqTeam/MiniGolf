using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FinishFlagning ostidagi circle animation uchun.
/// </summary>
public class RotateCurrentObj : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }
}
