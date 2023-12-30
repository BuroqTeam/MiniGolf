using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ISelf Validator 
/// </summary>
public class ExampleIselfValidator : MonoBehaviour, ISelfValidator
{
    [SerializeField]
    private List<GameObject> prefabs;

    [SerializeField]
    private float timeDelay;


    public void Validate(SelfValidationResult result)
    {
        if (timeDelay <= 0)
            result.AddError("timeDelay is eror");


        if (prefabs == null || prefabs.Count == 0)
            result.AddError("No prefab assigned to gameObject.");
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
