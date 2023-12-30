#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using MiniGolf;
using System.Collections.Generic;

[assembly: RegisterValidationRule(typeof(GolfSceneValidatorRule))]

public class GolfSceneValidatorRule : SceneValidator
{

    private static string gameObjectName = "Ball";
    public List<string> requiredRootObjects = new List<string>();

    public override bool CanValidateScene(SceneReference scene)
    {
        if (!scene.GetSceneAsset().name.Contains("Level"))
            return false;

        if (!scene.Path.Contains("Assets/_Scenes/Levels/"))
            return false;

        return base.CanValidateScene(scene);
    }



    protected override void Validate(ValidationResult result)
    {
        if (!FindComponentInSceneOfType<Rigidbody2D>())
            result.AddError("No Rigidbody2d found.");

        if (!FindComponentInSceneOfType<Image>())
            result.AddError("No Image found.");


        if (!GetGameObjectAtPath(gameObjectName))
            result.AddError($"No object found named {gameObjectName}");
        else if (!GetComponentAtPath<GameManager>(gameObjectName))
            result.AddWarning($"No GameManager on the gameObject {gameObjectName}.");


        foreach (var requiredObj in requiredRootObjects)
        {
            GameObject required = GetGameObjectAtPath(requiredObj);
            if (required != null || !GetSceneRoots().Contains(required))
                result.AddError($"{requiredObj} is missing or is not a root object in the scene");
        }

    }
}
#endif