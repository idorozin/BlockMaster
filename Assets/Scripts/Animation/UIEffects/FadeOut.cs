using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : Effect
{
	private Image image;
	protected override IEnumerator Animate()
	{
		yield return new WaitForSecondsRealtime(delay);
		image.DOColor(new Color(0.8f , 0.2f ,1f , 0.8f), duration);
	}
}
