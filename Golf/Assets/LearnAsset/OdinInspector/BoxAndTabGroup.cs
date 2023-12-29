using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAndTabGroup : MonoBehaviour
{
    [BoxGroup]
    public string FirstName;
    [BoxGroup]
    public string LastName;
    [BoxGroup]
    public int Age;
    [BoxGroup]
    public float Weight;

    //[BoxGroup("BoxBase/Box 1")]   // BoxBase is base group name, Column1 is column  name.
    //public string e;
    //[BoxGroup("BoxBase/Box 1")]   // BoxBase is base group name, Column1 is column  name.
    //public string f;



}
