using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailEffect : MonoBehaviour
{
	[SerializeField] private float spawnRate;
	private float nextSpawn;
	[SerializeField] private GameObject effect;
	
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time < nextSpawn)
			return;
		GameObject instance = (GameObject)Instantiate(effect,transform.position,Quaternion.identity);
		nextSpawn = Time.time + spawnRate;
		Destroy(instance, 0.3f);
	}
}
