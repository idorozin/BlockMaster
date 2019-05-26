
using System.Collections.Generic;
using UnityEngine;

public class SetSkin : MonoBehaviour
{
	private SpriteRenderer sprite;

	[SerializeField] private string path = "Cannons/";
	[SerializeField] private ItemsShopCatalog cats;
	
	void Start ()
	{
		List<List<Item>> itemsList = new List<List<Item>>();
		foreach (var cat in cats.serializedItems)
		{
			itemsList.Add(cat.list);
		}

		Item item = null;
		foreach (var cat in itemsList)
		{
			foreach (var i in cat)
			{
				if(i.Name == PlayerStats.Instance.lastCannon){
					item = i;
					break;
				}
			}
		}

		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path + PlayerStats.Instance.lastCannon);
		/*sprite = GetComponent<SpriteRenderer>();
		//if(item!=null)
		//	sprite.sprite = item.Icon;
		Texture2D texture = Resources.Load<Texture2D>(path + PlayerStats.Instance.lastCannon);
		//GetComponent<Renderer>().me
		if (texture == null)
			return;
		Vector2 pivot = new Vector2(0.5f , 0.5f);
		float pixelsPerUnit = 100.0f;
		Rect rect = new Rect(0.0f , 0.0f , texture.width , texture.height);
		Sprite newSprite = Sprite.Create(texture , rect , pivot);
		//sprite.sprite = newSprite;*/

	}
}
