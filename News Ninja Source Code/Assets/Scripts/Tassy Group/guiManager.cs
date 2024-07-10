using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class guiManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public string gameModeText;
    public GameObject homePanel;
    public GameObject topicListPanel;
    public GameObject gameModePanel;
    public GameObject levelCompletePanel;
    public GameObject sentenceResultPanel;
    public GameObject staricPanel;
    public GameObject lockedPanel;
    public GameObject attemptedQuestionsAnswers;
    public GameObject selectedQuickWords;
    public GameObject sentenceHeader;
    public GameObject quickWordHeader;
    public GameObject biasedButton;
    public GameObject notBiasedButton;
    public GameObject whiteFeedBackPanel;
    public Sprite[] biasedBtnSprites;
    public Sprite[] notBiasedBtnSprites;
    public GameObject quickWordNextButton;
    public GameObject publishModeWholeScreenBtn;

    public GameObject sentenceModeMotivationMessage;
    public GameObject insideHeaderPanel;
    public Text levelNoTextHeaderTxt;
    public Text levelNoInResultTxt;
    public Text quickWordsFound;
    public int screenNo;
    public Sprite[] footerActiveSprites;
    public Sprite[] footerInactiveSprites;
    public GameObject footerHomeBtnIcon;
    public GameObject footerShopBtnIcon;
    public GameObject awesomeBox;
    public GameObject quitPanel;

    public Sprite[] plantsInResultPanel;
    public Image awesomeImage;
    public Image levelUpImage;
    public Image homePlantImage;
    public Image introPopPupPlantImage;

    public GameObject topicShopPopUp;
    public Button shopButton;

    // Start is called before the first frame update
    public enum mainPanel
    {
        IntroPanel, homePanel, topicListPanel, sentencePanel
    }
    public mainPanel panelName;
    public enum GameMode
    {
        breakingnewsMode,
        publishSentenceMode,
        quickWordMode,
        sentenceMode

    }
    public GameMode gameMode;

    public enum GameType
    {
        tutorial,
        gameStart

    }
    public GameType gameType;
    private static guiManager instance;
    public static guiManager Instance
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
        //panelName=mainPanel;
        footerHomeBtnIcon.GetComponent<Image>().sprite=footerActiveSprites[0];
        
    }
    // Update is called once per frame
    void Update()
    {
        if(!quitPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape)){
            quitPanel.SetActive(true);
        }else if(quitPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape)){
             quitPanel.SetActive(false);
        }
    }
    public void goToHomePanel()
    {
        panelName = mainPanel.homePanel;
    }
    public void goToTopicListPanel(string mode)
    {
        gameModeText = mode;
        homePanel.SetActive(false);
           if (mode == "publish-mode")
        {
            gameMode = GameMode.publishSentenceMode;
            tutorialManager.Instance.levelNoTemp=5;
            quickWordHeader.SetActive(false);
            quickWordNextButton.SetActive(false);
            biasedButton.SetActive(true);
            notBiasedButton.SetActive(true);
            sentenceHeader.SetActive(true);
            QuestionsManager.Instance.totalQuestions=9;
        }
        if (mode == "sentence-mode")
        {
            gameMode = GameMode.sentenceMode;
            tutorialManager.Instance.levelNoTemp=3;
            quickWordHeader.SetActive(false);
            quickWordNextButton.SetActive(false);
            biasedButton.SetActive(true);
            notBiasedButton.SetActive(true);
            sentenceHeader.SetActive(true);
            QuestionsManager.Instance.totalQuestions=9;
        }
        else if (mode == "quickwords-mode")
        {
            gameMode = GameMode.quickWordMode;
            tutorialManager.Instance.levelNoTemp=4;
            //gameModePanel.SetActive(true);
            //topicListPanel.SetActive(false);
            //QuestionsManager.Instance.questionNo=0;
            //QuestionsManager.Instance.waitForNextQuestion();
            biasedButton.SetActive(false);
            notBiasedButton.SetActive(false);
            sentenceHeader.SetActive(false);
            quickWordHeader.SetActive(true);
            quickWordNextButton.SetActive(true);
            QuestionsManager.Instance.gameCard.GetComponent<swipeLogic>().enabled=false;
            if (gameType == GameType.tutorial){
                 QuestionsManager.Instance.totalQuestions=9;
            }else{
                 QuestionsManager.Instance.totalQuestions=29;
            }
           
        }
        else if (mode == "breakingnews-mode")
        {
            gameMode = GameMode.breakingnewsMode;
            quickWordHeader.SetActive(false);
            quickWordNextButton.SetActive(false);
            biasedButton.SetActive(true);
            notBiasedButton.SetActive(true);
            sentenceHeader.SetActive(true);
            QuestionsManager.Instance.totalQuestions=9;
        }
        if (gameType == GameType.tutorial)
        {
            //guiManager.instance.goToSentencePanel();
            gameModePanel.SetActive(true);
            tutorialManager.Instance.tutorialPanel.SetActive(true);
            if (tutorialManager.Instance.tutorialLevelNo == 1)
            {
                level_1_Setting();
                tutorialManager.Instance.tutorialPopUps[1].SetActive(true);
                guiManager.Instance.levelNoInResultTxt.text=tutorialManager.Instance.tutorialLevelNo.ToString();
            }
            else if (tutorialManager.Instance.tutorialLevelNo == 2)
            {
                tutorialManager.Instance.tutorialPopUps[2].SetActive(true);
                guiManager.Instance.levelNoInResultTxt.text=tutorialManager.Instance.tutorialLevelNo.ToString();
            }
            else if (tutorialManager.Instance.levelNoTemp == 3)
            {
                tutorialManager.Instance.tutorialPopUps[3].SetActive(true);
                guiManager.Instance.levelNoInResultTxt.text=tutorialManager.Instance.tutorialLevelNo.ToString();
            }
            else if (tutorialManager.Instance.levelNoTemp == 4)
            {
                Time.timeScale=0;
                tutorialManager.Instance.tutorialPopUps[4].SetActive(true);
                guiManager.Instance.levelNoInResultTxt.text=tutorialManager.Instance.tutorialLevelNo.ToString();
            }
            else if (tutorialManager.Instance.levelNoTemp == 5)
            {
                tutorialManager.Instance.tutorialPopUps[5].SetActive(true);
                guiManager.Instance.levelNoInResultTxt.text=tutorialManager.Instance.tutorialLevelNo.ToString();
            }
            uiUpdated.Instance.levelStartingNo += QuestionsManager.Instance.questionNo;

            //QuestionsManager.Instance.waitForNextQuestion();
        }
        else
        {
            if (mode == "quickwords-mode")
            {
                databaseManager.instance.wordsAnswer.Clear();
                if (databaseManager.instance.wordsAnswer.Count == 0)
                {
                    //databaseManager.instance.wordsAnswer.Add("0");//new
                }
                StartCoroutine(databaseManager.instance.getWordSentences());
                goToSentencePanel();
                //gameModePanel.SetActive(true);
            }
            topicListPanel.SetActive(true);
        }

        QuestionsManager.Instance.questionNo = 0;
        QuestionsManager.Instance.gameInsideSlider.value = 0;
     
        QuestionsManager.Instance.assignTutorialQuestions();
        
    }
    public void goToSentencePanel()
    {
        Time.timeScale=1;
        gameModePanel.SetActive(true);
        //topicListPanel.SetActive(false);
        uiUpdated.Instance.levelStartingNo += QuestionsManager.Instance.questionNo;
        //QuestionsManager.Instance.questionNo=uiUpdated.Instance.levelStartingNo;
        QuestionsManager.Instance.waitForNextQuestion();
        QuestionsManager.Instance.questionNo=0;
        //oneWordManager.Instance.showSentences();
        sentenceResultPanel.SetActive(false);
    }
    public void hideObject(GameObject hideObject)
    {
        hideObject.SetActive(false);
    }
    public void showObject(GameObject showObject)
    {
        showObject.SetActive(true);
    }
    public void showStaricPanel()
    {
        mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        mainCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        sentenceResultPanel.SetActive(true);
        uiUpdated.Instance.showBiasedIconsInResult();
        staricPanel.SetActive(true);
        uiUpdated.Instance.topicNameText.GetComponent<Text>().fontSize = 25;
        uiUpdated.Instance.topicNameBottomLineText.GetComponent<Text>().fontSize = 25;
        uiUpdated.Instance.topicNameQuickWordsText.GetComponent<Text>().fontSize = 25;
        quickWordTimer.Instance.showRemainingTimer.GetComponent<Text>().fontSize = 20;
        if(uiUpdated.Instance.rightAnsCounter>=7){
            awesomeBox.SetActive(true);
        }else{
            awesomeBox.SetActive(false);
        }
        if (gameMode == GameMode.publishSentenceMode || gameMode == GameMode.sentenceMode)
        {
            uiUpdated.Instance.staricImage.GetComponent<Image>().sprite = uiUpdated.Instance.allStaricImages[uiUpdated.Instance.rightAnsCounter];
            uiUpdated.Instance.showStaricCounterText.text = uiUpdated.Instance.rightAnsCounter + "/10";
        }
        else
        {
            if (tutorialManager.Instance.tutorialLevelNo == 2 || tutorialManager.Instance.tutorialLevelNo == 1)
            {
                uiUpdated.Instance.staricImage.GetComponent<Image>().sprite = uiUpdated.Instance.allStaricImages[uiUpdated.Instance.rightAnsCounter];
                uiUpdated.Instance.showStaricCounterText.text = uiUpdated.Instance.rightAnsCounter + "/10";
            }
            else
            {
                if(gameMode == GameMode.quickWordMode){
                    uiUpdated.Instance.showFoundWordCounter.SetActive(true);
                uiUpdated.Instance.staricAndCoinsContainer.SetActive(false);
                if(quickWordTimer.Instance.tappedWordCounter<=1){
                    if(QuestionsManager.Instance.questionNo>=QuestionsManager.Instance.totalQuestions){
                        quickWordsFound.text ="You completed all sentences and found"+ " " + quickWordTimer.Instance.tappedWordCounter + " " + "word";
                    }else{
                        quickWordsFound.text = quickWordTimer.Instance.tappedWordCounter + " " + "biased word found";
                    }
                }else{
                    if(QuestionsManager.Instance.questionNo>=QuestionsManager.Instance.totalQuestions){
                        quickWordsFound.text ="You completed all sentences and found"+ " " + quickWordTimer.Instance.tappedWordCounter + " " + "words";
                    }else{
                        quickWordsFound.text = quickWordTimer.Instance.tappedWordCounter + " " + "biased words found";
                    }
                }
                }
            }

        }
        //gameModePanel.SetActive(false);

    }
    public void showResultPanel()
    {
        if (gameMode == GameMode.publishSentenceMode || gameMode == GameMode.sentenceMode)
        {
            attemptedQuestionsAnswers.SetActive(true);
        }
        else if (gameMode == GameMode.breakingnewsMode)
        {
            if (tutorialManager.Instance.tutorialLevelNo == 2)
            {
                attemptedQuestionsAnswers.SetActive(true);
            }
            else
            {
                selectedQuickWords.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 0;
            //quickWordTimer.Instance.timerValue = 1;
            selectedQuickWords.SetActive(true);
            staricPanel.SetActive(false);
        }
        staricPanel.SetActive(false);
    }
    public void showLevelComplete()
    {
        levelCompletePanel.SetActive(true);
        gameModePanel.SetActive(false);
    }
    public void collectEarnings()
    {
        //homePanel.SetActive(true);
        if(gameType==GameType.tutorial){
            levelCompletePanel.SetActive(true);
            sentenceResultPanel.SetActive(false);
            attemptedQuestionsAnswers.SetActive(false);
            gameModePanel.SetActive(false);
        }else{
            loadScene();
        }
        
    }


    public void loadScene()
    {
        Time.timeScale = 1;
        tutorialManager.Instance.tutorialLevelUnlock();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void showLockedPanel()
    {
        lockedPanel.SetActive(true);
        Invoke("hideLockedPanel", 1.5f);
    }
    void hideLockedPanel()
    {
        lockedPanel.SetActive(false);
    }
    public void level_1_Setting(){
        for(int i=1;i<oneWordManager.Instance.parentRows.Length;i++){
            oneWordManager.Instance.parentRows[i].SetActive(false);
        }
        QuestionsManager.Instance.gameCard.GetComponent<VerticalLayoutGroup>().childAlignment=TextAnchor.MiddleCenter;
        QuestionsManager.Instance.gameCard.GetComponent<VerticalLayoutGroup>().padding.top=0;
        QuestionsManager.Instance.gameCard.GetComponent<Image>().sprite=oneWordManager.Instance.oneWordBgSprite;
        QuestionsManager.Instance.gameCard.GetComponent<Image>().preserveAspect=true;
        oneWordManager.Instance.parentRows[0].GetComponent<HorizontalLayoutGroup>().padding.top=0;
    }
    public void assignLvlNoInResult(){

    }
    public void footerHomeFun(){
        footerHomeBtnIcon.GetComponent<Image>().sprite=footerActiveSprites[0];
        footerShopBtnIcon.GetComponent<Image>().sprite=footerInactiveSprites[1];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void footerShopFun(){
        
        footerHomeBtnIcon.GetComponent<Image>().sprite=footerInactiveSprites[0];
        footerShopBtnIcon.GetComponent<Image>().sprite=footerActiveSprites[1];
        inAppEarning.Instance.showAvailableTopicsToBuy();
        
        
    }
    public void quitApp(){
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
        Debug.Log("App quit");
        //quitPanel.SetActive(false);
    }
   
}
