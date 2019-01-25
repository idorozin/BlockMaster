using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class Item {

	private string name;
	private float price;
	private float score;
	
	public Item (string name,float price,int score)
	{
		this.name=name;
		this.price=price;
		this.score = score;
	}

	public string getName(){
			return this.name;
	}
	
	public float getPrice(){
		return this.price;
	}

	public float getScore(){
		return this.score;
	}
}
