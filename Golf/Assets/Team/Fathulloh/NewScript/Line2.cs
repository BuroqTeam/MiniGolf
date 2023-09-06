using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallLine_Fathulloh
{
    public class Line2 : MonoBehaviour
    {
        public Camera MainCamera;
                
        [SerializeField] private Vector3 startPoint;
        [SerializeField] private Vector3 endPoint;
        private bool isDrawing = false;
        /*public*/ LineRenderer lineRenderer;

        public GameObject MainBall;
        public GameObject CircleObj;
        private Vector3 MousePos;
        bool _isFirstTime = true;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {   // Set the start point when the mouse button is pressed
                //startPoint = MainCamera.ScreenToViewportPoint(Input.mousePosition);
                startPoint = MainBall.transform.position;
                startPoint.z = 0; // Ensure the z-coordinate is appropriate for your scene
                isDrawing = true;
                MousePos = Input.mousePosition - GetMousePos();
            }

            if (isDrawing && Input.GetMouseButton(0))
            {   // Update the end point while holding down the mouse button
                //endPoint = MainCamera.ScreenToViewportPoint(Input.mousePosition);
                
                endPoint = MainCamera.ScreenToWorldPoint(Input.mousePosition - MousePos);
                
                //endPoint.z = 0; // Ensure the z-coordinate is appropriate for your scene
                Debug.Log(endPoint);
            }

            if (Input.GetMouseButtonUp(0))
            {   // Finish drawing when the mouse button is released
                endPoint = MainCamera.ScreenToViewportPoint(Input.mousePosition);
                //endPoint.z = 0; // Ensure the z-coordinate is appropriate for your scene
                isDrawing = false;
                _isFirstTime = false;
            }

            
            if (isDrawing) // Update LineRenderer positions
            {
                lineRenderer.positionCount = 2;
                if (_isFirstTime)
                {                    
                    lineRenderer.SetPosition(0, new Vector3(startPoint.x, 0.01f, startPoint.z));
                    lineRenderer.SetPosition(1, new Vector3(endPoint.x, 0.01f, endPoint.y + endPoint.z));
                }
                else
                {
                    lineRenderer.SetPosition(0, new Vector3(startPoint.x, 0.01f, startPoint.z));
                    lineRenderer.SetPosition(1, new Vector3(startPoint.x - endPoint.x, 0.01f, startPoint.z - endPoint.y));
                }                
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }

        
        private Vector3 GetMousePos() // Amalni bajaradi va Vector3 tipli qiymat qaytaradi.
        {
            //Debug.Log("GetMousePos() ");
            return MainCamera.WorldToScreenPoint(CircleObj.transform.position);
        }

    }
}
