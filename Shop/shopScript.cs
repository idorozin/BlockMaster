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
		if (items[currentItem].getScore() > PlayerStats.Instance.playerStats.highScore)
		{
			lockedUi.SetActive(true);
			lockedUiText=lockedUi.GetComponent<Text>();
			lockedUiText.text = "To unlock the \n cannon reach to \n " + items[currentItem].getScore() + "points";		
		}

		playerStats = GameObject.Find("PlayerStats");
		GetComponent<swipeDetector>().swipeDetected += moveToSwipeDirection;
	
	}

	void moveToSwipeDirection(string direction)
	{
		switch (direction)
		{
			case "Right":
				moveRight();
			    break;
			case "Left":
				moveLeft();
				break;
			case "Up":
				moveUp();
				break;
			case "Down":
				moveDown();
				break;
		}
	}

	void Update(){
		balance_text.text=PlayerStats.Instance.playerStats.money.ToString();
	}
	
	public void moveLeft()
	{
		if (currentItem - 1 >= 0 && items[currentItem - 1] != null)
		{
			Destroy(item);
			currentItem--;
			item = (GameObject) Instantiate(Resources.Load(path + items[currentItem].getName()));
			priceText.text = items[currentItem].getPrice().ToString();
			nameText.text = items[currentItem].getName();
			if (items[currentItem].getScore() > PlayerStats.Instance.playerStats.highScore)
			{
				lockedUi.SetActive(true);
				lockedUiText = lockedUi.GetComponent<Text>();
				lockedUiText.text = "To unlock the \n cannon reach to \n " + items[currentItem].getScore() + "points";
			}
			else
				lockedUi.SetActive(false);
		}

	}
	
	public void moveRight()
	{
		if (currentItem + 1 <= items.Length - 1 && items[currentItem + 1] != null)
		{
			Destroy(item);
			currentItem++;
			item = (GameObject) Instantiate(Resources.Load(path + items[currentItem].getName()));
			priceText.text = items[currentItem].getPrice().ToString();
			nameText.text = items[currentItem].getName();
			if (items[currentItem].getScore() > PlayerStats.Instance.playerStats.highScore)
			{
				lockedUi.SetActive(true);
				lockedUiText=lockedUi.GetComponent<Text>();
				lockedUiText.text = "To unlock the \n cannon reach to \n " + items[currentItem].getScore() + "points";
			}
			else
				lockedUi.SetActive(false);
		}
	}

	public void moveUp()
	{
		Debug.Log("moveUp");
	}
	
	public void moveDown()
	{
		Debug.Log("down");
	}


	public void buyButton()
	 {
		 if (items[currentItem].getScore() > PlayerStats.Instance.playerStats.highScore)
			 return;
		foreach(string item_ in PlayerStats.Instance.playerStats.ItemsOwned){
			if(item_ == items[currentItem].getName())
				return;
		}
		 if(PlayerStats.Instance.playerStats.money>=items[currentItem].getPrice()){
			PlayerStats.Instance.playerStats.ItemsOwned.Add(items[currentItem].getName());
			PlayerStats.Instance.playerStats.money-=items[currentItem].getPrice();
			Debug.Log("you bought the cannon");
			PlayerStats.Instance.saveFile();
		}
		
	}
	
	public void useButton()
	{
		if (PlayerStats.Instance.playerStats.lastCannon == items[currentItem].getName())
			return;
		bool have = false;
		foreach(string item_ in PlayerStats.Instance.playerStats.ItemsOwned){
			if(item_ == items[currentItem].getName())
				have=true;
		}
		if(!have)
			return;
		PlayerStats.Instance.playerStats.lastCannon=items[currentItem].getName();
		PlayerStats.Instance.saveFile();
	} 

	public void backButton(){
		SceneManager.LoadScene("MainMenu");
    }
	
	
}
