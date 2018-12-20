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


	// Use this for initialization
	public void LoadChallanges ()
	{
		int i = 0;
		foreach (Transform child in gameObject.transform)
		{
			foreach (Transform text in child.transform)
			{
				if (i <= PlayerStats.Instance.playerStats.challangeIndex)
				{
					Debug.Log(PlayerStats.Instance.playerStats.cs[i].reward);
					if (text.gameObject.name == "ChallangeTxt")
						text.gameObject.GetComponent<TextMeshProUGUI>().text =
							PlayerStats.Instance.playerStats.cs[i].challageText;
					if (text.gameObject.name == "Prize")
						text.gameObject.GetComponent<TextMeshProUGUI>().text =
							PlayerStats.Instance.playerStats.cs[i].reward;
					if(text.gameObject.name == "Process")
						text.gameObject.GetComponent<TextMeshProUGUI>().text =
							PlayerStats.Instance.playerStats.cs[i].process+"/"+PlayerStats.Instance.playerStats.cs[i].goal;
				}
				if (text.gameObject.name == "Level")
				text.gameObject.GetComponent<TextMeshProUGUI>().text =
					(i + 1).ToString();
			}

			i++;
		}
	}

	public void nextChallange()
	{
		if (PlayerStats.Instance.playerStats.cs[PlayerStats.Instance.playerStats.challangeIndex].process >
		    PlayerStats.Instance.playerStats.cs[PlayerStats.Instance.playerStats.challangeIndex].goal)
		{
			PlayerStats.Instance.playerStats.challangeIndex++;
			LoadChallanges();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
