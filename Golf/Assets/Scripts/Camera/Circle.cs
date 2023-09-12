using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MiniGolf
{
    public class Circle : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _ball;

        private SpriteRenderer _spriteRenderer;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        private void Update()
        {
            transform.Rotate(_rotation * Time.deltaTime);
            if (!_ball.GetComponent<Ball>().IsBallMoving && !_ball.GetComponent<Ball>().IsBallClicked)
            {
                _spriteRenderer.enabled = true;
            }
            else
            {
                _spriteRenderer.enabled = false;
            }
        }
    }

}


