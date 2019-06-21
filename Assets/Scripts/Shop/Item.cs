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
	public RuntimeAnimatorController Animator;
	
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
	public Item(string name , Sprite icon , RuntimeAnimatorController animator)
	{
		this.Name = name;
		this.Icon = icon;
		this.Animator = animator;
	}

	public void Buy(int index)
	{
		if (Score > PlayerStats.Instance.highScore || PlayerStats.Instance.ItemsOwned.Contains(index))
			return;
		
		if(PlayerStats.Instance.gold >= Gold && PlayerStats.Instance.diamonds >= Diamonds){
			PlayerStats.Instance.ItemsOwned.Add(index);
			PlayerStats.Instance.gold -= Gold;
			PlayerStats.Instance.diamonds -= Diamonds;
			PlayerStats.saveFile();
		}
	}

	public void Use(int index)
	{
		if (PlayerStats.Instance.lastCannon == index || !PlayerStats.Instance.ItemsOwned.Contains(index))
			return;
		//if(type == ItemType.Cannon)
			PlayerStats.Instance.lastCannon = index;
		PlayerStats.saveFile();
	}

	public bool Unlock(int index)
	{
		if (Score <= PlayerStats.Instance.highScore)
		{
			PlayerStats.Instance.ItemsUnlocked.Add(index);
			PlayerStats.saveFile();
		}
		return true;
	}

}