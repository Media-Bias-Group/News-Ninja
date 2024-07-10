using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quickWordTimer : MonoBehaviour
{
    public GameObject clockImage;
    public GameObject pauseImage;
    public Slider timerSlider;
    public float timerValue;
    public Text showRemainingTimer;
    public Text timerPopUp;
    public int tappedWordCounter;
    public GameObject[] foundWords;
    public GameObject timerSliderBg;
    private static quickWordTimer instance;
    public bool wordsInserted=false;
    public static quickWordTimer Instance
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
    // Start is called before the first frame update
    void Start()
    {
        timerValue = 120;
        pauseImage.SetActive(false);
        timerPopUp.text="";

    }

    // Update is called once per frame
    void Update()
    {
        // if(guiManager.Instance.gameModeText=="")  
        if (timerValue >= 0 && guiManager.Instance.gameMode==guiManager.GameMode.quickWordMode)
        {
            timerValue -= Time.deltaTime;
        }

        //timerValue= (int)(timerValue * 100f) / 100f;
        timerSlider.value = timerValue;
        showRemainingTimer.text = (timerValue).ToString("0") + "s";
        if (timerSlider.value <= 0)
        {
            if(!wordsInserted){
            databaseManager.instance.insertNonSelectedWords();
            wordsInserted=true;
            }
            if(!guiManager.Instance.selectedQuickWords.activeSelf)
            guiManager.Instance.showStaricPanel();
            showRemainingTimer.text="0s";
            //tutorialManager.Instance.tutorialLevelUnlock();
            //timerSlider.value=60;
        }
        if(guiManager.Instance.gameModeText == "quickwords-mode"){
            if(QuestionsManager.Instance.questionNo>QuestionsManager.Instance.totalQuestions){
            Time.timeScale=0;
             }
        }
        

    }
    public void addTimerPopUp(){
        timerPopUp.text="+5 s";
        timerValue += 5;
        timerPopUp.GetComponent<Text>().color=Color.green;
        timerSliderBg.GetComponent<Image>().color=Color.green;
        inAppEarning.Instance.earnDollar+=10;
        PlayerPrefs.SetInt("saveDoller", inAppEarning.Instance.earnDollar);
        inAppEarning.Instance.headerCashText.text=inAppEarning.Instance.earnDollar.ToString();
        Invoke("hideTimerPopUp",1.5f);
    }
    public void minusTimerPopUp(){
        timerPopUp.text="-5 s";
        timerValue -= 5;
        timerPopUp.GetComponent<Text>().color=Color.red;
        timerSliderBg.GetComponent<Image>().color=Color.red;
        Invoke("hideTimerPopUp",1.5f);
    }
    void hideTimerPopUp(){
        timerPopUp.text="";
        timerSliderBg.GetComponent<Image>().color=Color.green;
    }
}
