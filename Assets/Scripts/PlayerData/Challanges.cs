using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Challanges : MonoBehaviour
{
	[SerializeField]
	private GameObject challengeDisplay;	
	[SerializeField]
	private GameObject challengeCompleteDisplay;

	[SerializeField] 
	private Transform contentPanel;
	
	private void Start()
	{
		foreach (Challenge challenge in PlayerStats.Instance.challenges)
		{
			if (challenge.completed)
			{
				GameObject go = Instantiate(challengeCompleteDisplay, contentPanel);
				go.GetComponent<ChallengeDisplay>().ShowChallengeOnBoard(challenge);
			}
		}
		foreach (Challenge challenge in PlayerStats.Instance.challenges)
		{
			if (!challenge.completed)
			{
				GameObject go = Instantiate(challengeDisplay, contentPanel);
				go.GetComponent<ChallengeDisplay>().ShowChallengeOnBoard(challenge);
			}
		}
	}
}
