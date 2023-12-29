using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttributes : MonoBehaviour
{
    [Header("Int")]
    public int NumberA = 12;

    [ShowInInspector]
    private int NumberB = 25;

    [Space]
    [ShowInInspector]
    private static int LevelNumber = 7;

    [Header("String")]
    //[Space]
    public string TextA = "Learn";
    [ShowInInspector]
    private string TextB = "Odin Asset";


    [GUIColor(0.92f, 0.18f, 0.12f)]
    public string Maximum = "Maximum";
    [GUIColor(0.92f, 0.79f, 0.12f)]
    public string Medium = "Medium";
    [GUIColor(0.12f, 0.97f, 0.17f)]
    public string Minimum = "Minimum";


    [Button("Text Console")]
    void MessageOfButton()
    {
        Debug.Log("Function of Button");
    }




    /*
     //[Header("Vertital Group Title")]
    //[HorizontalGroup("Color2", Title = "Vertital Group Title")]

    //[VerticalGroup("Color2/Left")]
    //public string Blue;
    //[VerticalGroup("Color2/Left")]
    //public string Yellow;
    //[VerticalGroup("Color2/Left")]
    //public string Red;
     */
}
