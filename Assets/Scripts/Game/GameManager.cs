using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {
	
	public float height;
	public float score;
	public float lives;
	public float timePassed;
	private Transform camera;
	Rigidbody2D rb;
	public GameObject surface;
	
	public List<GameObject> shapes = new List<GameObject>();

	float fixedScore=0;
	[SerializeField] private GameObject highScoresign;
	[SerializeField] private TextMeshProUGUI text_;

	public static GameManager Instance;
	private float minimumLives = -3f;

	public static event Action GameOver = delegate {  };

	private void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		camera = GameObject.Find("Main Camera").transform;
		rb = GetComponent<Rigidbody2D>();
		OnScoreChanged();
		ShapeBehaviour.ShapeFell += HealthDown;
	}

	void HealthDown()
	{
		lives--;
		if (lives <= minimumLives)
			GameOver();
	}

	private void OnDisable()
	{
		ShapeBehaviour.ShapeFell -= HealthDown;
	}

	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector2(0,-2);
		timePassed += Time.deltaTime;
		surface.transform.position = new Vector3(surface.transform.position.x , DestroyShapes.height , surface.transform.position.z);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.GetComponent<Rigidbody2D>() !=null && col.gameObject.GetComponent<Rigidbody2D>().velocity.x<0.1 && col.gameObject.GetComponent<Rigidbody2D>().velocity.y<0.1 && col.gameObject.GetComponent<Rigidbody2D>().velocity.x>-0.1 && col.gameObject.GetComponent<Rigidbody2D>().velocity.y>-0.1 && col.isTrigger==false)
		{
			if (col.gameObject.name != surface.name && col.transform.position.y > surface.transform.position.y)
				score=(float)Math.Round((col.transform.position.y-surface.transform.position.y)*10);
			if(col.gameObject.name != surface.name && transform.position.y>0)
				height=col.transform.position.y;
		}
		rb.position = new Vector3(camera.position.x,camera.position.y+5f,camera.position.z);
		OnScoreChanged();
	}

	#region Score
	
	private float getHighScoreSignHeight()
	{
		return PlayerStats.Instance.highScoreHeight;
	}

	private void updateStats()
	{
		if(fixedScore>PlayerStats.Instance.highScore){
			PlayerStats.Instance.highScore=fixedScore;
			PlayerStats.Instance.highScoreHeight = height;
			PlayerStats.saveFile();
			PlayServices.Instance.addScoreToLeaderboard("",(int)fixedScore);
		}
	}

	private void setScore()
	{
		if(score!=0 && score>fixedScore)
			fixedScore=score;
		//text_.text = fixedScore.ToString();
		text_.GetComponent<ScrollingText>().SetNum((int)fixedScore);
//		PlayerStats.Instance.cs[PlayerStats.Instance.challengeIndex].setProcess((int)fixedScore , "record");
	}

	private void OnScoreChanged()
	{
		//set the current score
		setScore();
		// every time record is bitten save the file and push to leaderboard
		updateStats();
		//sign to show where is your highScore
		highScoresign.transform.position=new Vector3(highScoresign.transform.position.x,getHighScoreSignHeight(),0f);
	}
	
	#endregion

}
