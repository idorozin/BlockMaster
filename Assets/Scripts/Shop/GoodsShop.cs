using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.U2D;

public class GoodsShop : MonoBehaviour
{
	[SerializeField]
	private swipeDetector detector; 
	public List<List<IAP>> categories = new List<List<IAP>>();
	private List<IAP> items;
	private int currentItem=0 , currentType=0;
	
	[SerializeField]
	private TextMeshProUGUI priceText;
	[SerializeField] 
	private TextMeshProUGUI amount;
	[SerializeField]
	private Image itemImage;		
	[SerializeField]
	private Image type;	
	[SerializeField]
	private Sprite gold;	
	[SerializeField]
	private Sprite diamond;


	// Use this for initialization
	private void Start ()
	{
		categories.Add(new List<IAP>());
		categories.Add(new List<IAP>());
		foreach (var iap in IAPManager.Instance.products)
		{
			if (iap.Type == IAP.ProductTypes.Gold)
			{
				categories[0].Add(iap);
			}
			else if (iap.Type == IAP.ProductTypes.Diamonds)
			{
				categories[1].Add(iap);
			}
		}
		items = categories[currentType];
		currentItem=0;
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

	private void UpdateItem(IAP item)
	{
		if (item.Image != null)
		{
			itemImage.sprite = item.Image;
			itemImage.rectTransform.sizeDelta = item.size;
			itemImage.rectTransform.anchoredPosition = item.position;
			itemImage.rectTransform.rotation = Quaternion.identity;
		}

		if (item.Type == IAP.ProductTypes.Gold)
			type.sprite = gold;
		else
			type.sprite = diamond;
		priceText.text = "$"+item.Price + "";
		amount.text = item.Amount + "";

	}

	private void UpdateUI()
	{
		UpdateItem(items[currentItem]);
	}
	
	#endregion
	
	#region Buttons

	public void BuyButton()
	{
		items[currentItem].Buy();
	}

	#endregion
}
