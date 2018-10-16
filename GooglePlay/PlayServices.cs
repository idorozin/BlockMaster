

using System.Collections;
using System.Net.Mime;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class PlayServices : MonoBehaviour
{

    public static PlayServices Instance;
    [SerializeField]
    private Text status;
    
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
        PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        Debug.Log("PLAYSERVICES START");
    }

    public void SignInCallback(bool success)
    {
        if (success) {
            Debug.Log("(Lollygagger) Signed in!");
            
            // Change sign-in button text
            
            // Show the user's name
            status.text = "Signed in as: " + Social.localUser.userName;
        } else {
            Debug.Log("(Lollygagger) Sign-in failed...");
            
            // Show failure message
            status.text = "Sign-in failed";
        }
    }


    #region Leaderboard
    public void ShowLeaderboards() {
        if (PlayGamesPlatform.Instance.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }

    public void addScoreToLeaderboard(string leaderboardID,int score)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Note: make sure to add 'using GooglePlayGames'
            PlayGamesPlatform.Instance.ReportScore(score,
                GPGSIds.leaderboard_best_high_score,
                (bool success) =>
                {
                    Debug.Log("(game) Leaderboard update success: " + success);
                });
        }

    }
    #endregion
   
 }




