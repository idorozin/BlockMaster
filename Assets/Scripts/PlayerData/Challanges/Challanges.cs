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
		int i = 0;
		foreach (Challenge challenge in PlayerStats.Instance.challenges)
		{
			i += challenge.previousGoals.Count;
		}
		foreach (Challenge challenge in PlayerStats.Instance.challenges)
		{
			if (challenge.isActive)
			{
				i++;
				GameObject go = Instantiate(challengeDisplay, contentPanel);
				go.GetComponent<ChallengeDisplay>().ShowChallengeOnBoard(challenge,i);
			}
		}

		i = 0;
		foreach (Challenge challenge in PlayerStats.Instance.challenges)
		{
			foreach (var goal in challenge.previousGoals)
			{
				i++;
				GameObject go = Instantiate(challengeCompleteDisplay, contentPanel);
				go.GetComponent<ChallengeDisplay>().ShowChallengeOnBoard(challenge, i, goal);
			}
		}
	}
}
