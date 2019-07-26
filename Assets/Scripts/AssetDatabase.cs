
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GooglePlayGames.Native.Cwrapper;
using UnityEngine.Experimental.AI;
using Random = UnityEngine.Random;

public class AssetDatabase : MonoBehaviour
{
	public static AssetDatabase Instance;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public Category cannons;
	public Category platforms;
	public Category trails;
	public Category flames;	

	public Sprite GetLastCannon()
	{
		if (PlayerStats.Instance.lastCannon == 0)
			return null;
		return cannons.serializedItems.First(cannon => cannon.Id == PlayerStats.Instance.lastCannon).Icon;
	}
	
	public Item GetRandomCannon()
	{
		var cannons = from c in this.cannons.serializedItems
			where c.Gold < 500 where !PlayerStats.Instance.ItemsOwned.Contains(c.Id)
			select c;
		Item cannon = null;
		int m = cannons.Count();
		int r = Random.Range(0, m);
		int i = 0;
		foreach (var c in cannons)
		{
			i++;
			if (i == r)
			{
				cannon = c;
			}
		}
		return cannon;
	}	
	
	public Sprite GetLastPlatform()
	{
		if (PlayerStats.Instance.lastPlatform == 0)
			return null;
		return platforms.serializedItems.First(platform => platform.Id == PlayerStats.Instance.lastPlatform).Icon;
	}	
	public TrailRenderer GetLastTrail()
	{
		//return trails.serializedItems.First(cannon => cannon.Id  == PlayerStats.Instance.lastTrail).Icon;
		return null;
	}	
	public RuntimeAnimatorController GetLastFlame()
	{
		if (PlayerStats.Instance.lastFlame == 0)
			return null;
		return flames.serializedItems.First(flame => flame.Id == PlayerStats.Instance.lastFlame).Animator;
	}
	

	public bool CanBuyItem()
	{
		return CanBuyItem(cannons) || CanBuyItem(platforms) || CanBuyItem(trails) || CanBuyItem(flames);
	}

	private bool CanBuyItem(Category c)
	{
		if (c == null)
			return false;
		foreach (Item item in c.serializedItems)
		{
			if (PlayerStats.Instance.gold >= item.Gold && item.Score <= PlayerStats.Instance.highScore)
				return true;
		}
		return false;
	}
	
	private void OnValidate()
	{
		List<Category> categories = new List<Category>();
		categories.Add(cannons);
		categories.Add(platforms);
		categories.Add(trails);
		categories.Add(flames);
		AssetDatabaseHelper.categories = categories;
	}
	
	[ContextMenu("SetIds")]
	public void SetIds()
	{
		AssetDatabaseHelper.SetIds();
	}	
	[ContextMenu("PrintIds")]
	public void PrintIds()
	{
		AssetDatabaseHelper.PrintIds();
	}

}
