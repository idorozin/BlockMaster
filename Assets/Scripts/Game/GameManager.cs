using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public float height;
	public float score;
	public float lives;
	public float timePassed;

	public bool recordBroke;
	
	public List<GameObject> shapes = new List<GameObject>();

	public Queue<Challenge> challengesCompleted = new Queue<Challenge>();
	public NotflicationAnimation anim;
	
	public static event Action GameOver = delegate { };
	public static event Action NewGame = delegate { };
	
	private float minimumLives = -3f;
	private Rigidbody2D rb2d;
	private Transform camera;
	private float fixedScore = 0;
	[SerializeField] private GameObject surface;
	[SerializeField] private GameObject highScoresign;
	[SerializeField] private TextMeshProUGUI text_;

	private bool reviveUsed = false;
	
	private float startHeight;

	private void Awake()
	{
		NewGame();
		Instance = this;
	}

	// Use this for initialization
	void Start()
	{
		startHeight = surface.transform.position.y;
		camera = GameObject.Find("Main Camera").transform;
		rb2d = GetComponent<Rigidbody2D>();
		OnScoreChanged();
		ShapeBehaviour.ShapeFell += HealthDown;
	}
	
	private void OnDisable()
	{
		ShapeBehaviour.ShapeFell -= HealthDown;
	}

	[SerializeField] private GameObject revive;
	[SerializeField] private Transform canvasPause;
	
	void HealthDown()
	{
		if (PauseMenu.GameIsPaused)
			return;
		lives--;
		if (lives <= minimumLives)
		{
			TryRevive();
		}
	}

	public void LavaReached()
	{
		TryRevive();
	}

	private void TryRevive()
	{
		Debug.Log("TryRevive");
		if (!reviveUsed && fixedScore > 50 && (PlayerStats.Instance.gold > 10 || AdManager.Instance.CanPlayRewarded()))
		{
			reviveUsed = true;
			PauseMenu.GameIsPaused = true;
			Instantiate(revive, canvasPause);
		}
		else
		{
			GameOver();
		}
	}

	public void ChallengeComplete(Challenge c)
	{
		anim.animate(c);
	}

	void Update () {
		rb2d.velocity = new Vector2(0,-2);
		timePassed += Time.deltaTime;
		surface.transform.position = new Vector3(surface.transform.position.x , DestroyShapes.height , surface.transform.position.z);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
		if(col.gameObject.name != surface.name && rb !=null && rb.velocity.x<0.1 && rb.velocity.y<0.1 && rb.velocity.x>-0.1 && rb.velocity.y>-0.1 && col.isTrigger==false)
		{
			if (col.transform.position.y > startHeight)
				score=(float)Math.Round((col.transform.position.y-startHeight)*10);
			if(transform.position.y>0)
				height=col.transform.position.y;
		}
		rb2d.position = new Vector3(camera.position.x,camera.position.y+5f,camera.position.z);
		OnScoreChanged();
	}

	#region Score
	
	private float GetHighScoreSignHeight()
	{
		return PlayerStats.Instance.highScoreHeight;
	}

	private void UpdateStats()
	{
		if(fixedScore>PlayerStats.Instance.highScore){
			PlayerStats.Instance.highScore = fixedScore;
			PlayerStats.Instance.highScoreHeight = height;
			PlayerStats.saveFile();
			PlayServices.Instance.addScoreToLeaderboard("",(int)fixedScore);
		}
	}

	private void SetScore()
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
		SetScore();
		// every time record is bitten save the file and push to leaderboard
		UpdateStats();
		//sign to show where is your highScore
		highScoresign.transform.position = new Vector3(highScoresign.transform.position.x,GetHighScoreSignHeight(),0f);
	}
	
	#endregion

}
