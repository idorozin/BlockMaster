using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
	[SerializeField] 
	private GameObject starPrefab;
	[SerializeField]
	private int amount;
	[SerializeField]
	private Camera camera;

	private bool created = false;
	
	private void Create()
	{
		for (int i = 0; i < amount; i++)
		{
			GameObject go = Instantiate(starPrefab);
			go.transform.parent = transform;
		}
	}

	[SerializeField]
	private Transform startPosition;
	private void Update()
	{
		if (camera.transform.position.y > startPosition.position.y && !created)
		{
			created = !created;
			Create();
		}
	}
	

}
