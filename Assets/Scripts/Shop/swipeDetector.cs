﻿using System;
using UnityEngine;

public class swipeDetector : MonoBehaviour
{
	private Vector2 fingerDown;
	private Vector2 fingerUp;
	private float minDistance = 20;

	public delegate void OnSwipe(string direction);
	public event OnSwipe SwipeDetected;
	    
	void Update ()
	{
		if (!SelectShop.inShop)
			return;
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			
			switch (touch.phase)
			{
				case TouchPhase.Began:
					fingerDown = touch.position;
					fingerUp = touch.position;
				break;
				case TouchPhase.Ended:
					fingerUp = touch.position;
					CheckSwipe();
				break;
			}
		}
		
	}

	private void CheckSwipe() {
		if (swipeLongEnought())
		{
			if (isVerticalSwipe())
			{
				SendSwipe((fingerDown.y - fingerUp.y < 0 ? "Down" : "Up" ));
			}
			else
			{
				SendSwipe((fingerDown.x - fingerUp.x > 0 ? "Right" : "Left" ));
			}
		}
	}

	//swipe length , direction etc.
	#region swipeInfo
	bool swipeLongEnought()
	{
		return getVerticalMovment() > minDistance || getHorizontalMovment() > minDistance;
	}

	float getHorizontalMovment()
	{
		return Math.Abs(fingerDown.x-fingerUp.x);
	}
	
	
	float getVerticalMovment()
	{
		return Math.Abs(fingerDown.y-fingerUp.y);
	}

	bool isVerticalSwipe()
	{
		return getHorizontalMovment() < getVerticalMovment();
	}
	#endregion;

	private void SendSwipe(string direction)
	{
		if(SwipeDetected != null)
			SwipeDetected(direction);
	}
	
	
	
}
