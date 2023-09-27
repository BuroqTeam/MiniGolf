using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelButton : MonoBehaviour
{
    
    public string SceneToLoad;
    Button _button;

    private void Awake()
    {
        if (SceneToLoad != "Level 1")
        {
            PlayerPrefs.SetInt(SceneToLoad, 0);
        }
        else
        {
            PlayerPrefs.SetInt(SceneToLoad, 1);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TaskOnClick);
        //EnableLevel();

    }


	void TaskOnClick()
	{
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
		
	}


    public void EnableLevel()
    {
        Debug.Log(PlayerPrefs.GetInt(SceneToLoad));
        if (PlayerPrefs.GetInt(SceneToLoad).Equals(1))
        {
            transform.GetChild(2).GetComponent<Image>().enabled = false;
            _button.interactable = true;
        }
        else
        {
            transform.GetChild(2).GetComponent<Image>().enabled = true;
            _button.interactable = false;
        }
    }
}
