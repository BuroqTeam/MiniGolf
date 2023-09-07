using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ballni ustiga bosib turib mouseni qimirlatganda pozitsiyasi o‘zgaradigan va line rangi bilan bir xil rang oladigan Circle uchun script.
/// </summary>
public class MouseCircle : MonoBehaviour
{
    public Camera MainCamera;
    public Ball GolfBall;
    private SpriteRenderer _spriteRenderer;


    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if (GolfBall.IsBallClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //transform.position = 
                //Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                
            }

            if (Input.GetMouseButton(0))
            {
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 currentMousePosition = hit.point;

                    currentMousePosition.y = GolfBall.transform.position.y;
                    transform.position = currentMousePosition;
                    //_lineRenderer.SetPosition(1, currentMousePosition);
                    //_lineRenderer.enabled = true;
                }
            }



        }
    }
}
