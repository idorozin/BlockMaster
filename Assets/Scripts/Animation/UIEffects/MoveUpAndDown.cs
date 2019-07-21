using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;

public class MoveUpAndDown : Effect
{

	[SerializeField]
	private Vector3 up;
	[SerializeField]
	private Vector3 down;

	protected override IEnumerator Animate()
	{
		RectTransform rect = GetComponent<RectTransform>();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(rect.DOAnchorPos(up,duration));
		sequence.Append(rect.DOAnchorPos(down, duration));
		sequence.SetLoops(-1, LoopType.Restart);
		yield return null;
	}
}
