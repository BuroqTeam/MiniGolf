using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "ScriptableObjects/OdinValidator/ItemDataSO", order = 18)]
public class ItemData : ScriptableObject
{
    public string Name;
    public string Description;
    public int Damage;
}
