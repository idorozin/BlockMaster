using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerStats : MonoBehaviour {
	  

	public static string player="";
	public static float highScore=0;
	public static string lastCannon="default";
	public static List<string> cannonsOwned =new List<string>();
	public static float money=0;
	updatePlayerStats update;
	
	

	void Awake(){
		DontDestroyOnLoad(gameObject);
		
	}
	void Start(){
		update=GetComponent<updatePlayerStats>();
		Debug.Log(Application.persistentDataPath+"/PlayerFile.json");
		if(File.Exists(Application.persistentDataPath+"/PlayerFile.json")) Debug.Log("file exists"); else update.saveFile();	
		update.loadFile();
		Debug.Log(highScore);
		
	}

}
