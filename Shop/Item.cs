using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

	private string name;
	private float price;
	
	public Item (string name,float price){
		this.name=name;
		this.price=price;
	}
	
	public string getName(){
		return this.name;
	}
	
	public float getPrice(){
		return this.price;
	}	
}
