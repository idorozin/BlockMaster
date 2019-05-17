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

	private Transform height;

	private void Start()
	{
		height = Camera.main.transform;
	}

	// Update is called once per frame
	void Update () {
		if(PauseMenu.GameIsPaused)
			return;
		transform.position = transform.position+Vector3.up*initialSpeed+new Vector3(0f,CalcSpeed(),0f);
		if (transform.position.y > height.transform.position.y)//(GameManager.Instance.height + 2f))
		{
			GameManager.Instance.lives = -3;
		}
	}

	private float CalcSpeed()
	{
		return Math.Min(maxSpeed,((float) Math.Sqrt(GameManager.Instance.timePassed*0.5f) * 0.5f + (float) Math.Sin(GameManager.Instance.timePassed) * 5f) *
		       0.001f);
	}

}
