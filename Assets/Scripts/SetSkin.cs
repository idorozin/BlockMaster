
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SetSkin : MonoBehaviour
{
	private SpriteRenderer sprite;

	[SerializeField] private string path = "Cannons/";
	private string type = "cannon";
	
	void Start ()
	{
		if(type == "cannon")
			GetComponent<SpriteRenderer>().sprite = AssetDatabase.Instance.GetLastCannon();
		
	}
}
