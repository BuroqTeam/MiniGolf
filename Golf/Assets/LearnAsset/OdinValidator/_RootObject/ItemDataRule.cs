#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;

[assembly: RegisterValidationRule(typeof(ItemDataRule), Name = "ItemDataRule", Description = "Some description text.")]

public class ItemDataRule : RootObjectValidator<ItemData>
{
    
    public int SerializedConfig;

    protected override void Validate(ValidationResult result)
    {
        //if (string.IsNullOrEmpty(this.Value.Name))
        //{
        //    result.AddWarning("Please provide a name for the item.");
        //}
        //if (string.IsNullOrEmpty(this.Value.Description))
        //{
        //    result.AddWarning("Please provide a description for the item.");
        //}
        //if (this.Value.Damage > 100 || this.Value.Damage <= 0)
        //{
        //    result.AddWarning("The damage has to be between 0 and 100.");
        //}
    }
}
#endif
