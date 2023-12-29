using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleEnemyDataSO", menuName = "ScriptableObjects/SimpleEnemyDataSO", order = 21)]
public class SimpleEnemyData : ScriptableObject
{            
    public string enemyName;                 
    public string description;
    
    public GameObject enemyModel;

    public int health = 20;
    
    public float speed = 2f;
    public float detectRange = 10f;
    public int damage = 1;
}
