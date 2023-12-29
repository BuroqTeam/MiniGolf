#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;

[assembly: RegisterValidationRule(typeof(LevelSceneValidatorRule))]
/// <summary>
/// Validator 6 video.
/// This validator help to search components in the scene.
/// </summary>
public class LevelSceneValidatorRule : SceneValidator
{    
    //public string SpawnerManagerPath = "Managers/Spawner";


    private static string playerObjectName = "Enviroment";  // gameObject name 
    public List<string> requiredRootObjects = new List<string>();

    public override bool CanValidateScene(SceneReference scene)
    {
        if (!scene.GetSceneAsset().name.Contains("Validator")) // -Level
            return false;
        if (!scene.Path.Contains("Assets/LearnAsset/OdinValidator/"))
            return false;
        return base.CanValidateScene(scene);
    }


    protected override void Validate(ValidationResult result)
    {
        if (!FindComponentInSceneOfType<Camera>())             // Search Camera component 
            result.AddError("No camera found in the scene.");
        
        if (!FindComponentInSceneOfType<Image>())             // Search Image component 
            result.AddError("No Image found in the scene.");

        if (!GetGameObjectAtPath(playerObjectName))            // Search game Object with name.
            result.AddError($"No object found named {playerObjectName}");
        else if (!GetComponentAtPath<InputManager>(playerObjectName))  // Search component from the gameObject          -CharacterController2D
            result.AddWarning($"No InputManager on the game object named {playerObjectName}");


        foreach (var requiredObject in requiredRootObjects)
        {
            GameObject required = GetGameObjectAtPath(requiredObject);
            if (required == null || !GetSceneRoots().Contains(required))
                result.AddError($"{requiredObject} is missing or is not a root object in the scene. {required != null} and {!GetSceneRoots().Contains(required)}");
        }        
    }

    
}
#endif