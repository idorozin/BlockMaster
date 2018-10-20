using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats Instance;
	public static string player="";
	public static float highScore=0;
	public static string lastCannon="default";
	public static List<string> cannonsOwned =new List<string>();
	public static float money=0;
	public static float highScoreHeight=0;
	private static bool loaded = false;
	updatePlayerStats update;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

	}
	
	void Start()
	{
		if(loaded)
			return;
		update=GetComponent<updatePlayerStats>();
		if(File.Exists(Application.persistentDataPath+"/PlayerFile.json")) Debug.Log("file exists"); else update.saveFile();	
		update.loadFile();
		GameObject.Find("MenuCanvas").GetComponent<MenuScript>().setRecordText();
		loaded = true;		
	}

}
