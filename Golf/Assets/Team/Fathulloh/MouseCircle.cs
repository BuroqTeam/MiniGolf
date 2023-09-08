using UnityEngine;

/// <summary>
/// Ballni ustiga bosib turib mouseni qimirlatganda pozitsiyasi o‘zgaradigan va line rangi bilan bir xil rang oladigan Circle uchun script.
/// </summary>
public class MouseCircle : MonoBehaviour
{
    public Camera MainCamera;
    public Ball GolfBall;
    private SpriteRenderer _spriteRenderer;

    private LineRenderer _lineRenderer;
    bool _isDrawingLine;
    //float colorfulLineDistance = 0.0145f;


    void Start()
    {        
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _lineRenderer = GolfBall.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        if (!GolfBall.IsBallMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name.Equals("Ball"))
                {
                    _isDrawingLine = true;
                    _spriteRenderer.enabled = false;
                    transform.position = GolfBall.transform.position;
                }
            }


            if (Input.GetMouseButton(0) && _isDrawingLine)
            {
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 currentMousePosition = hit.point;
                    currentMousePosition.y = GolfBall.transform.position.y;
                    _spriteRenderer.enabled = true;
                    //transform.position = currentMousePosition;//o'chiriladi

                    float lengthLine = Vector3.Distance(currentMousePosition, _lineRenderer.GetPosition(0));

                    if (lengthLine >= 0.40f)
                    {
                        transform.position = FindPointOnLine(GolfBall.transform.position, currentMousePosition, 0.40f);                       
                    }
                    else
                    {
                        transform.position = currentMousePosition;
                    }
                    
                    _spriteRenderer.color = _lineRenderer.material.color;
                }
            }


            if (Input.GetMouseButtonUp(0) && _isDrawingLine)
            {
                _isDrawingLine = false;
                _spriteRenderer.enabled = false;
            }

        }
    }


    /// <summary>
    /// Ikkita nuqta berilgan. Birinchi va ikkinchi nuqtalar orasida joylashgan va birinchi nuqtadan x masofada joylashgan uchinchi nuqtani topish.
    /// </summary>
    /// <param name="point1">Birinchi nuqtaning kordinatasi</param>
    /// <param name="point2">Ikkinchi nuqtaning kordinatasi</param>
    /// <param name="distance">Birinchi nuqtadan maksimal masofa</param>
    /// <returns></returns>
    Vector3 FindPointOnLine(Vector3 point1, Vector3 point2, float distance)
    {
        float totalDistance = Vector3.Distance(point1, point2);
        float ratio = distance / totalDistance;

        float newX = point1.x + ratio * (point2.x - point1.x);
        float newY = point1.y /*+ ratio * (point2.y - point1.y)*/;
        float newZ = point1.z + ratio * (point2.z - point1.z);

        return new Vector3(newX, newY, newZ);
    }

}
