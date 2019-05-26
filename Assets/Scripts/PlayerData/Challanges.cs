using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Challanges : MonoBehaviour
{
	[SerializeField]
	private GameObject challengeDisplay;

	[SerializeField] 
	private Transform contentPanel;
	
	private void Start()
	{
		foreach (Challenge challenge in PlayerStats.Instance.challenges)
		{
			GameObject go = Instantiate(challengeDisplay , contentPanel);
			go.GetComponent<ChallengeDisplay>().ShowCompleteChallenge(challenge);
		}
	}

	/*private void Awake()
	{
		Debug.Log("this and that");
		Instance = this;
	}

	public static Challanges Instance;
	[SerializeField] private GameObject challenge;
	[SerializeField] private Sprite skip , tick , collect , yellow;


	// Use this for initialization
	public void LoadChallanges ()
	{
		PlayerStats.Instance.challengeIndex = 0;
		DestroyUI();
		for(int i=0;i<PlayerStats.Instance.cs.Length;i++)
		{
			GameObject obj=Instantiate(challenge, gameObject.transform);
			foreach (Transform child in obj.transform)
			{
				if (i <= PlayerStats.Instance.challengeIndex)
				{
					obj.GetComponent<Image>().sprite = yellow;
					if (child.gameObject.name == "ChallangeTxt")
						child.gameObject.GetComponent<TextMeshProUGUI>().text =
							PlayerStats.Instance.cs[i].challageText;
					if (child.gameObject.name == "Prize")
						child.gameObject.GetComponent<TextMeshProUGUI>().text =
							PlayerStats.Instance.cs[i].reward;
					if(child.gameObject.name == "Process")
						child.gameObject.GetComponent<TextMeshProUGUI>().text =
							PlayerStats.Instance.cs[i].process+"/"+PlayerStats.Instance.cs[i].goal;
					if (child.gameObject.GetComponent<Button>() != null)
					{
						if (i < PlayerStats.Instance.challengeIndex)
						{
							child.gameObject.transform.parent.gameObject.GetComponent<Image>().sprite = tick;
							Destroy(child.gameObject);
						}
						if (i == PlayerStats.Instance.challengeIndex)
						{
							child.gameObject.GetComponent<Image>().enabled = true;
							child.GetComponent<Button>().onClick.AddListener(skipChallange);
							if (PlayerStats.Instance.cs[PlayerStats.Instance.challengeIndex]
								    .process <
							    PlayerStats.Instance.cs[PlayerStats.Instance.challengeIndex]
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
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(-62.1f,PlayerStats.Instance.cs.Length*80f);
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
		if (PlayerStats.Instance.money >= 200)
			PlayerStats.Instance.money -= 200;
		PlayerStats.Instance.challengeIndex++;
		PlayerStats.saveFile();
		LoadChallanges();
	}

*/

}
