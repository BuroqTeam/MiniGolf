using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.name == "end")
    //    {
    //        Debug.Log(111);
    //    }
    //}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
