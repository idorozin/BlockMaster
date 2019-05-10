using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : MonoBehaviour
{
	[SerializeField]
	private float initialSpeed;
	[SerializeField] 
	private float maxSpeed=1000;
	
	// Update is called once per frame
	void Update () {
		if(pauseMenu.GameIsPaused)
			return;
		transform.position = transform.position+Vector3.up*initialSpeed+new Vector3(0f,CalcSpeed(),0f);
		if (transform.position.y > HeightFinder.height + 1.5f)
		{
			HeightFinder.lives = -3;
		}
	}

	private float CalcSpeed()
	{
		return Math.Min(maxSpeed,((float) Math.Sqrt(HeightFinder.timePassed) * 0.5f + (float) Math.Sin(HeightFinder.timePassed) * 5f) *
		       0.001f);
	}

}
