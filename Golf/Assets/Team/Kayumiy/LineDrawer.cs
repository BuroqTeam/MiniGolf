using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public Camera MainCamera;

    private LineRenderer _lineRenderer;    
    private bool _isDrawingLine = false;

    Ball _ball;
    


    void Start()
    {
        _ball = transform.parent.GetComponent<Ball>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, transform.position);        
        _lineRenderer.enabled = false;
    }

    void Update()
    {

        if (!_ball.IsBallMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name.Equals("Ball"))
                {
                    _isDrawingLine = true;
                    _lineRenderer.SetPosition(0, transform.position);
                    _lineRenderer.enabled = false;
                }
            }

            if (Input.GetMouseButton(0) && _isDrawingLine)
            {

                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 currentMousePosition = hit.point;
                    
                    currentMousePosition.y = transform.position.y; // 0
                    _lineRenderer.SetPosition(1, currentMousePosition);
                    _lineRenderer.enabled = true;
                }
            }

            if (Input.GetMouseButtonUp(0) && _isDrawingLine)
            {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.enabled = false;
                _isDrawingLine = false;

                Vector3 startPoint = _lineRenderer.GetPosition(0);
                Vector3 endPoint = _lineRenderer.GetPosition(1);

                // Calculate the direction vector.
                Vector3 direction = startPoint - endPoint;

                // Normalize the direction vector if you want a unit vector.
                direction.Normalize();
                GetComponent<AudioSource>().Play();
                _ball.gameObject.GetComponent<Rigidbody>().AddForce(direction * 1000);
            }

        }

        
    }

    
    public void ChangeLineColor(Color newColor)// for color change
    {
        Material newMat = (_lineRenderer.material);    
        newMat.color = newColor;
        _lineRenderer.material = newMat;
    }

}
