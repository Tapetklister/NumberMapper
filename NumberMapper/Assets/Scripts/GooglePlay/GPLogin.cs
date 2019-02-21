using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPLogin : MonoBehaviour
{
    private void Awake()
    {
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();
        
        PlayGamesPlatform.DebugLogEnabled = true;
        
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(SignInCB, true);
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Authenticate(SignInCB, false);
        }
        else
        {
            PlayGamesPlatform.Instance.SignOut();
        }
    }

    public void SignInCB(bool success)
    {
        if (success)
        {
            Debug.Log(Social.localUser.userName + " signed in!");
        }
        else
        {
            Debug.Log("Error signing in");
        }
    }
}
