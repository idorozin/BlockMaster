using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using SimpleJSON;
using Newtonsoft;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

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
		public int offsetW , offsetG , GiftIndex;
		public bool musicOn=true, soundOn=true;
		private Challange shoot = new Challange("Shoot n shapes",new []{1,2,3,4,5},new []{"1"});
		public Challange shoot1 = new Challange("Shoot n shapes",new []{1,2,3,4,5},new []{"2"});
		public Challange shoot2 = new Challange("Shoot n shapes",new []{1,2,3,4,5},new []{"3"});
		public Challange shoot3 = new Challange("Shoot n shapes",new []{1,2,3,4,5},new []{"4"});
		public Challange shoot4 = new Challange("Shoot n shapes",new []{1,2,3,4,5},new []{"5"});
		public Challange shoot5 = new Challange("Shoot n shapes",new []{1,2,3,4,5},new []{"6"});
		public Challange[] cs = new Challange[6];

		public void SetChallanges()
		{
			cs[0] = shoot;
			cs[1] = shoot1;
			cs[2] = shoot2;
			cs[3] = shoot3;
			cs[4] = shoot4;
			cs[5] = shoot5;
		}


	}

	public class Challange
	{
		public String challageText="" ;
		private string[] rewards = new string[5];
		private int[] challangeGoal = new int[5];
		public int goal , process , level;

		public Challange(String challageText,int[] challangeGoal , string[] rewards)
		{
			this.challangeGoal = challangeGoal;
			this.challageText = challageText;
			this.rewards = rewards;
			goal = challangeGoal[0];
		}

		public void reportProcess(int process)
		{
			this.process += process;
		}

		public string nextLevel()
		{
			level++;
			goal = challangeGoal[level];	
			return rewards[level-1];
		}

		public string reward()
		{
			return rewards[level];
		}

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
		saveFile();
	}
	
	public void saveFile()
	{
		//parse this class to json string
		string playerStats = Newtonsoft.Json.JsonConvert.SerializeObject(this.playerStats);
		//save json file
		File.WriteAllText(path , playerStats); 
	}

	#endregion


}
