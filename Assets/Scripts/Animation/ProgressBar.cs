using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{

	[SerializeField]
	private Image image;
	public int current = 1;
	public int maximum = 1;

	private void Update ()
	{
		SetFill();
	}

	private void SetFill()
	{
		float fillAmount = current / Math.Max(maximum,0.01f);
		image.fillAmount = fillAmount;
	}
}
