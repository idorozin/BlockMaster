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
	[SerializeField] private GameObject challange;
	[SerializeField] private Sprite skip , tick , collect , yellow;


	// Use this for initialization
	public void LoadChallanges ()
	{
		PlayerStats.Instance.playerStats.challangeIndex = 0;
		DestroyUI();
		for(int i=0;i<PlayerStats.Instance.playerStats.cs.Length;i++)
		{
			Debug.Log(i);
			GameObject obj=Instantiate(challange, gameObject.transform);
			foreach (Transform child in obj.transform)
			{
				if (i <= PlayerStats.Instance.playerStats.challangeIndex)
				{
					obj.GetComponent<Image>().sprite = yellow;
					if (child.gameObject.name == "ChallangeTxt")
						child.gameObject.GetComponent<TextMeshProUGUI>().text =
							PlayerStats.Instance.playerStats.cs[i].challageText;
					if (child.gameObject.name == "Prize")
						child.gameObject.GetComponent<TextMeshProUGUI>().text =
							PlayerStats.Instance.playerStats.cs[i].reward;
					if(child.gameObject.name == "Process")
						child.gameObject.GetComponent<TextMeshProUGUI>().text =
							PlayerStats.Instance.playerStats.cs[i].process+"/"+PlayerStats.Instance.playerStats.cs[i].goal;
					if (child.gameObject.GetComponent<Button>() != null)
					{
						if (i < PlayerStats.Instance.playerStats.challangeIndex)
						{
							child.gameObject.transform.parent.gameObject.GetComponent<Image>().sprite = tick;
							Destroy(child.gameObject);
						}
						if (i == PlayerStats.Instance.playerStats.challangeIndex)
						{
							child.gameObject.GetComponent<Image>().enabled = true;
							child.GetComponent<Button>().onClick.AddListener(skipChallange);
							if (PlayerStats.Instance.playerStats.cs[PlayerStats.Instance.playerStats.challangeIndex]
								    .process <
							    PlayerStats.Instance.playerStats.cs[PlayerStats.Instance.playerStats.challangeIndex]
								    .goal)
								child.gameObject.GetComponent<Image>().sprite = skip;
						}
					}
				}
				if (child.gameObject.name == "Level")
					child.gameObject.GetComponent<TextMeshProUGUI>().text =
						(i + 1).ToString();
			}
		}
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(-62.1f,PlayerStats.Instance.playerStats.cs.Length*80f);
	}

	private void DestroyUI()
	{
		foreach (Transform child in gameObject.transform)
		{
			Destroy(child.gameObject);
		}
	}

	public void skipChallange()
	{
		if (PlayerStats.Instance.playerStats.money >= 200)
			PlayerStats.Instance.playerStats.money -= 200;
		PlayerStats.Instance.playerStats.challangeIndex++;
		PlayerStats.Instance.saveFile();
		LoadChallanges();
	}


}
