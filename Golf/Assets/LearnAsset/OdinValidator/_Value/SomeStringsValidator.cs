#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;

[assembly: RegisterValidator(typeof(SomeStringsValidator))]
/// <summary>
/// Validator third video. I make this script for learning. 
/// Work.
/// </summary>
public class SomeStringsValidator : ValueValidator<SomeStrings>
{
    protected override void Validate(ValidationResult result)
    {
        if (this.Value.String1 == "")
            result.AddError("String1 is null");

        if (this.Value.String2 == "")
            result.AddError("String2 has not been null");

        if (this.Value.String3 == "")
            result.AddError("String3 is null");

    }
}
#endif
