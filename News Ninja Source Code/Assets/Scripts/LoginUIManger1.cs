
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoginUIManger1 : MonoBehaviour
{
    public static LoginUIManger1 instance;
    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject resetpasswordUI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        resetpasswordUI.SetActive(false);
    }
    public void RegisterScreen() // Regester button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
        resetpasswordUI.SetActive(false);
    }
    public void ResetScreen() // Regester button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        resetpasswordUI.SetActive(true);
    }
}
