using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RtsCamera : MonoBehaviour
{
    public float panSpeed = 5f;             // Speed of camera panning.
    public float pinchZoomSpeed = 0.1f;     // Speed of camera pinch zoom.
    public float rotationSpeed = 2f;        // Speed of camera rotation.
    public float minY = 10f;                // Minimum camera height.
    public float maxY = 80f;                // Maximum camera height.

    private Vector2 touchStartPos;
    private float startPinchDistance;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleTouchInput();
        HandlePinchZoom();
        HandleRotation();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDelta = touch.position - touchStartPos;
                Vector3 translation = new Vector3(-touchDelta.x * panSpeed * Time.deltaTime, 0, -touchDelta.y * panSpeed * Time.deltaTime);
                mainCamera.transform.Translate(translation);
            }
        }
    }

    void HandlePinchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                startPinchDistance = Vector2.Distance(touch1.position, touch2.position);
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float currentPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float pinchAmount = startPinchDistance - currentPinchDistance;

                Vector3 pos = mainCamera.transform.position;
                pos.y += pinchAmount * pinchZoomSpeed * Time.deltaTime;
                pos.y = Mathf.Clamp(pos.y, minY, maxY);
                mainCamera.transform.position = pos;
            }
        }
    }

    void HandleRotation()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
            {
                Vector2 previousTouch1Pos = touch1.position - touch1.deltaPosition;
                Vector2 previousTouch2Pos = touch2.position - touch2.deltaPosition;

                float angleDelta = Vector2.Angle(previousTouch2Pos - previousTouch1Pos, touch2.position - touch1.position);
                Vector3 cameraRotation = new Vector3(0, angleDelta * rotationSpeed * Time.deltaTime, 0);
                mainCamera.transform.Rotate(cameraRotation);
            }
        }
    }
}
