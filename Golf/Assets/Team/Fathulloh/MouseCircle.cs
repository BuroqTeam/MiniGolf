using UnityEngine;

/// <summary>
/// Ballni ustiga bosib turib mouseni qimirlatganda pozitsiyasi o‘zgaradigan va line rangi bilan bir xil rang oladigan Circle uchun script.
/// </summary>
public class MouseCircle : MonoBehaviour
{
    public Camera MainCamera;
    public Ball GolfBall;
    private SpriteRenderer _spriteRenderer;

    LineRenderer _lineRenderer;
    bool _isDrawingLine;
    float maxLengthOfLine;
    float colorfulLineDistance = 0.0145f;


    void Start()
    {        
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _lineRenderer = GolfBall.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();

        maxLengthOfLine = GolfBall.transform.GetChild(0).gameObject.GetComponent<LineDrawer>().maxLengthOfLine;
        //colorfulLineDistance = 0.013f;
        //Debug.Log("Color = " + _lineRenderer.material.color);
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
                    //_lineRenderer.enabled = false;
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

                    //float dist = Vector3.Distance(GolfBall.transform.position, currentMousePosition);
                    //if (dist <= maxLengthOfLine + colorfulLineDistance)
                    //{
                    //    transform.position = currentMousePosition;
                    //}

                    transform.position = currentMousePosition;//o'chiriladi
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
}
