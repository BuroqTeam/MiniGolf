using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grouping : MonoBehaviour
{    
    [HorizontalGroup("HorizontalColor", Title = "Horizontal Group Title")]
    public List<Color> BlueGroup;

    [HorizontalGroup("HorizontalColor")]
    public List<Color> YellowGroup;

    [HorizontalGroup("HorizontalColor")]
    public List<Color> RedGroup;




    [Header("Vertital Group Title")]
    [VerticalGroup("VerticalColor")]
    public List<Color> BlueGroup1;

    [HorizontalGroup("VerticalColor")]
    public List<Color> YellowGroup1;

    [HorizontalGroup("VerticalColor")]
    public List<Color> RedGroup1;



    

}


