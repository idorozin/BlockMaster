
using UnityEngine;
using System.Linq;
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
		return cannons.serializedItems.First(cannon => cannon.Id  == PlayerStats.Instance.lastCannon).Icon;
		//cannons.serializedItems.Find(PlayerStats.Instance.lastCannon);
	}

	public bool CanBuyItem()
	{
		return CanBuyItem(cannons);
	}

	private bool CanBuyItem(Category cannons)
	{
		foreach (Item item in cannons.serializedItems)
		{
			if (PlayerStats.Instance.gold >= item.Gold)
				return true;
		}
		return false;
	}

	[ContextMenu("SetIds")]
	public void SetIds()
	{
		SetIds(cannons);
		SetIds(platforms);
		SetIds(trails);
		SetIds(flames);
	}
	public void SetIds(Category c)
	{
		ResetIds(c);
		int i=0;
		if(c == null)
			return;
		foreach(Item item in c.serializedItems)
		{
			bool idDoesntExists = false;
			while(!idDoesntExists)
			{
				idDoesntExists = true;
				foreach(Item item_ in c.serializedItems)
				{
					if(item_.Id == i)
					{
						idDoesntExists = false;
						i++;
						break;
					}
				}
			}
			item.Id = i;
		}
	}


	[ContextMenu("PrintIds")]
	public void printIds()
	{
		PrintIds(cannons);
		PrintIds(platforms);
		PrintIds(trails);
		PrintIds(flames);
	}

	void PrintIds(Category c)
	{
		if(c==null)
			return;
		foreach(Item item in c.serializedItems)
		{
			Debug.Log(item.Id + " : " + item.Name);
		}
	}

	void ResetIds(Category c)
	{
		if(c==null)
			return;
		foreach(Item item in c.serializedItems)
		{
			item.Id = 0;
		}
	}


}
