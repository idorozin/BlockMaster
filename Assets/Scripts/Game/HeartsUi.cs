using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUi : MonoBehaviour
{
	private int index;

	// Use this for initialization
	void Start ()
	{
		index = 0;
		SetInitialHearts(3);
		ShapeBehaviour.ShapeFell += HeartsDown;
	}

	[SerializeField]
	private GameObject heart;
	[SerializeField]
	private GameObject PanelHearts;
	[SerializeField] 
	private Color fadedHeartColor;
	
	
	public void SetInitialHearts(int amount)
	{
		foreach (Transform child in PanelHearts.transform)
		{
			Destroy(child.gameObject);
		}
		for (int i = 0; i < amount; i++)
			Instantiate(heart,PanelHearts.transform);
	}

	public void HeartsDown()
	{
		if(PanelHearts.transform.childCount <= index) return;
		Transform t = PanelHearts.transform.GetChild(index);
		if (t != null)
			t.GetComponent<Image>().color = fadedHeartColor;
		index++;
	}

	private void OnDisable()
	{
		ShapeBehaviour.ShapeFell -= HeartsDown;
	}
}
