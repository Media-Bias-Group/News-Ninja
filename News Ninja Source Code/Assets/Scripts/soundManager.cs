using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource simpleBtnClick;
    public AudioSource starColletion;
    public AudioSource wrongAnswer;
    public AudioSource levelSuccess;
    private static soundManager instance;
    public static soundManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //simpleBtnClick.GetComponent<AudioSource>().Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void simpleBtnSound(){
        simpleBtnClick.Play();
    }
    public void starCollectSound(){
        starColletion.Play();

    }
     public void correctAnsSound(){
        starColletion.Play();

    }
    public void wrongAnsSound(){
        wrongAnswer.Play();
    }
    public void levelSuccessSound(){
        levelSuccess.Play();
    }
}
