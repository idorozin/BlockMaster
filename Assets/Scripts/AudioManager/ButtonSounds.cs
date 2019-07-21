using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour {

	void Start()
	{
		AddSound(SceneManager.GetActiveScene() , LoadSceneMode.Single);
		SceneManager.sceneLoaded += AddSound;
	}

	void AddSound(Scene s , LoadSceneMode m)
	{
		List<Button> buttons = new List<Button>();

		foreach (Button go in Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[])
		{
			if(go.GetComponent<Button>()!=null && !go.CompareTag("noClick"))
				buttons.Add(go.GetComponent<Button>());
		}
		
		foreach (var button in buttons)
		{
			button.GetComponent<Button>().onClick.AddListener(
				()=>AudioManager.Instance.PlaySound(AudioManager.SoundName.buttonClick));
			
		}
	}

}
