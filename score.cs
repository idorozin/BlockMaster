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
		if(HeightFinder.score!=0 && (float)Math.Round((HeightFinder.score-surface.position.y))>fixedScore)
			fixedScore=(float)Math.Round(HeightFinder.score);
		text_.text=(fixedScore*10).ToString(); 
		// text_.text=updatePlayerStats.GetComponent<updatePlayerStats>().saveFiles();
		// every time record is bitten the file is saved
		if(fixedScore>PlayerStats.highScore/10){
			PlayerStats.highScore=fixedScore*10;
			updatePlayerStats.GetComponent<updatePlayerStats>().saveFile();
			PlayServices.Instance.addScoreToLeaderboard("",(int)fixedScore);
		}
		highScoresign.transform.position=new Vector3(highScoresign.transform.position.x,PlayerStats.highScore/10+surface.transform.position.y+1f,0f);
		
	}
}
