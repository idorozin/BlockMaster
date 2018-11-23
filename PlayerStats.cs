using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats Instance;
	public string player="";
	public float highScore=0;
	public string lastCannon="default";
	public List<string> cannonsOwned =new List<string>();
	public float money=0;
	public float highScoreHeight=0;
	public string wheelTime = "";
	public string giftTime = "";
	public int offset , maxCountDown , GiftIndex;
	public bool musicOn=true, soundOn=true;
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			path=Application.persistentDataPath+"/PlayerFile.json";
			if(File.Exists(Application.persistentDataPath+"/PlayerFile.json")) Debug.Log("file exists"); else saveFile();	
			loadFile();
			GameObject.Find("MenuCanvas").GetComponent<MenuScript>().setRecordText();
			GetComponent<DailyReward>().callStart();
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
	{		string fileString = File.ReadAllText(path);
		Debug.Log(fileString);
		JSONObject playerJson= (JSONObject)JSON.Parse(fileString);
		//get stats
		player=playerJson["Name"];
		highScore=playerJson["highScore"];
		lastCannon=playerJson["lastCannon"];
		money=playerJson["Money"];
		highScoreHeight=playerJson["highScoreHeight"];
		wheelTime=playerJson["wheelTime"];
		offset = playerJson["Offset"];
		maxCountDown = playerJson["MaxCountDown"];
		musicOn = playerJson["MusicOn"];
		soundOn = playerJson["SoundOn"];
		GiftIndex = playerJson["GiftIndex"];
		//get cannonsOwned
		JSONArray jsonArray = (JSONArray) playerJson["cannonsOwned"].AsArray;
		foreach (JSONNode explrObject in jsonArray) 
			cannonsOwned.Add(explrObject.Value);
		Debug.Log(highScore);
	}
	
	public void saveFile()
	{
		JSONObject playerStats = new JSONObject();
		//stats
		playerStats.Add("Name",player);
		playerStats.Add("highScore",highScore);	
		playerStats.Add("lastCannon",lastCannon);
		playerStats.Add("Money",money);
		playerStats.Add("highScoreHeight",highScoreHeight);
		playerStats.Add("wheelTime",wheelTime);
		playerStats.Add("Offset",offset);
		playerStats.Add("MusicOn",musicOn);
		playerStats.Add("SoundOn",soundOn);
		playerStats.Add("GiftIndex",GiftIndex);
		//cannonsOwned
		JSONArray cannonsOwned = new JSONArray();
		if(cannonsOwned!=null && cannonsOwned.Count!=0)
			foreach(string cannon in this.cannonsOwned)
			{
				cannonsOwned.Add(cannon);
			}
		playerStats.Add("cannonsOwned",cannonsOwned);
		//save json file
		File.WriteAllText(path , playerStats.ToString());
	}

	#endregion


}
