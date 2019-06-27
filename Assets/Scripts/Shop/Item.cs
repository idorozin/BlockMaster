﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
	public int Id;
	public RuntimeAnimatorController Animator;
	
	public enum ItemType
	{
		Cannon , Platform , Engine , Trail
	}

	public Item()
	{
		SetId();
	}
	public Item(string name , Sprite icon)
	{
		this.Name = name;
		this.Icon = icon;
		SetId();
	}	
	public Item(string name , Sprite icon , RuntimeAnimatorController animator)
	{
		this.Name = name;
		this.Icon = icon;
		this.Animator = animator;
		SetId();
	}

	void SetId()
	{
		Id = AssetDatabaseHelper.GetUniqueId();
	}

	public void Buy()
	{
		if (Score > PlayerStats.Instance.highScore || PlayerStats.Instance.ItemsOwned.Contains(Id))
			return;	
		if(PlayerStats.Instance.gold >= Gold && PlayerStats.Instance.diamonds >= Diamonds){
			PlayerStats.Instance.ItemsOwned.Add(Id);
			PlayerStats.Instance.gold -= Gold;
			PlayerStats.Instance.diamonds -= Diamonds;
			PlayerStats.saveFile();
		}
	}

	public void Use()
	{
		if (PlayerStats.Instance.lastCannon == Id || !PlayerStats.Instance.ItemsOwned.Contains(Id))
			return;
		switch (type)
		{
			case ItemType.Cannon:
				PlayerStats.Instance.lastCannon = Id;
				break;
			case ItemType.Platform:
				PlayerStats.Instance.lastPlatform = Id;
				break;
			case ItemType.Engine:
				PlayerStats.Instance.lastFlame = Id;
				break;
			case ItemType.Trail:
				PlayerStats.Instance.lastTrail = Id;
				break;
		}
	}

	public bool Unlock()
	{
		if (Score <= PlayerStats.Instance.highScore)
		{
			PlayerStats.Instance.ItemsUnlocked.Add(Id);
			PlayerStats.saveFile();
		}
		return true;
	}

	public bool Unlocked()
	{
		return PlayerStats.Instance.ItemsUnlocked.Contains(Id);
	}

}