using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Ball : MonoBehaviour
{
    public Camera MainCamera;
    public bool IsBallClicked;

    float _initialFieldView;
   

    Vector3 _previousClickPosition = new Vector3();


    private void Awake()
    {
        _initialFieldView = MainCamera.fieldOfView;
        transform.position = new Vector3(0, GetComponent<Renderer>().bounds.size.y * 0.5f, 0);

    }


    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Cast a ray from the camera to the touch position
            Ray ray = MainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Check if the ray hits a GameObject
            if (Physics.Raycast(ray, out hit))
            {
                // The hit.collider.gameObject is the GameObject that was touched
                GameObject touchedObject = hit.collider.gameObject;
                
                if (touchedObject.name.Equals("Ball"))
                {
                    IsBallClicked = true;
                }
            }
        }

        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a GameObject
            if (Physics.Raycast(ray, out hit))
            {
                // The hit.collider.gameObject is the GameObject that was clicked
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.name.Equals("Ball"))
                {
                    IsBallClicked = true;
                    _previousClickPosition = MainCamera.ScreenToViewportPoint(Input.mousePosition);
                }
                else
                {
                    IsBallClicked = false;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (IsBallClicked)
            {
                float distance = Vector3.Distance(_previousClickPosition, MainCamera.ScreenToViewportPoint(Input.mousePosition));
                distance = distance * 100;
                if (distance > 23)
                {
                    distance = 23;                    
                }
                MainCamera.fieldOfView = Mathf.MoveTowards(MainCamera.fieldOfView, _initialFieldView + distance, 20 * Time.deltaTime);

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (IsBallClicked)
            {
                MainCamera.DOFieldOfView(_initialFieldView, 0.25f);
                IsBallClicked = false;
            }
        }
    }
}
