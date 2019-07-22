using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class Currecy : MonoBehaviour
{


	[SerializeField] private TextMeshProUGUI gold;
	[SerializeField] private TextMeshProUGUI diamond;

	private void Start()
	{
		Item.onPurchase += OnPurchase;
	}

	private void OnDestroy()
	{
		Item.onPurchase -= OnPurchase;
	}

	// Use this for initialization
	private void OnEnable()
	{
		UpdateText();
	}
	
	void OnPurchase()
	{
		UpdateText();
	}

	void UpdateText()
	{
		gold.text = PlayerStats.Instance.gold + "";
		diamond.text = PlayerStats.Instance.diamonds + "";
	}



}
