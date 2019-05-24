using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[System.Serializable]

public class Item
{
	public string Name;
	public Sprite Icon;
	public ItemType type;
	public int Gold;
	public int Diamonds;
	public int Score;

	
	public enum ItemType
	{
		Cannon , Troll ,
	}

	public void Buy()
	{
		if (Score > PlayerStats.Instance.highScore || PlayerStats.Instance.ItemsOwned.Contains(Name))
			return;
		
		if(PlayerStats.Instance.gold >= Gold && PlayerStats.Instance.diamonds >= Diamonds){
			PlayerStats.Instance.ItemsOwned.Add(Name);
			PlayerStats.Instance.gold -= Gold;
			PlayerStats.saveFile();
		}
	}

	public void Use()
	{
		if (PlayerStats.Instance.lastCannon == Name || !PlayerStats.Instance.ItemsOwned.Contains(Name))
			return;
		if(type == ItemType.Cannon)
			PlayerStats.Instance.lastCannon= Name;
		PlayerStats.saveFile();
	}
}