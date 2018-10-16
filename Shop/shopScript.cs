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
	public GameObject lockedUi;
	private Text lockedUiText;
	[SerializeField]
	private Text priceText;
	[SerializeField] 
	private Text nameText;
	
	
	// Use this for initialization
	void Start () {
 		items=new Item[2];
		items[0]=new Item("1",20,10);
		items[1]=new Item("2",10,40); 
		currentItem=0;
		item = (GameObject)Instantiate(Resources.Load(path+items[currentItem].getName()));
		priceText.text = items[currentItem].getPrice().ToString();
		nameText.text = items[currentItem].getName();
		if (items[currentItem].getScore() > PlayerStats.highScore)
		{
			lockedUi.SetActive(true);
			lockedUiText=lockedUi.GetComponent<Text>();
			lockedUiText.text = "To unlock the \n cannon reach to \n " + items[currentItem].getScore() + "points";		
		}

		playerStats = GameObject.Find("PlayerStats");
	}

	void Update(){
		balance_text.text=PlayerStats.money.ToString();
	}
	
	public void leftButton()
	{
		if (currentItem - 1 >= 0 && items[currentItem - 1] != null)
		{
			Destroy(item);
			currentItem--;
			item = (GameObject) Instantiate(Resources.Load(path + items[currentItem].getName()));
			priceText.text = items[currentItem].getPrice().ToString();
			nameText.text = items[currentItem].getName();
			if (items[currentItem].getScore() > PlayerStats.highScore)
			{
				lockedUi.SetActive(true);
				lockedUiText = lockedUi.GetComponent<Text>();
				lockedUiText.text = "To unlock the \n cannon reach to \n " + items[currentItem].getScore() + "points";
			}
			else
				lockedUi.SetActive(false);
		}

	}
	
	public void rightButton()
	{
		if (currentItem + 1 <= items.Length - 1 && items[currentItem + 1] != null)
		{
			Destroy(item);
			currentItem++;
			item = (GameObject) Instantiate(Resources.Load(path + items[currentItem].getName()));
			priceText.text = items[currentItem].getPrice().ToString();
			nameText.text = items[currentItem].getName();
			if (items[currentItem].getScore() > PlayerStats.highScore)
			{
				lockedUi.SetActive(true);
				lockedUiText=lockedUi.GetComponent<Text>();
				lockedUiText.text = "To unlock the \n cannon reach to \n " + items[currentItem].getScore() + "points";
			}
			else
				lockedUi.SetActive(false);
		}
	}
	
 	public void buyButton()
	 {
		 if (items[currentItem].getScore() > PlayerStats.highScore)
			 return;
		foreach(string item_ in PlayerStats.cannonsOwned){
			if(item_ == items[currentItem].getName())
				return;
		}
		 if(PlayerStats.money>=items[currentItem].getPrice()){
			PlayerStats.cannonsOwned.Add(items[currentItem].getName());
			PlayerStats.money-=items[currentItem].getPrice();
			Debug.Log("you bought the cannon");
			playerStats.GetComponent<updatePlayerStats>().saveFile();
		}
		
	}
	
	public void useButton()
	{
		if (PlayerStats.lastCannon == items[currentItem].getName())
			return;
		bool have = false;
		foreach(string item_ in PlayerStats.cannonsOwned){
			if(item_ == items[currentItem].getName())
				have=true;
		}
		if(!have)
			return;
		PlayerStats.lastCannon=items[currentItem].getName();
		playerStats.GetComponent<updatePlayerStats>().saveFile();
	} 

	public void backButton(){
		SceneManager.LoadScene("MainMenu");
    }
	
	
}
