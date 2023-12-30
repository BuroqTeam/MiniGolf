#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;

[assembly: RegisterValidator(typeof(ItemDataValidator))]

public class ItemDataValidator : RootObjectValidator<ItemData>
{
    protected override void Validate(ValidationResult result)
    {
        if (string.IsNullOrEmpty(this.Value.Name))
        {
            result.AddWarning("Name is empty");
        }

        if (string.IsNullOrEmpty(this.Value.Description))
        {
            result.AddWarning("Description is empty");
        }

        if (this.Value.Damage > 100 || this.Value.Damage <= 0)
        {
            result.AddWarning("Damage is error value.");
        }

    }
}
#endif
