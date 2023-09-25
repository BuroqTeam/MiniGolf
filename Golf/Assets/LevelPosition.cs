using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelPosition : MonoBehaviour
{

    public List<Transform> Lands;

    private void Awake()
    {
        Lands = transform.GetComponentsInChildren<Transform>().ToList();
        Lands.RemoveAt(0);

    }

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 1; i < Lands.Count; i++)
        {
            Debug.Log("Salom");
            Lands[i].position = new Vector3(Lands[i - 1].position.x + Lands[i - 1].GetComponent<MeshRenderer>().bounds.size.x,
                Lands[i].position.y, Lands[i].position.z);

            
        }


       


    }

   
}
