using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ScaleIn : Effect
{
	[SerializeField]
	private Vector3 scaleFactor = new Vector3(0.75f , 0.75f, 1f);

	protected override IEnumerator Animate()
	{
		image.enabled = false;
		yield return new WaitForSecondsRealtime(delay);
		image.enabled = true;
		transform.DOScale(scaleFactor, duration);
	}
}
