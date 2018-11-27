using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using Newtonsoft;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats Instance;
	public PlayerStats_ playerStats;

	public class PlayerStats_
	{
		public float highScore=0;
		public string lastCannon="default";
		public List<string> ItemsOwned =new List<string>();
		public float money=0;
		public float highScoreHeight=0;
		public string wheelTime = "";
		public string giftTime = "";
		public int offset , GiftIndex;
		public bool musicOn=true, soundOn=true;
	}

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			playerStats=new PlayerStats_();
			path = Application.persistentDataPath + "/PlayerFile.json";
			if (File.Exists(Application.persistentDataPath + "/PlayerFile.json")) Debug.Log("file exists");
			else saveFile();
			loadFile();
			GameObject.Find("MenuCanvas").GetComponent<MenuScript>().setRecordText();
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	string path;

	#region save&load

	public void loadFile()
	{		
		//read file
		string fileString = File.ReadAllText(path);
		//Serialize object
		Newtonsoft.Json.JsonConvert.PopulateObject(fileString , playerStats);
	}
	
	public void saveFile()
	{
		//parse this class to json string
		string playerStats = Newtonsoft.Json.JsonConvert.SerializeObject(this.playerStats);
		Debug.Log(playerStats);
		//save json file
		File.WriteAllText(path , playerStats); 
	}

	#endregion


}
