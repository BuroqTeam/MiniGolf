using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchToZoom : MonoBehaviour
{
    public float zoomSpeed = 0.5f; // Zoom speed factor
    public float minFOV = 10.0f;  // Minimum FOV for zooming in
    public float maxFOV = 60.0f;  // Maximum FOV for zooming out

    private Camera mainCamera;
    private Vector2 touchStartPos;
    private float initialFOV;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        initialFOV = mainCamera.fieldOfView;
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            // Get the two touch positions
            Vector2 touch1 = Input.GetTouch(0).position;
            Vector2 touch2 = Input.GetTouch(1).position;

            // Calculate the initial distance between touches
            float initialDistance = Vector2.Distance(touch1, touch2);

            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                // Store the initial FOV when the pinch gesture begins
                initialFOV = mainCamera.fieldOfView;
                touchStartPos = (touch1 + touch2) / 2;
            }

            // Calculate the current distance between touches
            float currentDistance = Vector2.Distance(touch1, touch2);

            // Calculate the zoom factor based on the change in distance
            float zoomFactor = initialDistance / currentDistance;

            // Apply the zoom by adjusting the FOV
            float newFOV = initialFOV * zoomFactor;

            // Clamp the FOV within the specified range
            newFOV = Mathf.Clamp(newFOV, minFOV, maxFOV);

            // Set the camera's FOV to the new value
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, newFOV, zoomSpeed * Time.deltaTime);
        }
    }
}
