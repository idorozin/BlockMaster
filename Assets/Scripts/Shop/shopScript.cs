using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shopScript : MonoBehaviour
{
	private bool inItemsShop = false;
	[SerializeField] private ItemsShop serializedItems;
	public List<List<Item>> catagories = new List<List<Item>>();
	private List<Item> items , cannons , particles;
	[SerializeField]
	private int currentItem=0 , currentType=0;
	private const string Path = "Shop/Items/";
	private GameObject item;
	[SerializeField]
	private Image itemImage;
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
		foreach (var listWraper in serializedItems.serializedItems)
		{
			catagories.Add(listWraper.list);
		}

		items = catagories[currentType];
		currentItem=0;
		UpdateUI();
		GetComponent<swipeDetector>().SwipeDetected += MoveToSwipeDirection;
	}
	
	private void Update(){ // TODO
		balance_text.text=PlayerStats.Instance.money.ToString();
	}
	
	[ContextMenu("MoveUp")]
	public void up() {
		MoveUp();
	}
	[ContextMenu("MoveDown")]
	public void down() {
		MoveDown();
	}
	[ContextMenu("MoveRight")]
	public void right() {
		MoveRight();
	}
	[ContextMenu("MoveLeft")]
	public void left() {
		MoveLeft();
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
		UpdateUI();
	}

	public void MoveRight()
	{
		if (currentItem + 1 > items.Count - 1 || items[currentItem + 1] == null) return;
		currentItem++;
		UpdateUI();
	}

	public void MoveUp()
	{
		if(currentType  + 1 > catagories.Count - 1 || catagories[currentType+1] == null) return;
		currentType++;
		currentItem = 0;
		items = catagories[currentType];
		UpdateUI();
	}
	
	public void MoveDown()
	{
		if(currentType-1 < 0 || catagories[currentType-1] == null) return;
		currentType--;
		currentItem = 0;
		items = catagories[currentType];
		UpdateUI();
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

	private void UpdateItem(Item item_)
	{
		itemImage.sprite = item_.Sprite;
		priceText.text = item_.Gold.ToString();
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

	private void UpdateUI()
	{
		SetButton();
		UpdateItem(items[currentItem]);
		SetLockedUi();
	}
	

	#endregion
	//TODO
	#region Buttons

	public void BuyButton()
	{
		if (items[currentItem].Score > PlayerStats.Instance.highScore || PlayerStats.Instance.ItemsOwned.Contains(items[currentItem].Name))
			return;
		
		if(PlayerStats.Instance.money>=items[currentItem].Gold){
			PlayerStats.Instance.ItemsOwned.Add(items[currentItem].Name);
			PlayerStats.Instance.money-=items[currentItem].Gold;
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
		UpdateUI();
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
