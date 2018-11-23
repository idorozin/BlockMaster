using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class score : MonoBehaviour {
	
	public Transform surface;
	float fixedScore=0;
	public GameObject updatePlayerStats;
	public GameObject highScoresign;
	public TextMeshProUGUI text_;

	// Use this for initialization
	void Start () {
		updatePlayerStats=GameObject.Find("PlayerStats");
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
		return PlayerStats.Instance.highScoreHeight;
	}

	private void updateStats()
	{
		if(fixedScore>PlayerStats.Instance.highScore){
			PlayerStats.Instance.highScore=fixedScore;
			PlayerStats.Instance.highScoreHeight = HeightFinder.height;
			PlayerStats.Instance.saveFile();
			PlayServices.Instance.addScoreToLeaderboard("",(int)fixedScore);
		}
	}

	private void setScore()
	{
		if(HeightFinder.score!=0 && HeightFinder.score>fixedScore)
			fixedScore=HeightFinder.score;
		text_.text=(fixedScore).ToString(); 
	}
	
	
	//round diff (probably not needed)
	/*float roundDiff;
		if (PlayerStats.Instance.highScoreHeight > Math.Round(PlayerStats.Instance.highScoreHeight))
		{
			roundDiff = (float)(PlayerStats.Instance.highScoreHeight-Math.Round(PlayerStats.Instance.highScoreHeight));
		}
		else
		{
			roundDiff = (float)(-PlayerStats.Instance.highScoreHeight+Math.Round(PlayerStats.Instance.highScoreHeight));
		}*/
}
