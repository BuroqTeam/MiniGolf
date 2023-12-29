#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(StrValidator))]
/// <summary>
/// Validator third video. I make this script for learning. 
/// Work.
/// </summary>
public class StrValidator : ValueValidator<SomeStrings> // This Validator give a messege when strings are empty. 
{
    protected override void Validate(ValidationResult result)
    {

        //if (this.Value.String1 == "")
        //    result.AddError("String1 has not been null");

        //if (this.Value.String2 == "")
        //    result.AddError("String2 has not been null");

        //if (this.Value.String3 == "")
        //    result.AddError("String3 has not been null");

    }
}
#endif
