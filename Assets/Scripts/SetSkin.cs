
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class SetSkin : MonoBehaviour
{
	private SpriteRenderer sprite;

	[SerializeField]
	private Image image;

	[SerializeField]
	private SpriteRenderer renderer;

	public string type = "cannon";
	
	void Start ()
	{
		if (type == "cannon")
		{
			if (image != null)
			{
				image.sprite = AssetDatabase.Instance.GetLastCannon();
			}	
			else if (renderer != null)
			{
				renderer.sprite = AssetDatabase.Instance.GetLastCannon();
			}
		}
	}
}
