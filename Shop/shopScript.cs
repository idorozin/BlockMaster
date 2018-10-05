using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class shopScript : MonoBehaviour {
	
	Item[] items;
	int currentItem=0;
	string path="Shop/Items/";
	GameObject item;
	GameObject playerStats;
	public Text balance_text;

	// Use this for initialization
	void Start () {
 		items=new Item[2];
		items[0]=new Item("1",20);
		items[1]=new Item("2",10); 
		currentItem=0;
		Debug.Log(path+items[currentItem].getName());
		item = (GameObject)Instantiate(Resources.Load(path+items[currentItem].getName()));
		playerStats = GameObject.Find("PlayerStats");
		Debug.Log(PlayerStats.money);
	}

	void Update(){
		balance_text.text=PlayerStats.money.ToString();
	}
	
	public void leftButton()
	{
		if(currentItem-1>=0 && items[currentItem-1]!=null){
		Destroy(item);
		currentItem--;
		item = (GameObject)Instantiate(Resources.Load(path+items[currentItem].getName()));
		}
	}
	
	public void rightButton()
	{
		/*  string[] items=new string[2];
		items[0]="1";
		items[1]="2"; */
		if(currentItem+1<=items.Length-1 /* && items[currentItem+1]!=null */){
		Destroy(item);
		currentItem++;
		item = (GameObject)Instantiate(Resources.Load(path+items[currentItem].getName()));
		}
	}
	
 	public void buyButton()
	{
		foreach(string item_ in PlayerStats.cannonsOwned){
			Debug.Log("you have this item");
			if(item_ == items[currentItem].getName())
				return;
		}
		 if(PlayerStats.money>=items[currentItem].getPrice()){
			PlayerStats.cannonsOwned.Add(items[currentItem].getName());
			PlayerStats.money-=items[currentItem].getPrice();
			Debug.Log("you bought the cannon");
			playerStats.GetComponent<updatePlayerStats>().saveFile();
		}
		else
			Debug.Log("you dont have enough money");
		
	}
	
	public void useButton()
	{
		bool have = false;
		foreach(string item_ in PlayerStats.cannonsOwned){
			Debug.Log(item_ + " citem " +items[currentItem].getName());
			if(item_ == items[currentItem].getName())
				have=true;
		}
		if(have){
		PlayerStats.lastCannon=items[currentItem].getName();
					Debug.Log("you are using the cannon");

					playerStats.GetComponent<updatePlayerStats>().saveFile();

		}
		
	} 

	public void backButton(){
		SceneManager.LoadScene("MainMenu");
    }
	
	
}
