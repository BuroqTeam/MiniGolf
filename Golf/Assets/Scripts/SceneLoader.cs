using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public List<string> Scenes = new List<string>();

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }


    public void ReplayCurrentScene()
    {
        string currentScenenName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScenenName, LoadSceneMode.Single);
    }

    public void EnableScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        for (int i = 0; i < Scenes.Count-1; i++)
        {
            
            if (scene.name == Scenes[i])
            {
                string key = Scenes[i + 1];
                //Debug.Log(key);
                PlayerPrefs.SetInt(key, 1);
            }
        }
    
    }


    public void BackToMenu()
    {
        StartCoroutine(SceneWaitToLoad());
    }


    IEnumerator SceneWaitToLoad()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

    }



}
