using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class score : MonoBehaviour {
	
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
		if(fixedScore>PlayerStats.highScore){
			PlayerStats.highScore=fixedScore;
			updatePlayerStats.GetComponent<updatePlayerStats>().saveFile();
		}
		highScoresign.transform.position=new Vector3(highScoresign.transform.position.x,PlayerStats.highScore+surface.transform.position.y+1f,0f);
		
	}
}
