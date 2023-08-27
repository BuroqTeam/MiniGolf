using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

   


    private void Awake()
    {

        

        transform.position = new Vector3(0, GetComponent<Renderer>().bounds.size.y * 0.5f, 0);
        

        
        
    }
    
}
