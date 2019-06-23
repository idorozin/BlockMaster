using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.U2D;

public class ItemsShop : MonoBehaviour
{
	[SerializeField]
	private swipeDetector detector; 
	private List<List<Item>> categories = new List<List<Item>>();
	private List<Item> items;
	private int currentItem=0 , currentType=0;
	
	[SerializeField]
	private TextMeshProUGUI goldBalance;
	[SerializeField]
	private TextMeshProUGUI diamondBalance;
	[SerializeField]
	private TextMeshProUGUI priceText;
	[SerializeField] 
	private TextMeshProUGUI nameText;
	[SerializeField]
	private Image itemImage;
	[SerializeField]
	private GameObject lockedUi;
	[SerializeField]
	private GameObject lock_;


	
	// Use this for initialization
	private void Start ()
	{
		if(AssetDatabase.Instance.cannons != null)
			categories.Add(AssetDatabase.Instance.cannons.serializedItems);		
		if(AssetDatabase.Instance.platforms != null)
			categories.Add(AssetDatabase.Instance.platforms.serializedItems);		
		if(AssetDatabase.Instance.trails != null)
			categories.Add(AssetDatabase.Instance.trails.serializedItems);		
		if(AssetDatabase.Instance.flames != null)
			categories.Add(AssetDatabase.Instance.flames.serializedItems);
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
		if (currentItem < 1)
			currentItem = items.Count - 1;
		else
			currentItem--;
		UpdateUI();
	}

	public void MoveRight()
	{
		if (currentItem + 1 >= items.Count)
			currentItem = 0;
		else
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

	private void SetLockedUi(Item item)
	{
		if (item.Score > PlayerStats.Instance.highScore)
		{
			lock_.SetActive(true);
			lockedUi.SetActive(true);
			lock_.GetComponent<Button>().enabled = false;
			Text lockedUiText = lockedUi.GetComponent<Text>();
			lockedUiText.text = "To unlock the \n cannon reach to \n " + item.Score + "points";
		}
		else
		{
			if (!item.Unlocked())
			{
				lock_.SetActive(true);
				lock_.GetComponent<Button>().enabled = true;
				lockedUi.SetActive(false);
			}
			else
			{
				lock_.SetActive(false);
				lockedUi.SetActive(false);
			}
		}
	}

	public void TryUnlock()
	{
		if(items[currentItem].Unlock())
			SetLockedUi(items[currentItem]);
	}

	private void UpdateItem(Item item)
	{
		itemImage.sprite = item.Icon;
		itemImage.GetComponent<ImageCanvasAnimator>().SetController(item.Animator);
		
		if (item.Gold > item.Diamonds)
			priceText.text = item.Gold.ToString();
		else
			priceText.text = item.Diamonds.ToString();
		nameText.text = item.Name;
	}

	[SerializeField]
	private GameObject buy, use; 
	private void SetButton(Item item)
	{	
		bool owned = PlayerStats.Instance.ItemsOwned.Contains(item.Id);
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
		SetButton(items[currentItem]);
		UpdateItem(items[currentItem]);
		SetLockedUi(items[currentItem]);
	}
	

	#endregion
	#region Buttons

	public void BuyButton()
	{
		items[currentItem].Buy();
		SetButton(items[currentItem]);
	}
	
	public void UseButton()
	{
		items[currentItem].Use();
	} 


	#endregion
}
