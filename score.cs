using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class score : MonoBehaviour {
	//TODO : set score in HeigthFinder class and use only one varible;
	
	public Text text_;
	public Transform surface;
	float fixedScore=0;
	public GameObject updatePlayerStats;
	public GameObject highScoresign;

	// Use this for initialization
	void Start () {
		updatePlayerStats=GameObject.Find("PlayerStats");
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
		float roundDiff;
		if (PlayerStats.highScoreHeight > Math.Round(PlayerStats.highScoreHeight))
		{
			roundDiff = (float)(PlayerStats.highScoreHeight-Math.Round(PlayerStats.highScoreHeight));
		}
		else
		{
			roundDiff = (float)(-PlayerStats.highScoreHeight+Math.Round(PlayerStats.highScoreHeight));
		}
		Debug.Log(roundDiff);
		return PlayerStats.highScoreHeight - surface.transform.position.y;
	}

	private void updateStats()
	{
		if(fixedScore>PlayerStats.highScore){
			PlayerStats.highScore=fixedScore;
			PlayerStats.highScoreHeight = HeightFinder.height;
			updatePlayerStats.GetComponent<updatePlayerStats>().saveFile();
			PlayServices.Instance.addScoreToLeaderboard("",(int)fixedScore);
		}
	}

	private void setScore()
	{
		if(HeightFinder.score!=0 && HeightFinder.score>fixedScore)
			fixedScore=HeightFinder.score;
		text_.text=(fixedScore).ToString(); 
	}
}
