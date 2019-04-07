using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class Item
{
	public string Name {get; private set;}
	public float Price { get; private set;}
	public float Score { get; private set;}

	/*public enum Name
	{
	1, 2
	};*/
	//TODO change string to enum (maybe)
	public Item (string name,float price,int score)
	{
		this.Name=name;
		this.Price=price;
		this.Score = score;
	}
}
