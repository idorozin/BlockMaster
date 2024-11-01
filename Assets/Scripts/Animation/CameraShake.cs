﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

	[SerializeField] private float magnitude= 0.02f;
	[SerializeField] private float duaration = 1;
	

	public IEnumerator ShakeCamera()
	{
		Vector3 originalPos = transform.position;

		float elapsed = 0.0f;

		while (elapsed < duaration)
		{
			while(PauseMenu.GameIsPaused)
				yield return null;
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;
			
			transform.localPosition = new Vector3(originalPos.x + x , originalPos.y + y , originalPos.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = originalPos;
	}
}
