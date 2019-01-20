using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class score : MonoBehaviour {
	
	float fixedScore=0;
	public GameObject highScoresign;
	public TextMeshProUGUI text_;

	// Use this for initialization
	void Start () {
		text_ = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		//set the current score
		setScore();
		// every time record is bitten the file is saved
		updateStats();
		//sign to show where is your highScore
		highScoresign.transform.position=new Vector3(highScoresign.transform.position.x,getHighScoreSignHeight(),0f);
		
	}

	private float getHighScoreSignHeight()
	{
		return PlayerStats.Instance.playerStats.highScoreHeight;
	}

	private void updateStats()
	{
		if(fixedScore>PlayerStats.Instance.playerStats.highScore){
			PlayerStats.Instance.playerStats.highScore=fixedScore;
			PlayerStats.Instance.playerStats.highScoreHeight = HeightFinder.height;
			PlayerStats.Instance.saveFile();
			PlayServices.Instance.addScoreToLeaderboard("",(int)fixedScore);
		}
	}

	private void setScore()
	{
		if(HeightFinder.score!=0 && HeightFinder.score>fixedScore)
			fixedScore=HeightFinder.score;
		text_.text=(fixedScore).ToString();
		PlayerStats.Instance.playerStats.cs[PlayerStats.Instance.playerStats.challangeIndex].setProcess((int)fixedScore , "record");
	}
	
	
	
}
