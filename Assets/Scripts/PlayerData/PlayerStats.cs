using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerStats : MonoBehaviour
{
	public static PlayerData Instance;

	[SerializeField]
	private ChallengesTemplates templates;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = new PlayerData();
			path = Application.persistentDataPath + "/PlayerFile_.json";
			if (File.Exists(path)) 
				Debug.Log("file exists");
			else
				saveFile();
			
			loadFile();
			
			if (Instance.challenges == null)
			{
				Instance.challenges = new List<Challenge>();
				foreach (Challenge c in templates.Challenges)
				{
					Instance.challenges.Add(new Challenge(c));
				}
				Instance.ChallengesAvailable = 5;		
				Instance.ActivateChallenge();
			}
			else
				Instance.SubscribeToNewGame();
			DontDestroyOnLoad(gameObject);
			GameObject.Find("MenuCanvas")?.GetComponent<MenuScript>().setRecordText();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	[ContextMenu("file_path")]
	public void printPath()
	{
		Debug.Log(Application.persistentDataPath + "/PlayerFile.json");
	}

	static string path;
	
	#region Save & Load

	public static void loadFile()
	{		
		//read file
		string fileString = File.ReadAllText(path);
		//Serialize object
		JsonConvert.PopulateObject(fileString , Instance);			
		saveFile();
	}
	
	[ContextMenu("Reset_")]
	public void ResetFile()
	{
		//parse this class to json string
		//save json file
		File.Delete(Application.persistentDataPath + "/PlayerFile_.json"); 
	}
	
	public static void saveFile()
	{
		//parse this class to json string
		string playerStats = JsonConvert.SerializeObject(Instance);
		//save json file
		File.WriteAllText(path , playerStats); 
	}

	#endregion


}
