using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
		public int offsetW , offsetG , GiftIndex , challangeIndex=0;
		public bool musicOn=true, soundOn=true;
		private Challange shoot = new Challange("Shoot 10 shapes", 10 , "shot" , "100");
		private Challange shoot1 = new Challange("Shoot 10 shapes", 100 , "record" , "200");
		private Challange shoot2 = new Challange("Shoot 10 shapes", 10 , "shot" , "100");
		private Challange shoot3 = new Challange("Shoot 10 shapes", 10 , "shot" , "100");
		private Challange shoot4 = new Challange("Shoot 10 shapes", 10 , "shot" , "100");
		private Challange shoot5 = new Challange("Shoot 10 shapes", 10 , "shot" , "100");

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
		public string challageText="" ;
		public string reward , action;
		public int goal , process , level;

		public Challange(string challageText,int goal , string action , string reward)
		{
			this.challageText = challageText;
			this.goal = goal;
			this.action = action;
			this.reward = reward;
		}

		public void reportProcess(int process , string action)
		{
			if(this.action==action)
				this.process += process;
			if (process > goal)
				nextChallange();
		}
		
		public void setProcess(int process , string action)
		{
			if(this.action==action && process>this.process)
				this.process = process;
			if (process > goal)
				nextChallange();
		}
		
 		public void skipChallange()
		{
			
		}

		public void nextChallange()
		{
			Instance.playerStats.challangeIndex++;
			Instance.saveFile();
			pauseMenu.rewards.Push(this);
			//claimReward(reward);
		}

		public override string ToString()
		{
			return challageText;
		}
	}


	void Awake()
	{
		if (Instance == null)
		{
			
			Instance = this;
			playerStats=new PlayerStats_();
			playerStats.SetChallanges();
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
		Debug.Log(fileString);
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
