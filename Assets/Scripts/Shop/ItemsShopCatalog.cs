using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemsShopCatalog : ScriptableObject
{
    public List<ListWraper> serializedItems = new List<ListWraper>();
}

