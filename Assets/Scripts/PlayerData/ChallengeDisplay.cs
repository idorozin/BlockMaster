
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeDisplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI descriptionText;
	[SerializeField] private TextMeshProUGUI rewardText;
	[SerializeField] private TextMeshProUGUI progressText;
	[SerializeField] private Image difficultyArt;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField]private Sprite[] difficultyArts;
	
	public void ShowChallenge(Challenge challenge)
	{
		descriptionText.text = challenge.description + Progress(challenge);
		if (Math.Max(0, (challenge.difficulty - 1) % difficultyArts.Length) == 0 && difficultyArt!=null)
			difficultyArt.rectTransform.sizeDelta = new Vector2(110 , 110);
		else
		if(difficultyArt != null)
			difficultyArt.rectTransform.sizeDelta = new Vector2(190 , 185);
		if(difficultyArt != null)
		difficultyArt.sprite = difficultyArts[Math.Max(0,(challenge.difficulty-1) % difficultyArts.Length)];
		levelText.text = challenge.level.ToString();
	}	
	
	public void ShowChallengeOnBoard(Challenge challenge)
	{
		descriptionText.text = challenge.description;
		progressText.text = challenge.goal + "/" + challenge.goal;
		rewardText.text = challenge.reward.ToString();
		levelText.text = challenge.level.ToString();
	}

	public void ShowCompleteChallenge(Challenge challenge)
	{
		descriptionText.text = challenge.description;
		rewardText.text = challenge.reward.ToString();
	}

	private string Progress(Challenge challenge)
	{
		return " \n( " + (challenge.goal - challenge.progress) + " to go )";
	}
}
