using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : MonoBehaviour
{
	[SerializeField]
	private float initialSpeed;
	
	// Update is called once per frame
	void Update () {
		if(pauseMenu.GameIsPaused)
			return;
		transform.position = transform.position+Vector3.up*initialSpeed+new Vector3(0f,calcSpeed(),0f);
		if (transform.position.y > HeightFinder.height)
			HeightFinder.lives = -3;
	}

	private float calcSpeed()
	{
		return ((float) Math.Sqrt(HeightFinder.timePassed) * 0.5f + (float) Math.Sin(HeightFinder.timePassed) * 5f) *
		       0.001f;
	}
}
