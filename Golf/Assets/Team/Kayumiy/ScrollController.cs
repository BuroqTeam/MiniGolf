using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollController : MonoBehaviour
{

    public Camera MainCamera;
    public Ball BallObj;

    

    float _initialFieldView;

    private void Awake()
    {
        _initialFieldView = MainCamera.fieldOfView;
    }





    void Update()
    {
        // Get the scroll input from the mouse
        Vector2 scrollDelta = Input.mouseScrollDelta;

        if (!BallObj.IsBallClicked)
        {
            // Check if there was any scroll input
            if (scrollDelta != Vector2.zero)
            {
                // Scroll up
                if (scrollDelta.y > 0)
                {                                       
                    if (_initialFieldView >= 67.66f)
                    {
                        _initialFieldView -= 7.66f;
                        MainCamera.DOFieldOfView(_initialFieldView, 0.7f);                                                
                    }                    
                    
                }
                // Scroll down
                else if (scrollDelta.y < 0)
                {                    
                    if (_initialFieldView <= 75.34f)
                    {
                        _initialFieldView += 7.66f;
                        MainCamera.DOFieldOfView(_initialFieldView, 0.7f);
                    }                    
                }
            }
        }
        
    }
}
