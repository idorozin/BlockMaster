using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Challange : MonoBehaviour
{
	private void Awake()
	{
		Instance = this;
	}

	public static Challange Instance;

	[SerializeField]private GameObject Challanges;

	// Use this for initialization
	public void LoadChallanges ()
	{
		PlayerStats.Instance.playerStats.SetChallanges();
		int i = 0;
		foreach (Transform child in Challanges.transform)
		{
			foreach (Transform text in child.transform)
			{
				if (text.gameObject.name == "ChallangeTxt")
					text.gameObject.GetComponent<TextMeshProUGUI>().text=PlayerStats.Instance.playerStats.cs[i].challageText;
				if(text.gameObject.name=="PrizeTxt")
					text.gameObject.GetComponent<TextMeshProUGUI>().text=PlayerStats.Instance.playerStats.cs[i].reward();
			}

			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
