using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUi : MonoBehaviour
{
	public static HeartsUi heartsUi;

	private void Awake()
	{
		heartsUi = this;
	}

	// Use this for initialization
	void Start () {
		setInitialHearts(3);
	}

	[SerializeField]
	private GameObject heart;
	[SerializeField]
	private GameObject PanelHearts;
	[SerializeField] 
	private Color fadedHeartColor;
	
	
	public void setInitialHearts(int amount)
	{
		for (int i = 0; i < amount; i++)
			Instantiate(heart,PanelHearts.transform);
	}

	public void heartsDown(int index)
	{
		PanelHearts.transform.GetChild(index).GetComponent<Image>().color = fadedHeartColor;
	}
}
