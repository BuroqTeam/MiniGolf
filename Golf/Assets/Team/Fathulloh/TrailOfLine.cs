using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ball yurgan yo‘lda qoladigan iz uchun script.
/// </summary>
public class TrailOfLine : MonoBehaviour
{
    public GameObject Ball;
    private LineRenderer _lineRenderer;

    Vector3 startPoint;
    bool _isFirstTime = true;

    Color _startColor = new Color(0.55f, 0.27f, 0.97f);
    Color _endColor = new Color(0.97f, 0.97f, 0.97f, 0.60f);


    void Start()
    {        
        _lineRenderer = GetComponent<LineRenderer>();
        //_lineRenderer.SetColors(_startColor, _endColor);

        //Material newMat = (_lineRenderer.material);
        //newMat.color = _startColor;
        //_lineRenderer.material = newMat;
        Debug.Log(_lineRenderer.material.color);
    }

    
    void Update()
    {
        if (Ball.GetComponent<Ball>().IsBallMoving)
        {
            _lineRenderer.positionCount = 2;
            Vector3 endPoint = Ball.transform.position;

            if (_isFirstTime)
            {
                startPoint = Ball.transform.position;
                _lineRenderer.SetPosition(0, startPoint);
                _isFirstTime = false;
            }
            else
            {
                Vector3 oldStartPos = _lineRenderer.GetPosition(0);
                _lineRenderer.SetPosition(0, FindPointOnLine(oldStartPos, endPoint, 0.021f));
                _lineRenderer.SetPosition(1, endPoint);
                SetTrail();
            }            
        }
        else
        {            
            if (!_isFirstTime)
            {
                //float distance = Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1));
                //Debug.Log("distance = " + distance);
                _isFirstTime = true;
                _lineRenderer.positionCount = 0;
            }            
        }
    }


    void SetTrail()
    {
        Debug.Log("SetTrail");
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
