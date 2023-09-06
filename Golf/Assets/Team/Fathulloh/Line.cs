using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LineSh_Test
{
    public class Line : MonoBehaviour
    {
        public Camera MainCamera;

        private LineRenderer lineRenderer;
        private Vector3 startPoint;
        private Vector3 endPoint;
        private bool isDrawing = false;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Set the start point when the mouse button is pressed
                startPoint = MainCamera.ScreenToViewportPoint(Input.mousePosition);



                startPoint.z = 0; // Ensure the z-coordinate is appropriate for your scene
                isDrawing = true;
            }

            if (isDrawing && Input.GetMouseButton(0))
            {
                // Update the end point while holding down the mouse button
                endPoint = MainCamera.ScreenToViewportPoint(Input.mousePosition);

                endPoint.z = 0; // Ensure the z-coordinate is appropriate for your scene
                Debug.Log(endPoint);

            }

            if (Input.GetMouseButtonUp(0))
            {
                // Finish drawing when the mouse button is released
                endPoint = MainCamera.ScreenToViewportPoint(Input.mousePosition);
                endPoint.z = 0; // Ensure the z-coordinate is appropriate for your scene
                isDrawing = false;
            }

            // Update LineRenderer positions
            if (isDrawing)
            {

                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, startPoint);
                lineRenderer.SetPosition(1, endPoint);
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }
    }
}
