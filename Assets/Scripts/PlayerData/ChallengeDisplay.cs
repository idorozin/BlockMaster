using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeDisplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI descriptionText;
	[SerializeField] private Sprite difficultyArt;
	[SerializeField] private TextMeshProUGUI levelText;
	
	public void ShowChallenge(Challenge challenge)
	{
		descriptionText.text = challenge.description;
		//difficultyArt.sprite = challenge.difficulty;
		levelText.text = challenge.level.ToString();
	}
}
