#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;

[assembly: RegisterValidator(typeof(StringValidator))]
/// <summary>
/// Validator third video.
/// Don't work
/// </summary>
public class StringValidator : ValueValidator<string>
{
    protected override void Validate(ValidationResult result)
    {

        if (string.IsNullOrEmpty(this.Value))
        {
            return;
        }

        if (this.Value.Contains("Sirenix"))
        {
            result.AddWarning("Sirenix should be capitalized. ");
        }


        //var val = this.Value;
        
        //if (val has something wrong with it)
        //{
        //    result.AddError("Something is wrong");
        //}
    }
}
#endif
