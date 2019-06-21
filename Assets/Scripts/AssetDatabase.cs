
using UnityEngine;

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
		return cannons.serializedItems[PlayerStats.Instance.lastCannon].Icon;
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


}
