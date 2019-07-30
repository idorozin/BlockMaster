
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;


public class PlayServices : MonoBehaviour
{

    public static PlayServices Instance;
    
    // ... other code here... 
    void Awake()
    {
       Instance = this;
    }
    
    public void Start() {
        // Create client configuration
        PlayGamesClientConfiguration config = new 
            PlayGamesClientConfiguration.Builder()
            .Build();
        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;
        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(SignInCallback);
    }

    public void SignInCallback(bool success)
    {
        if (success) {
            Debug.Log("(Lollygagger) Signed in!");

        } else {
            Debug.Log("(Lollygagger) Sign-in failed...");
            // Show failure message
        }
    }
    
    public void TryAgain()
    {
        triedAgain = true;
        Social.localUser.Authenticate(TryAgainCallback);
    }
    
    public void TryAgainCallback(bool success)
    {
        if (success) {
            if (Social.localUser.authenticated) 
            {
                Social.ShowLeaderboardUI();
            }
        } else {
            Debug.Log("(Lollygagger) Sign-in failed...");
            // Show failure message
        }
    }

    private bool triedAgain = false;

    #region Leaderboards
    public void ShowLeaderboards() {
        if (Social.localUser.authenticated) 
        {
            Social.ShowLeaderboardUI();
        }
        else 
        {
            if(!triedAgain)
                TryAgain();
        }
    }

    public void addScoreToLeaderboard(string leaderboardID,int score)
    {
        if (Social.localUser.authenticated)
        {
            // Note: make sure to add 'using GooglePlayGames'
            Social.ReportScore(score,
                GPGSIds.leaderboard_best_high_score,
                (bool success) =>
                {
                    Debug.Log("(game) Leaderboard update success: " + success);
                });
        }

    }
    #endregion
   
 }




