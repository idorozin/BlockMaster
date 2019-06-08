using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
	public static AssetsManager Instance;
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

	public Sprite GetLastCannon()
	{
		return cannons.serializedItems[PlayerStats.Instance.lastCannon].Icon;
	}
}
