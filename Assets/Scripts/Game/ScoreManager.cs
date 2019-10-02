
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private bool recordBroke;
    [SerializeField]
    private ScrollingText scoreText;
    [SerializeField] private GameObject highScoreSign;

    void Start()
    {
        OnScoreChanged(-1.2f);
        HeightFinder.ScoreChanged += OnScoreChanged;
    }

    void OnDisable()
    {
        HeightFinder.ScoreChanged -= OnScoreChanged;
    }

    private float GetHighScoreSignHeight()
    {
        return PlayerStats.Instance.highScoreHeight;
    }

    private void UpdateStats()
    {
        if(GameManager.Instance.fixedScore>PlayerStats.Instance.highScore)
        {
            if (!recordBroke)
            {
                //play animation
                AudioManager.Instance.PlaySound(AudioManager.SoundName.NewRecord);
                GameManager.Instance.oldRecord = PlayerStats.Instance.highScore;
                recordBroke = true;
            }
            PlayerStats.Instance.highScore = GameManager.Instance.fixedScore;
            PlayerStats.Instance.highScoreHeight = GameManager.Instance.height;
            PlayerStats.saveFile();
            PlayServices.Instance.addScoreToLeaderboard("",GameManager.Instance.fixedScore);
        }
    }

    private void SetScore()
    {
        if (GameManager.Instance.score != 0 && GameManager.Instance.score > GameManager.Instance.fixedScore)
        {
            GameManager.Instance.fixedScore = GameManager.Instance.score;
            PlayerStats.Instance.ReportProgress(GameManager.Instance.fixedScore,"score");
        }
        scoreText.SetNum(GameManager.Instance.fixedScore);
    }

    private void OnScoreChanged(float height)
    {
        TrackCamera.height = height;
        if (height > GameManager.Instance.startHeight)
            GameManager.Instance.score=(int)Math.Round((height-GameManager.Instance.startHeight)*10);
        GameManager.Instance.height = height;
        //set the current score
        SetScore();
        // every time record is bitten save the file and push to leaderboard
        UpdateStats();
        //sign to show where is your highScore
        if(!recordBroke)
            highScoreSign.transform.position = new Vector3(highScoreSign.transform.position.x,GetHighScoreSignHeight(),0f);
    }
	
    
}
