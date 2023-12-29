#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[assembly: RegisterValidator(typeof(NavMeshAgentValidator))]
/// <summary>
/// Validator second video.
/// </summary>
public class NavMeshAgentValidator : RootObjectValidator<NavMeshAgent> // second video
{
    protected override void Validate(ValidationResult result)
    {
        Vector3 position = this.Object.transform.position;
        float maxDistance = this.Object.radius;

        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, maxDistance, NavMesh.AllAreas))
            result.AddWarning("Nav mesh agent is not on a nav mesh.");

                
    }
}
#endif
