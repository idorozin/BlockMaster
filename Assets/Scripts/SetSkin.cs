
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
	[SerializeField]
	private Animator animator;

	public Item.ItemType type;
	
	void Start ()
	{
		Sprite sprite;
		if (type == Item.ItemType.Cannon)
		{
			sprite = AssetDatabase.Instance.GetLastCannon();
			if(sprite == null)
				return;
			if (image != null)
			{
				image.sprite = AssetDatabase.Instance.GetLastCannon();
			}	
			else if (renderer != null)
			{
				renderer.sprite = AssetDatabase.Instance.GetLastCannon();
			}
		}		
		if (type == Item.ItemType.Platform)
		{
			sprite = AssetDatabase.Instance.GetLastPlatform();
			if(sprite == null)
				return;
			if (image != null)
			{
				image.sprite = AssetDatabase.Instance.GetLastPlatform();
			}	
			else if (renderer != null)
			{
				renderer.sprite = AssetDatabase.Instance.GetLastPlatform();
			}
		}	
		if (type == Item.ItemType.Engine)
		{
			var anim = AssetDatabase.Instance.GetLastFlame();
			if(anim == null)
				return;
			if (animator != null)
			{
				animator.runtimeAnimatorController = AssetDatabase.Instance.GetLastFlame();
			}	
		}
	}
}
