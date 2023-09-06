using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallLine_Fathulloh
{
    /// <summary>
    /// LineRendererni boshqarish uchun script. Kutilgandek ishlamayabdi.
    /// </summary>
    public class LineManage : MonoBehaviour
    {
        public Camera MainCamera;        

        private LineRenderer lineRenderer;
        [SerializeField] private Vector3 startPoint;
        [SerializeField] private Vector3 endPoint;
        private bool isDrawing = false;
        public GameObject MainBall;
        
        /*[SerializeField]*/ private Vector3 offset;
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
                offset = - GetMouseWorldPos();
                
                //startPoint = MainCamera.ScreenToViewportPoint(Input.mousePosition - MousePos);
                startPoint = MainBall.transform.position;
                //startPoint.z = 0.01f; // Ensure the z-coordinate is appropriate for your scene
                isDrawing = true;
            }

            if (isDrawing && Input.GetMouseButton(0))
            {   // Update the end point while holding down the mouse button
                Vector3 newPos = offset + GetMouseWorldPos();
                endPoint = newPos; //MainCamera.ScreenToViewportPoint(Input.mousePosition);

                //endPoint.z = 0.01f; // Ensure the z-coordinate is appropriate for your scene
                //Debug.Log(endPoint);
            }

            if (Input.GetMouseButtonUp(0))
            {   // Finish drawing when the mouse button is released
                endPoint = MainCamera.ScreenToViewportPoint(Input.mousePosition);
                //endPoint.z = 0.01f; // Ensure the z-coordinate is appropriate for your scene
                isDrawing = false;
                _isFirstTime = false;
            }

            
            if (isDrawing)  // Update LineRenderer positions
            {
                lineRenderer.positionCount = 2;
                if (_isFirstTime)
                {
                    lineRenderer.SetPosition(0, new Vector3(startPoint.x, 0.01f, startPoint.z));
                    lineRenderer.SetPosition(1, new Vector3(endPoint.x, 0.01f, endPoint.y));
                }
                else if (!_isFirstTime)
                {
                    lineRenderer.SetPosition(0, new Vector3(startPoint.x, 0.01f, startPoint.z));
                    lineRenderer.SetPosition(1, new Vector3(startPoint.x - (endPoint.x) / 12, 0.01f, startPoint.z - (endPoint.y + endPoint.z) / 10));
                }                
            }
            else
            {                
                lineRenderer.positionCount = 0;
            }
        }
        

        Vector3 GetMouseWorldPos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -MainCamera.GetComponent<Transform>().transform.position.z;            
            return MainCamera.ScreenToWorldPoint(mousePos);
        }
        

    }
}
