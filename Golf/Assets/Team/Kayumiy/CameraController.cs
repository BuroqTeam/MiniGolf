using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{

    public Transform Ball;

    private Vector3 _offset;
    private Vector3 _previousPosition;
    Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
        _offset = transform.position - Ball.transform.position;

        transform.position = Ball.position;
        transform.Translate(_offset);


    }


    // Update is called once per frame
    void Update()
    {


        //transform.RotateAround(Ball.position, Vector3.up, 20 * Time.deltaTime);
        //transform.position = Ball.transform.position + _offset;

        if (Input.GetMouseButtonDown(0))
        {
            _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);

        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = _previousPosition - _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            transform.position = Ball.position;
           
            // Rotate in Vertical Axis
            transform.Rotate(Vector3.right, direction.y * 180);
            float verticalAngle = transform.rotation.eulerAngles.x;
            verticalAngle = Mathf.Clamp(verticalAngle, 5, 40);



            // Rotate in Horizontal Axis
            transform.Rotate(Vector3.up, direction.x * -180, Space.World);
            float horizontalAngle = transform.rotation.eulerAngles.y;            
            transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

          

            // Move to near Ball
            transform.Translate(_offset);
            _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }

    }


   


}
