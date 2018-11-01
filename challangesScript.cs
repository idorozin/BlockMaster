using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

	//TODO 
	private int countHeight;
	private int height = 10;
	private int prize = 10;
	// Use this for initialization
	void Start () {
		
	}

	void OnReachHeight()
	{
		countHeight++;
		if (countHeight > 10)
		{
			height = (int) (height * 1.5);
			prize = (int) (prize * 1.5);
			PlayerStats.money += prize;
		}
		
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
