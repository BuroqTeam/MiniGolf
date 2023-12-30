using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Validator fifth video.
/// This script is responsible for check variable is correct or not!
/// 
/// </summary>
public class Spawner : MonoBehaviour, ISelfValidator
{
    [SerializeField]
    private float spawnDelay;

    [SerializeField]
    private List<GameObject> prefabs;

    //public List<GameObject> newPrefabs;

    //private void Awake()
    //{
    //    Debug.Log(newPrefabs.Count == 0);
    //    Debug.Log(newPrefabs == null);
    //}

    public void Validate(SelfValidationResult result)
    {
        //if (!NavMesh.SamplePosition(this.transform.position, out NavMeshHit hit, 0.25f, NavMesh.AllAreas))        
        //    result.AddError("Spawner is not on a nav mesh.");

        if (spawnDelay <= 0f)              // spawnDelay 0 ga teng bo'lsa yoki 0 dan kichik bo'lsa Error chiqadi.  
            result.AddError("The spawn delay needs to be greater than zero.");

        if (prefabs == null || prefabs.Count == 0)   // List bo'sh bo'lsa Error qaytaradi.
            result.AddError("No prefabs assigned to spawner");
        else 
        {
            for (int i = 0; i < prefabs.Count; i++)
            {
                if (prefabs[i] == null)
                    result.AddWarning($"Prefab slot {i} is empty");
            }
        }
        


    }
}
