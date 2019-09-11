using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.U2D;
using MyAsset;
public class ItemsShop : MonoBehaviour
{
	[SerializeField]
	private swipeDetector detector; 
	private List<Category> categories = new List<Category>();
	private List<Item> items;
	private int currentItem=0 , currentType=0;
	
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

	[SerializeField] private Image type;
	[SerializeField]
	private	GameObject secondPrice;
	[SerializeField]
	private TextMeshProUGUI secondPriceText;

	[SerializeField] private Sprite gold;
	[SerializeField] private Sprite dia;

	public RectTransform transform;


	// Use this for initialization
	private void Start ()
	{
		if(AssetDatabase.Instance.cannons != null)
			categories.Add(AssetDatabase.Instance.cannons);		
		if(AssetDatabase.Instance.platforms != null)
			categories.Add(AssetDatabase.Instance.platforms);		
		if(AssetDatabase.Instance.trails != null)
			categories.Add(AssetDatabase.Instance.trails);		
		if(AssetDatabase.Instance.flames != null)
			categories.Add(AssetDatabase.Instance.flames);
		items = categories[currentType].serializedItems;
		currentItem=0;
		AdjustUI();
		UpdateUI();
		detector.SwipeDetected += MoveToSwipeDirection;
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
		if(!gameObject.activeSelf)
			return;
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
		if(currentType+ 1 > categories.Count - 1 || categories[currentType+1].serializedItems.Count==0) return;
		currentType++;
		currentItem = 0;
		items = categories[currentType].serializedItems;
		AdjustUI();
		UpdateUI();
	}
	
	public void MoveDown()
	{
		if(currentType-1 < 0 || categories[currentType-1].serializedItems.Count==0) return;
		currentType--;
		currentItem = 0;
		items = categories[currentType].serializedItems;
		AdjustUI();
		UpdateUI();
	}

	private void AdjustUI()
	{
		Category c = categories[currentType];
		itemImage.rectTransform.sizeDelta = new Vector2(c.width , c.height);
		//itemImage.rectTransform.position = new Vector2(c.x , c.y);
		itemImage.rectTransform.rotation = Quaternion.identity;
		itemImage.rectTransform.Rotate(0 ,0 , c.rotation);
	}

	#endregion

	#region Create

	private void SetLockedUi(Item item)
	{
		bool owned = PlayerStats.Instance.ItemsOwned.Contains(item.Id);
		if (owned)
		{
			lock_.SetActive(false);
			lockedUi.SetActive(false);
			return;
		}
		if (item.Score > PlayerStats.Instance.highScore)
		{
			lock_.SetActive(true);
			lockedUi.SetActive(true);
			lock_.GetComponent<Button>().enabled = false;
			TextMeshProUGUI lockedUiText = lockedUi.GetComponent<TextMeshProUGUI>();
			lockedUiText.text = "To unlock the \n cannon reach to \n " + item.Score + "meters";
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
		if (items[currentItem].Unlock())
		{
			SetLockedUi(items[currentItem]);
			SetButton(items[currentItem]);
		}
	}

	private void UpdateItem(Item item)
	{
		if (item.Animator == null)
		{
			itemImage.sprite = item.Icon;
		}

		itemImage.GetComponent<ImageCanvasAnimator>().SetController(item.Animator);
		secondPrice.SetActive(false);

		if (item.Gold >= item.Diamonds)
		{
			priceText.text = item.Gold.ToString();
			type.sprite = gold;
			if (item.Diamonds > 0)
			{
				secondPrice.SetActive(true);
				secondPriceText.text = item.Diamonds.ToString();
			}
		}
		else
		{
			priceText.text = item.Diamonds.ToString();
			type.sprite = dia;
		}

		nameText.text = item.Name;
	}

	[SerializeField]
	private GameObject buy, use;

	private void SetButton(Item item)
	{
		if (!item.Unlocked())
		{
			buy.GetComponent<Button>().enabled = false;
			use.GetComponent<Button>().enabled = false;
		}		
		else
		{
			buy.GetComponent<Button>().enabled = true;
			use.GetComponent<Button>().enabled = true;
		}

		//if (item.Default())
		//	BuyButton();
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
