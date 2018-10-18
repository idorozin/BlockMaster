using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;


public class updatePlayerStats : MonoBehaviour {
	
	string path;

	// Use this for initialization
	void Start () 
	{
		path=Application.persistentDataPath+"/PlayerFile.json";
	}
	
	public void loadFile()
	{
		string fileString = File.ReadAllText(path);
		Debug.Log(fileString);
		JSONObject playerJson= (JSONObject)JSON.Parse(fileString);
		//get stats
		PlayerStats.player=playerJson["Name"];
		PlayerStats.highScore=playerJson["highScore"];
		PlayerStats.lastCannon=playerJson["lastCannon"];
		PlayerStats.money=playerJson["Money"];
		PlayerStats.highScoreHeight=playerJson["highScoreHeight"];
		//get cannonsOwned
	 	JSONArray jsonArray = (JSONArray) playerJson["cannonsOwned"].AsArray;
		foreach (JSONNode explrObject in jsonArray) 
			PlayerStats.cannonsOwned.Add(explrObject.Value); 
	}
	
	public void saveFile()
	{
		JSONObject playerStats = new JSONObject();
		//stats
		playerStats.Add("Name",PlayerStats.player);
		playerStats.Add("highScore",PlayerStats.highScore);	
		playerStats.Add("lastCannon",PlayerStats.lastCannon);
		playerStats.Add("Money",PlayerStats.money);
		playerStats.Add("highScoreHeight",PlayerStats.highScoreHeight);
		//cannonsOwned
		JSONArray cannonsOwned = new JSONArray();
		if(PlayerStats.cannonsOwned!=null && PlayerStats.cannonsOwned.Count!=0)
			foreach(string cannon in PlayerStats.cannonsOwned)
			{
				cannonsOwned.Add(cannon);
			}
		playerStats.Add("cannonsOwned",cannonsOwned);
		//save json file
		File.WriteAllText(path , playerStats.ToString());
	}

}
