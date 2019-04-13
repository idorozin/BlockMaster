using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class PlayerStats : MonoBehaviour
{
	public static PlayerData Instance;

	[SerializeField]
	private List<Challenge> challenges;

	void Awake()
	{
		if (Instance == null)
		{

			Instance = new PlayerData();
			path = Application.persistentDataPath + "/PlayerFile.json";
			if (File.Exists(Application.persistentDataPath + "/PlayerFile.json")) Debug.Log("file exists");
			else saveFile();
			loadFile();
			Instance.challenges = new List<Challenge>(challenges);
			Instance.ChallengesAvailable = 1;
			GameObject.Find("MenuCanvas").GetComponent<MenuScript>().setRecordText();
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	
	static string path;
	
	#region Save & Load

	public static void loadFile()
	{		
		//read file
		string fileString = File.ReadAllText(path);
		Debug.Log(fileString);
		//Serialize object
		Newtonsoft.Json.JsonConvert.PopulateObject(fileString , Instance);			
		saveFile();
	}
	
	public static void saveFile()
	{
		//parse this class to json string
		string playerStats = Newtonsoft.Json.JsonConvert.SerializeObject(Instance);
		//save json file
		File.WriteAllText(path , playerStats); 
	}

	#endregion


}
