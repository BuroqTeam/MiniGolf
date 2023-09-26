using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
    
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
