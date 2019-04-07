using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class shopScript : MonoBehaviour
{
	private bool inItemsShop = false;
	[SerializeField]
	public List<Item[]> catagories = new List<Item[]>();
	private Item[] items , cannons , particles;
	[SerializeField]
	private int currentItem=0 , currentType=0;
	private const string Path = "Shop/Items/";
	private GameObject item;
	public Text balance_text;
	public GameObject lockedUi;
	private Text lockedUiText;
	[SerializeField]
	private Text priceText;
	[SerializeField] 
	private Text nameText;

	public static bool inShop=false;
	
	// Use this for initialization
	private void Start ()
	{
		cannons = new Item[2];
		cannons[0]=new Item("1",20,10);
		cannons[1]=new Item("2",10,40);
		items = cannons;
		catagories.Add(cannons);
		particles = new Item[2];
		particles[1]=new Item("DiamondLogo",20,10);
		particles[0]=new Item("diamonds",10,40);
		catagories.Add(particles);
		currentItem=0;
		GetComponent<swipeDetector>().SwipeDetected += MoveToSwipeDirection;
	
	}
	private void Update(){ // TODO
		balance_text.text=PlayerStats.Instance.money.ToString();
	}
	private void MoveToSwipeDirection(string direction)
	{
		switch (direction)
		{
			case "Right":
				MoveRight();
				break;
			case "Left":
				MoveLeft();
				break;
			case "Up":
				MoveUp();
				break;
			case "Down":
				MoveDown();
				break;
		}
	}

	#region Move

	private void MoveLeft()
	{
		if (currentItem - 1 < 0 || items[currentItem - 1] == null) return;
		currentItem--;
		CreateItem();
	}

	public void MoveRight()
	{
		if (currentItem + 1 > items.Length - 1 || items[currentItem + 1] == null) return;
		currentItem++;
		CreateItem();
	}

	public void MoveUp()
	{
		Debug.Log("UP");
		if(currentType  + 1 > catagories.Count - 1 || catagories[currentType+1] == null) return;
		currentType++;
		currentItem = 0;
		items = catagories[currentType];
		CreateItem();
	}
	
	public void MoveDown()
	{
		Debug.Log("Down");
		if(currentType-1 < 0 || catagories[currentType-1] == null) return;
		currentType--;
		currentItem = 0;
		items = catagories[currentType];
		CreateItem();
	}

	#endregion

	#region Create

	private void SetLockedUi()
	{
		if (items[currentItem].Score > PlayerStats.Instance.highScore)
		{
			lockedUi.SetActive(true);
			lockedUiText = lockedUi.GetComponent<Text>();
			lockedUiText.text = "To unlock the \n cannon reach to \n " + items[currentItem].Score + "points";
		}
		else
			lockedUi.SetActive(false);
	}

	private void InstantiateItem(Item item_)
	{
		var g = Resources.Load<SpriteRenderer>(Path + item_.Name);
		GameObject itemPrefab = (g as SpriteRenderer).gameObject;
		item = Instantiate(itemPrefab);
		priceText.text = item_.Price.ToString();
		nameText.text = item_.Name;
	}

	// TODO
	private GameObject buy, use; 
	private void SetButton()
	{
		if (!inItemsShop) return;
		if(buy==null)
			buy = GameObject.Find("CannonsShop").transform.Find("buy").gameObject;
		if(use==null)
			use = GameObject.Find("CannonsShop").transform.Find("use").gameObject;
		
		bool owned = PlayerStats.Instance.ItemsOwned.Contains(items[currentItem].Name);
		if (owned)
		{
			if(buy.activeSelf)
			buy.SetActive(false);
			use.SetActive(true);
		}
		if (!owned)
		{
			buy.SetActive(true);
			if(use.activeSelf)
			use.SetActive(false);
		}
	}

	private void CreateItem()
	{
		Destroy(item);
		SetButton();
		InstantiateItem(items[currentItem]);
		SetLockedUi();
	}
	

	#endregion
	//TODO
	#region Buttons

	public void BuyButton()
	{
		if (items[currentItem].Score > PlayerStats.Instance.highScore || PlayerStats.Instance.ItemsOwned.Contains(items[currentItem].Name))
			return;
		
		if(PlayerStats.Instance.money>=items[currentItem].Price){
			PlayerStats.Instance.ItemsOwned.Add(items[currentItem].Name);
			PlayerStats.Instance.money-=items[currentItem].Price;
			Debug.Log("you bought the cannon");
			PlayerStats.saveFile();
		}
		SetButton();
		
	}
	
	public void UseButton()
	{
		if (PlayerStats.Instance.lastCannon == items[currentItem].Name || !PlayerStats.Instance.ItemsOwned.Contains(items[currentItem].Name))
			return;
		PlayerStats.Instance.lastCannon=items[currentItem].Name;
		PlayerStats.saveFile();
	} 

	public void BackButton(){
		SceneManager.LoadScene("MainMenu");
	}

	[SerializeField]private GameObject goodsShop;
	[SerializeField]private GameObject itemsShop;
	[SerializeField] private GameObject chooseShop;
	
	public void OpenItemsShop()
	{
		inItemsShop = true;
		itemsShop.SetActive(true);
		chooseShop.SetActive(false);
		inShop = true;
		CreateItem();
	}

	public void CloseItemsShop()
	{
		itemsShop.SetActive(false);
		chooseShop.SetActive(true);
		inShop = true;
		inItemsShop = false;
	}
	public void OpenGoodsShop()
	{
		goodsShop.SetActive(true);
		chooseShop.SetActive(false);
		inShop = true;
	}

	public void CloseGoodsShop()
	{
		goodsShop.SetActive(false);
		chooseShop.SetActive(true);
		inShop = true;
	}

	#endregion
}
