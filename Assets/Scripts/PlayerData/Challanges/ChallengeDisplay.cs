
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
	[SerializeField] private ProgressBar progressBar;
	
	public void ShowChallenge(Challenge challenge)
	{
		descriptionText.text = challenge + " \n" + Progress(challenge);
		if (Math.Max(0, (challenge.difficulty - 1) % difficultyArts.Length) == 0 && difficultyArt!=null)
			difficultyArt.rectTransform.sizeDelta = new Vector2(110 , 110);
		else
			if(difficultyArt != null)
				difficultyArt.rectTransform.sizeDelta = new Vector2(190 , 185);
			if(difficultyArt != null)
				difficultyArt.sprite = difficultyArts[Math.Max(0,(challenge.difficulty-1) % difficultyArts.Length)];
		levelText.text = challenge.level.ToString();
	}	
	
	public void ShowChallengeOnBoard(Challenge challenge, int level ,int goal = -1)
	{
		if (goal == -1)
		{
			goal = challenge.goal;
			progressBar.maximum = goal;
			progressBar.current = challenge.progress;
			if(challenge.incrementable && !challenge.oneRun)
				progressText.text = challenge.progress + "/" + challenge.goal;
			else
				progressText.text = " best " + challenge.progress;
			descriptionText.text = challenge + "";
		}
		else
		{
			descriptionText.text = challenge.ToString(goal);
			progressText.text = goal + "/" + goal;
		}
		rewardText.text = challenge.reward.ToString();
		levelText.text = level + "";
	}

	public void ShowCompleteChallenge(Challenge challenge)
	{
		descriptionText.text = challenge + "";
		rewardText.text = challenge.reward.ToString();
	}

	private string Progress(Challenge challenge)
	{
		if(challenge.incrementable && !challenge.oneRun)
			return  " ( " + (challenge.goal - challenge.progress) + " to go )";
		return " ( best: " + (challenge.progress) + " ) ";
	}
}
