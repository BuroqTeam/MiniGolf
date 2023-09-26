using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
	public AudioSource BackgroundMusic;


	private Button _button;
	 

	void Start()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		if (_button.image.color.Equals(Color.white))
		{
			BackgroundMusic.Stop();
			_button.image.color = Color.grey;
		}
		else
		{
			BackgroundMusic.Play();
			_button.image.color = Color.white;
		}
		
	}
}
