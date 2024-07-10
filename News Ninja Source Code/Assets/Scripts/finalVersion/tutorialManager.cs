using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialManager : MonoBehaviour
{

    public GameObject publishedLockedBtn;
    public GameObject quickWordsLockedBtn;
    public GameObject contextLockedBtn;
    public GameObject[] breakingNewsWordsInResult;
    public GameObject tutorialPanel;
    public GameObject[] tutorialPopUps;
    public GameObject breakingNewsButton;
    public GameObject breakingNewsGlowingBtn;

    public GameObject tutorialUserStat;
    public GameObject gamePlayUserStat;
    public int tutorialLevelNo;
    public int levelNoTemp;
    public GameObject[] surveyScreens;
    public int surveyQuestionNo;
    public int surveyCompleted;
    private static tutorialManager instance;
    public static tutorialManager Instance
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
        //PlayerPrefs.DeleteAll();
        tutorialLevelNo = 1;
        tutorialLevelNo = PlayerPrefs.GetInt("unlockTutorial", tutorialLevelNo);
        guiManager.Instance.levelNoTextHeaderTxt.text=tutorialLevelNo.ToString();
        PlayerPrefs.SetInt("unlockTutorial", tutorialLevelNo);
        // if (tutorialLevelNo <= 1)
        // {
        //     guiManager.Instance.gameType = guiManager.GameType.tutorial;
        //     tutorialUserStat.SetActive(true);
        //     tutorialPanel.SetActive(true);
        //     tutorialPopUps[0].SetActive(true);
        // }
        if (tutorialLevelNo > 5)
        {
            guiManager.Instance.gameType = guiManager.GameType.gameStart;
            //gamePlayUserStat.SetActive(true);
            //tutorialUserStat.SetActive(false);
        }
        
        guiManager.Instance.awesomeImage.sprite=guiManager.Instance.plantsInResultPanel[PlayerPrefs.GetInt("unlockTutorial")-1];
        guiManager.Instance.levelUpImage.sprite=guiManager.Instance.plantsInResultPanel[PlayerPrefs.GetInt("unlockTutorial")];
        guiManager.Instance.homePlantImage.sprite=guiManager.Instance.plantsInResultPanel[PlayerPrefs.GetInt("unlockTutorial")-1];
        guiManager.Instance.introPopPupPlantImage.sprite=guiManager.Instance.plantsInResultPanel[PlayerPrefs.GetInt("unlockTutorial")-1];

        if(guiManager.Instance.gameType == guiManager.GameType.gameStart){
            guiManager.Instance.shopButton.interactable=true;
        }else{
            guiManager.Instance.shopButton.interactable=false;
        }
        surveyCompleted=PlayerPrefs.GetInt("surveyCompleted",surveyCompleted);
        if (surveyCompleted == 0)
        {
            //guiManager.Instance.gameType = guiManager.GameType.tutorial;
            tutorialUserStat.SetActive(true);
            tutorialPanel.SetActive(true);
            tutorialPopUps[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialLevelNo == 3)
        {
            breakingNewsButton.GetComponent<Button>().interactable = false;
            breakingNewsGlowingBtn.SetActive(false);
            contextLockedBtn.SetActive(false);
        }
        else if (tutorialLevelNo == 4)
        {
            breakingNewsButton.GetComponent<Button>().interactable = false;
            breakingNewsGlowingBtn.SetActive(false);
            contextLockedBtn.SetActive(false);
            quickWordsLockedBtn.SetActive(false);
        }
        else if (tutorialLevelNo >= 5)
        {
            breakingNewsButton.GetComponent<Button>().interactable = false;
            breakingNewsGlowingBtn.SetActive(false);
            contextLockedBtn.SetActive(false);
            quickWordsLockedBtn.SetActive(false);
            publishedLockedBtn.SetActive(false);
        }
    }

    public void showBreakingNewsWordsInResult()
    {
        for (int i = 0; i < breakingNewsWordsInResult.Length; i++)
        {
            breakingNewsWordsInResult[i].SetActive(true);
        }
    }
    public void hideBreakingNewsWordsInResult()
    {
        for (int i = 0; i < breakingNewsWordsInResult.Length; i++)
        {
            breakingNewsWordsInResult[i].SetActive(false);
        }
    }
    public void showTutorialScreen()
    {
        tutorialPanel.SetActive(true);
    }
    public void delayTutorialScreen()
    {
        Invoke("showTutorialScreen", .02f);
    }
    public void tutorialLevelUnlock()
    {
        if (tutorialLevelNo <= 5)
        {
            tutorialLevelNo += 1;
            PlayerPrefs.SetInt("unlockTutorial", tutorialLevelNo);
        }
    }
    public void surveyBackButton(){
        
        if(surveyQuestionNo>1){
            surveyQuestionNo-=1;
            prototypeManager.Instance.surveyHealthBar.value-=1;
            for(int i=0;i<surveyScreens.Length;i++){
            surveyScreens[i].SetActive(false);
        }
        surveyScreens[surveyQuestionNo-1].SetActive(true);
        
        }
        
    }
}
