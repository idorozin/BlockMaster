using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Effect : MonoBehaviour {
	

	[SerializeField]
	protected float duration = 1f;
	[SerializeField]
	protected float delay = 1f;
	
	protected virtual void OnEnable()
	{
		StartCoroutine(Animate());
	}
	
	protected abstract IEnumerator Animate();
}
