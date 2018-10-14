/*

using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayServices : MonoBehaviour {

    // ... other code here... 
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
        
    }
    public void SignInCallback(bool success) {}


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
                GPGSIds.leaderboardID,
                (bool success) =>
                {
                    Debug.Log("(game) Leaderboard update success: " + success);
                });
        }

    }
    #endregion
   
 }
*/




