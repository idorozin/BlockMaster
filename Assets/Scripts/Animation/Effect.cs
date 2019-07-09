using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Effect : MonoBehaviour {
	
	protected Image image;

	[SerializeField]
	protected float duration = 1f;
	[SerializeField]
	protected float delay = 1f;
	
	protected virtual void OnEnable()
	{
		image = GetComponent<Image>();
		StartCoroutine(Animate());
	}
	
	protected abstract IEnumerator Animate();
}
