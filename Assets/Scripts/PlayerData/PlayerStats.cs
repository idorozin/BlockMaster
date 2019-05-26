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
			//if (Instance.challenges == null)
			//{
				Instance.challenges = new List<Challenge>(challenges);
				for (int i = 0; i < 3; i++)
				{
					Instance.challenges[i].Activate();
				}
				Instance.ChallengesAvailable = 1;
			//}
			DontDestroyOnLoad(gameObject);
			GameObject.Find("MenuCanvas").GetComponent<MenuScript>().setRecordText();
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
