
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeDisplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI descriptionText;
	[SerializeField] private TextMeshProUGUI rewardText;
	[SerializeField] private Image difficultyArt;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField]private Sprite[] difficultyArts;
	
	public void ShowChallenge(Challenge challenge)
	{
		descriptionText.text = challenge.description + Progress(challenge);
		difficultyArt.sprite = difficultyArts[challenge.difficulty-1];
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
