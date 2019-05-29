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
	public bool Locked = true;

	
	public enum ItemType
	{
		Cannon , Troll ,
	}

	public Item(){}
	public Item(string name , Sprite icon)
	{
		this.Name = name;
		this.Icon = icon;
	}

	public void Buy()
	{
		if (Score > PlayerStats.Instance.highScore || PlayerStats.Instance.ItemsOwned.Contains(Name))
			return;
		
		if(PlayerStats.Instance.gold >= Gold && PlayerStats.Instance.diamonds >= Diamonds){
			PlayerStats.Instance.ItemsOwned.Add(Name);
			PlayerStats.Instance.gold -= Gold;
			PlayerStats.Instance.diamonds -= Diamonds;
			PlayerStats.saveFile();
		}
	}

	public void Use()
	{
		if (PlayerStats.Instance.lastCannon == Name || !PlayerStats.Instance.ItemsOwned.Contains(Name))
			return;
		//if(type == ItemType.Cannon)
			PlayerStats.Instance.lastCannon = Name;
		PlayerStats.saveFile();
	}

	public bool Unlock()
	{
		if (Score <= PlayerStats.Instance.highScore)
		{
			Locked = false;
			PlayerStats.Instance.ItemsUnlocked.Add(Name);
			PlayerStats.saveFile();
		}
		return !Locked;
	}

	public class Item2 : Item
	{
		public TrailEffect trail;
	}

}