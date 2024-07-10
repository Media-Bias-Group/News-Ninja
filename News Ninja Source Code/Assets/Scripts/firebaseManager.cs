using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using Random = UnityEngine.Random;
using shuffle_list;
using UnityEngine.Networking;

public class firebaseManager : MonoBehaviour
{
    public static firebaseManager instance;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    public string displayName;
    public string emailAddress;
    public string photoUrl;

    public string UserId;

    void Awake()
    {



        InitializeFirebase();

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // userMoney = (int)Convert.ToInt32(databaseManager.instance.singleselect("Users", "money", "id", UserId,result));





    }


    public void InitializeFirebase()
    {

        //initialize firebase auth
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                // Debug.Log("Signed in " + user.UserId);
                UserId = user.UserId ?? "";
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
            }
        }
    }




    public void updateCash(int money)
    {
        // databaseManager.instance.updateRow("Users", new string[] { "money" }, new System.Object[] { money }, "id", UserId);
        StartCoroutine(databaseManager.instance.updateValue("users", "money", new System.Object[] { money }, "id", UserId));
    }


}