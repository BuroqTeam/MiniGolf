using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat : MonoBehaviour
{
    [Header("If StatType is equal none print error messege")]
    public StatType statType;

    [Header("If StatValue is equal or smallar than zero print error messege.")]
    public int statValue;
}

public enum StatType
{
    none,
    maxSpeed,
    damage,
    armor,
    hitPoints
}
