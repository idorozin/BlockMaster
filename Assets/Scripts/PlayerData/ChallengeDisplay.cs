using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeDisplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI descriptionText;
	[SerializeField] private TextMeshProUGUI rewardText;
	[SerializeField] private Sprite difficultyArt;
	[SerializeField] private TextMeshProUGUI levelText;
	
	public void ShowChallenge(Challenge challenge)
	{
		descriptionText.text = challenge.description + Progress(challenge);
		//difficultyArt.sprite = challenge.difficulty;
		levelText.text = challenge.level.ToString();
	}

	public void ShowCompleteChallenge(Challenge challenge)
	{
		descriptionText.text = challenge.description;
		rewardText.text = challenge.reward.ToString();
	}

	private string Progress(Challenge challenge)
	{
		return " ( " + (challenge.goal - challenge.progress) + " to go )";
	}
}
