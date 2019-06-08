using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Category : ScriptableObject
{
	public List<Item> serializedItems = new List<Item>();
}
