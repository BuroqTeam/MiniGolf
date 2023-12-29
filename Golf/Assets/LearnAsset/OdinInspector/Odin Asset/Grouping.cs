using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grouping : MonoBehaviour
{
    [HorizontalGroup("First", Title = "Horizontal Group")]
    public string Name1;
    [HorizontalGroup("First")]
    public int Health1;
    [HorizontalGroup("First")]
    public float Attack1;

    [Header("Vertical Group")]
    [VerticalGroup("Second")]
    public string Name2;
    [VerticalGroup("Second")]
    public int Health2;
    [VerticalGroup("Second")]
    public float Attack2;



    
    [HorizontalGroup("Color1", Title = "Horizontal Color")]
    public Color[] BlueGroup1 = { new Color(0.02f, 0.44f, 0.87f, 1), new Color(0.21f, 0.55f, 0.89f, 1), new Color(0.37f, 0.64f, 0.91f, 1) };
    [HorizontalGroup("Color1")]
    public Color[] YellowGroup1 = { new Color(0.96f, 0.8f, 0, 1), new Color(0.96f, 0.83f, 0.2f, 1), new Color(0.97f, 0.87f, 0.35f, 1) };
    [HorizontalGroup("Color1")]
    public Color[] RedGroup1 = { new Color(0.96f, 0.12f, 0.12f, 1), new Color(0.97f, 0.29f, 0.29f, 1), new Color(0.97f, 0.43f, 0.43f, 1) };


    [Header("Vertical Group")]
    [VerticalGroup("Color2")]
    public Color[] BlueGroup2 = { new Color(0.02f, 0.44f, 0.87f, 1), new Color(0.21f, 0.55f, 0.89f, 1), new Color(0.37f, 0.64f, 0.91f, 1) };
    [VerticalGroup("Color2")]
    public Color[] YellowGroup2 = { new Color(0.96f, 0.8f, 0, 1), new Color(0.96f, 0.83f, 0.2f, 1), new Color(0.97f, 0.87f, 0.35f, 1) };
    [VerticalGroup("Color2")]
    public Color[] RedGroup2 = { new Color(0.96f, 0.12f, 0.12f, 1), new Color(0.97f, 0.29f, 0.29f, 1), new Color(0.97f, 0.43f, 0.43f, 1) };

}


