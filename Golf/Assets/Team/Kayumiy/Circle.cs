using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;

    [SerializeField] Camera _camera;
    [SerializeField] GameObject _ball;

    SpriteRenderer _spriteRenderer;

   


    private void Awake()
    {
        transform.position =  Vector3.Lerp(_camera.transform.position, _ball.transform.position, 0.8f);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }



    private void Update()
    {
        transform.Rotate(_rotation * -Time.deltaTime);

        //
        //if (_lineForce.IsIdle)
        //{
        //    _spriteRenderer.enabled = true;
        //}
        //else
        //{
        //    _spriteRenderer.enabled = false;
        //}

        

        



    }



}
