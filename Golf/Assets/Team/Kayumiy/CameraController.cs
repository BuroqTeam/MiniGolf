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
       


    }

    private void Start()
    {
        _offset = transform.position - Ball.transform.position;
        transform.position = _offset;

        transform.position = Ball.position;
        transform.Translate(new Vector3(0, 0.15f, -0.55f));
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            

        }

        if (Input.GetMouseButton(0))
        {
            if (!Ball.GetComponent<Ball>().IsBallClicked)
            {
                Vector3 direction = _previousPosition - _mainCamera.ScreenToViewportPoint(Input.mousePosition);

                transform.position = Ball.position;



                // Rotate in Vertical Axis
                transform.Rotate(Vector3.right, direction.y * 180);
                float verticalAngle = transform.rotation.eulerAngles.x;
                verticalAngle = Mathf.Clamp(verticalAngle, 5, 30);


                // Rotate in Horizontal Axis
                transform.Rotate(Vector3.up, direction.x * -180, Space.World);
                float horizontalAngle = transform.rotation.eulerAngles.y;

                //transform.eulerAngles = new Vector3(verticalAngle, horizontalAngle, 0);
                transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);



                // Move to near Ball

                transform.Translate(new Vector3(0, 0.15f, -0.55f));



                _previousPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);

            }
            

            
        }

    }


   


}
