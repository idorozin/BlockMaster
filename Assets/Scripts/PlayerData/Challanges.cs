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
}
