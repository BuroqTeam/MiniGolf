#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;

[assembly: RegisterValidator(typeof(StatValidator))]
/// <summary>
/// Validator third video.
/// Work
/// </summary>
public class StatValidator : ValueValidator<Stat>
{
    protected override void Validate(ValidationResult result) // If StatType set none give warning and error messege
    {
        if (this.Value.statType == StatType.none)        
            result.AddError("Stat type has not been set.");


        if (this.Value.statValue <= 0)
            result.AddWarning("Stat value should be great than zero.");

    }
}
#endif
