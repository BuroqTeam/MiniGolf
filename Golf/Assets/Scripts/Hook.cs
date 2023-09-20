using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hook : MonoBehaviour
{

    public TMP_Text ID;


    public void GetUserID(string userID)
    {
        ID.text = userID;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
