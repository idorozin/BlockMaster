using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;


public class updatePlayerStats : MonoBehaviour {
	
	string path;

	// Use this for initialization
	void Start () {
		path=Application.persistentDataPath+"/PlayerFile.json";
	}
	
	public void loadFile(){
		string fileString = File.ReadAllText(path);
		Debug.Log(fileString);
		JSONObject playerJson= (JSONObject)JSON.Parse(fileString);
		//get stats
		PlayerStats.player=playerJson["Name"];
		PlayerStats.highScore=playerJson["highScore"];
		PlayerStats.lastCannon=playerJson["lastCannon"];
		Debug.Log(playerJson["lastCannon"]);
		PlayerStats.money=playerJson["Money"];
		//get cannonsOwned
		//*********************************************FIX**************************************
	 	JSONArray jsonArray = (JSONArray) playerJson["cannonsOwned"].AsArray;
		Debug.Log("typeof" + jsonArray.GetType());
		foreach (JSONNode explrObject in jsonArray) 
			PlayerStats.cannonsOwned.Add(explrObject.Value); 
	
		foreach(string c in PlayerStats.cannonsOwned)
			Debug.Log(c);
	}
	
	public void saveFile(){
		JSONObject playerStats = new JSONObject();
		//stats
		playerStats.Add("Name",PlayerStats.player);
		playerStats.Add("highScore",PlayerStats.highScore);	
		playerStats.Add("lastCannon",PlayerStats.lastCannon);
		playerStats.Add("Money",PlayerStats.money);
		//cannonsOwned
		JSONArray cannonsOwned = new JSONArray();
		if(PlayerStats.cannonsOwned!=null && PlayerStats.cannonsOwned.Count!=0)
		foreach(string cannon in PlayerStats.cannonsOwned){
			cannonsOwned.Add("default");
			cannonsOwned.Add(cannon);
		}
		playerStats.Add("cannonsOwned",cannonsOwned);
		//save json file
		File.WriteAllText(path , playerStats.ToString());
	}

}
