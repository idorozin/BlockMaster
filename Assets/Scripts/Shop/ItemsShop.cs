using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemsShop : MonoBehaviour
{
	[SerializeField]
	private swipeDetector detector; 
	[SerializeField] private ItemsShopCatalog serializedItems;
	public List<List<Item>> categories = new List<List<Item>>();
	private List<Item> items;
	private int currentItem=0 , currentType=0;
	
	[SerializeField]
	private Text goldBalance;
	[SerializeField]
	private Text diamondBalance;
	[SerializeField]
	private GameObject lockedUi;
	[SerializeField]
	private Image itemImage;
	[SerializeField]
	private Text priceText;
	[SerializeField] 
	private Text nameText;

	
	// Use this for initialization
	private void Start ()
	{
		foreach (var listWraper in serializedItems.serializedItems)
		{
			categories.Add(listWraper.list);
		}
		items = categories[currentType];
		currentItem=0;
		UpdateUI();
		detector.SwipeDetected += MoveToSwipeDirection;
	}
	
	private void Update(){ // TODO
		goldBalance.text=PlayerStats.Instance.gold.ToString();
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
		if(currentType+ 1 > categories.Count - 1 || categories[currentType+1].Count==0) return;
		currentType++;
		currentItem = 0;
		items = categories[currentType];
		UpdateUI();
	}
	
	public void MoveDown()
	{
		if(currentType-1 < 0 || categories[currentType-1].Count==0) return;
		currentType--;
		currentItem = 0;
		items = categories[currentType];
		UpdateUI();
	}

	#endregion

	#region Create

	private void SetLockedUi()
	{
		if (items[currentItem].Score > PlayerStats.Instance.highScore)
		{
			lockedUi.SetActive(true);
			Text lockedUiText = lockedUi.GetComponent<Text>();
			lockedUiText.text = "To unlock the \n cannon reach to \n " + items[currentItem].Score + "points";
		}
		else
			lockedUi.SetActive(false);
	}

	private void UpdateItem(Item item)
	{
		itemImage.sprite = item.Icon;
		if (item.Gold > item.Diamonds)
			priceText.text = item.Gold.ToString();
		else
			priceText.text = item.Diamonds.ToString();
		nameText.text = item.Name;
	}

	// TODO
	private GameObject buy, use; 
	private void SetButton()
	{
		if(buy==null)
			buy = GameObject.Find("ItemsShop").transform.Find("buy").gameObject;
		if(use==null)
			use = GameObject.Find("ItemsShop").transform.Find("use").gameObject;
		
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
		items[currentItem].Buy();
		SetButton();
		
	}
	
	public void UseButton()
	{
		items[currentItem].Use();
	} 


	#endregion
}
