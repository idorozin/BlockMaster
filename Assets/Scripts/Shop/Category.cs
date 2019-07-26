using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Category : ScriptableObject
{
	public List<Item> serializedItems = new List<Item>();

	public Item.ItemType type;

	public float  x ,y, width, height , rotation;
	public IEnumerator<object> GetEnumerator()
	{
		throw new System.NotImplementedException();
	}
}
