using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
	[SerializeField] private float spawnRate;
	private float nextSpawn;
	[SerializeField] private GameObject effect;
	private static GameObject trail;

	private void Start()
	{
		if(trail == null)
			trail = new GameObject("Trail");
	}

	// Update is called once per frame
	private void Update ()
	{
		if (Time.time < nextSpawn)
			return;
		GameObject instance = (GameObject)Instantiate(effect,transform.position,Quaternion.identity , trail.transform);
		nextSpawn = Time.time + spawnRate;
		Destroy(instance, 0.3f);
	}
}
