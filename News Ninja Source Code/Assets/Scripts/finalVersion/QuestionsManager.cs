using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestionsManager : MonoBehaviour
{
    public Text questionText;
    public int questionNo;
    public int totalQuestions;
    public GameObject gameCard;

    public int rightAnsCounter;
    public Slider gameInsideSlider;
    public int wholeScreenTouchCount = 0;
    public bool ansRight;
    public bool nextBtnClicked;
    public string selectedSentenceAnswer;
    public string[] tutorialQuestions;
    public string[] tutorialSentenceAnswers;
    public string[] tutorialWordsAnswers;

    [Header("Tutorial Level 1")]
    public string[] intenseWords;
    public string[] intenseWordsAnswer;

    [Header("Tutorial Level 2")]
    public string[] spinSentences;
    public string[] spinSentencesAnswers;
    public string[] spinWordAnswers;
    public string[][] spinWordAnswer_L2 = new string[10][]{
				new string[1]{"refuse"},
                new string[1]{"recently"},
                new string[1]{"gloated"},
                new string[1]{"serious"},
                new string[2]{"rape","murder"},
                new string[3]{"stubborn","refusal","admit"},
                new string[1]{"according"},
                new string[1]{"dodged"},
                new string[2]{"famously","corrupted"},
                new string[4]{"silly","frivolous","stupidly","indulge"}
		};
    
    //publish Mode Sentences (Sentence + Words)
    [Header("Tutorial Level 5")]
    [Header("publish Mode Questions (Senntences + Words)")]
    public string[] publishModeQuestions; //Every level has 10 questions
    public string[] publishModeAnswers; //publish Mode Answers (sentences biased or not biased)
    public string[] publisModeSingleWordAnswers;

     public string[][] publishWordAnswers_L5 = new string[10][]{
				new string[1]{"Shockingly"},
                new string[2]{"remarkable","demonize"},
                new string[1]{"slammed"},
                new string[0]{},
                new string[3]{"firing","warning","shot"},
                new string[1]{"gawk"},
                new string[2]{"continue","rage"},
                new string[0]{},
                new string[5]{"Trumpism","spread","infect","deadly","cancer"},
                new string[0]{},
		};

    //Sentences for Quick Word Mode (Select Words only)
    [Header("Tutorial Level 4")]
    [Header("Sentences for Quick Word Mode (Select Words only)")]
    public string[] quickWordsSentences;
    public string[] quickWordsAnswers;
    public string[][] quickWordsAnswers_L4 = new string[10][]{
				new string[2]{"seemingly","unhinged"},
                new string[0]{},
                new string[2]{"tragically","could"},
                new string[3]{"apparently","hostage","terrifying"},
                new string[0]{},
                new string[3]{"well-deserved","dangerous","suggests"},
                new string[1]{"suggests"},
                new string[4]{"pretty","obvious","suffer","squeeze"},
                new string[0]{},
                new string[4]{"explosion","illegal","aliens","occupy"},
		};


    //Sentence Mode Questions (only sentences)
    [Header("Tutorial Level 3")]
    [Header("Sentence Mode Questions (only sentences)")]
    public string[] sentenceModeQuestions; //Every level has 10 questions
    public string[] sentenceModeAnswers; //Sentence Mode Level 1 Answers (sentences biased or not biased)

    public string annotationBtnState;
    public bool annotationBtnTapped;

    private static QuestionsManager instance;
    public static QuestionsManager Instance
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
        nextBtnClicked = false;
        annotationBtnTapped = false;
        uiUpdated.Instance.headerCoinsText.text = inAppEarning.Instance.earnDollar.ToString(); 
          
        
    }

    public void assignWordsToDBScript(){
        if(guiManager.Instance.gameType == guiManager.GameType.tutorial){
            databaseManager.instance.wordsAnswer.Clear();
            if(guiManager.Instance.gameMode == guiManager.GameMode.breakingnewsMode && tutorialManager.Instance.tutorialLevelNo==2){
            {
                for(int i=0;i<spinWordAnswer_L2[QuestionsManager.instance.questionNo].Length;i++){
            databaseManager.instance.wordsAnswer.Add(spinWordAnswer_L2[QuestionsManager.instance.questionNo][i]);
                }
            }
            }else if(guiManager.Instance.gameMode == guiManager.GameMode.quickWordMode){
                for(int i=0;i<quickWordsAnswers_L4[QuestionsManager.instance.questionNo].Length;i++){
            databaseManager.instance.wordsAnswer.Add(quickWordsAnswers_L4[QuestionsManager.instance.questionNo][i]);
        } 
            }
            else if(guiManager.Instance.gameMode == guiManager.GameMode.publishSentenceMode){
                for(int i=0;i<publishWordAnswers_L5[QuestionsManager.instance.questionNo].Length;i++){
            databaseManager.instance.wordsAnswer.Add(publishWordAnswers_L5[QuestionsManager.instance.questionNo][i]);
        } 
            }
        }
        }      
    
    // Update is called once per frame
    void Update()
    {
        //questionText.text = publishModeQuestions[uiUpdated.Instance.levelStartingNo+questionNo].ToString();
        
    }
    public void quickWordNextButton()
    {       
       databaseManager.instance.insertNonSelectedWords();
        Invoke("waitForNextQuestion", .5f);
        oneWordManager.Instance.annotateNextButtonDeActive();
    }
    public void annotationBtn(string functionName)
    {

        oneWordManager.Instance.rowNo = 1;
        if (!annotationBtnTapped)
        {
            if (questionNo <= totalQuestions)
            {
                gameInsideSlider.value += 1;
                wholeScreenTouchCount++;
                if (functionName == "Biased")
                {
                    if (guiManager.Instance.gameType == guiManager.GameType.tutorial)
                    {
                        selectedSentenceAnswer = tutorialSentenceAnswers[questionNo];
                    }
                    else
                    {
                        selectedSentenceAnswer = databaseManager.instance.resultsOnly[questionNo];
                    }
                    if (selectedSentenceAnswer == "Biased")
                    {
                        //correct anwwer here
                        annotationBtnState = "biased";
                        ansRight = true;
                        guiManager.Instance.biasedButton.GetComponent<Image>().sprite = guiManager.Instance.biasedBtnSprites[1];
                        inAppEarning.Instance.earnDollar += 20;
                        // uiUpdated.Instance.rightAnsCounter += 1;
                        //rightAnsCounter += databaseManager.instance.star(0.5);
                        //if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
                         uiUpdated.Instance.rightAnsCounter+=databaseManager.instance.star(0.5);
                        soundManager.Instance.correctAnsSound();
                        //guiManager.Instance.publishModeWholeScreenBtn.SetActive(true);
                        uiUpdated.Instance.biasedTickMark.SetActive(true);
                        FeedbackBeforeNextBtn();
                        if (guiManager.Instance.gameMode != guiManager.GameMode.sentenceMode)
                        {
                            Invoke("FeedbackBeforeNextBtn", 2f);
                        }
                        else
                        {
                            Invoke("FeedbackBeforeNextBtn", 1f);
                        }

                        if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
                        {
                            StartCoroutine(databaseManager.instance.submitSentenceAnswer(databaseManager.instance.indicesOnly[questionNo], 1, 1,databaseManager.instance.skill[questionNo]));
                            //Invoke("FeedbackWithNextButton", 1.5f);
                        }
                        else if (guiManager.Instance.gameType == guiManager.GameType.tutorial){
                            
                         
                            StartCoroutine(databaseManager.instance.submitTutorial(tutorialManager.Instance.tutorialLevelNo-1+""+questionNo,1));
                        }
                        if (tutorialManager.Instance.tutorialLevelNo == 1)
                        {
                            oneWordManager.Instance.SpawnOneWordResult();
                            oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                            oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = tutorialQuestions[questionNo];
                            quickWordTimer.Instance.tappedWordCounter += 1;
                        }
                    }
                    else
                    {
                        //wrong answer here
                        // gameCard.GetComponent<Image>().color = new Color32(217,57,50,255);
                        ansRight = false;
                        uiUpdated.Instance.notBiasedCrossMark.SetActive(true);
                        soundManager.Instance.wrongAnsSound();
                        //guiManager.Instance.publishModeWholeScreenBtn.SetActive(true);
                        FeedbackBeforeNextBtn();
                        if (guiManager.Instance.gameMode != guiManager.GameMode.sentenceMode)
                        {
                            Invoke("FeedbackBeforeNextBtn", 2f);
                        }
                        else
                        {
                            Invoke("FeedbackBeforeNextBtn", 1f);
                        }
                        //rightAnsCounter += databaseManager.instance.star(0);
                        //if(guiManager.Instance.gameType == guiManager.GameType.gameStart)
                         uiUpdated.Instance.rightAnsCounter += databaseManager.instance.star(0);
                        guiManager.Instance.biasedButton.GetComponent<Image>().sprite = guiManager.Instance.biasedBtnSprites[2];
                        if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
                        {
                            StartCoroutine(databaseManager.instance.submitSentenceAnswer(databaseManager.instance.indicesOnly[questionNo], 1, 0,databaseManager.instance.skill[questionNo]));
                            //Invoke("waitForNextQuestion", 1.5f);
                        }
                        else if (guiManager.Instance.gameType == guiManager.GameType.tutorial){
                            
                            StartCoroutine(databaseManager.instance.submitTutorial(tutorialManager.Instance.tutorialLevelNo-1+""+questionNo,0));
                        }
                        if (tutorialManager.Instance.tutorialLevelNo == 1)
                        {
                            oneWordManager.Instance.SpawnOneWordResult();
                            oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
                            oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = tutorialQuestions[questionNo];
                        }
                    }
                }
                else if (functionName == "Non-biased")
                {
                    if (guiManager.Instance.gameType == guiManager.GameType.tutorial)
                    {
                        //selectedSentenceAnswer = tutorialSentenceAnswers[questionNo];
                        selectedSentenceAnswer = tutorialSentenceAnswers[questionNo];//new
                    }
                    else
                    {
                        //selectedSentenceAnswer = databaseManager.instance.resultsOnly[questionNo];
                        selectedSentenceAnswer = databaseManager.instance.resultsOnly[questionNo];//new
                    }
                    if (selectedSentenceAnswer == "Non-biased")
                    {
                        //correct answer here
                        // gameCard.GetComponent<Image>().color = new Color32(137,226,25,255);
                        ansRight = true;
                        //uiUpdated.Instance.correctAnswerSign.SetActive(true);
                        uiUpdated.Instance.notBiasedCrossMark.SetActive(true);
                        guiManager.Instance.notBiasedButton.GetComponent<Image>().sprite = guiManager.Instance.notBiasedBtnSprites[1];
                        inAppEarning.Instance.earnDollar += 20;
                        // uiUpdated.Instance.rightAnsCounter += 1;
                        //if(guiManager.Instance.gameType == guiManager.GameType.gameStart)
                         uiUpdated.Instance.rightAnsCounter += databaseManager.instance.star(0.5);
                        //databaseManager.instance.star(0.5);
                        soundManager.Instance.correctAnsSound();
                        //guiManager.Instance.publishModeWholeScreenBtn.SetActive(true);
                        FeedbackBeforeNextBtn();
                        if (guiManager.Instance.gameMode != guiManager.GameMode.sentenceMode)
                        {
                            Invoke("FeedbackBeforeNextBtn", 2f);
                        }
                        else
                        {
                            Invoke("FeedbackBeforeNextBtn", 1f);
                        }
                        if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
                        {
                            StartCoroutine(databaseManager.instance.submitSentenceAnswer(databaseManager.instance.indicesOnly[questionNo], 0, 1,databaseManager.instance.skill[questionNo]));
                            // Invoke("FeedbackWithNextButton", 1.5f);
                        }
                        else if (guiManager.Instance.gameType == guiManager.GameType.tutorial){
                            
                            StartCoroutine(databaseManager.instance.submitTutorial(tutorialManager.Instance.tutorialLevelNo-1+""+questionNo,1));
                        }
                        if (tutorialManager.Instance.tutorialLevelNo == 1)
                        {
                            oneWordManager.Instance.SpawnOneWordResult();
                            oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                            oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = tutorialQuestions[questionNo];
                        }

                    }
                    else
                    {
                        //worng answer here
                        // gameCard.GetComponent<Image>().color = new Color32(217,57,50,255);
                        ansRight = false;
                        uiUpdated.Instance.biasedTickMark.SetActive(true);
                        soundManager.Instance.wrongAnsSound();
                        //guiManager.Instance.publishModeWholeScreenBtn.SetActive(true);
                        FeedbackBeforeNextBtn();
                        if (guiManager.Instance.gameMode != guiManager.GameMode.sentenceMode)
                        {
                            Invoke("FeedbackBeforeNextBtn", 2f);
                        }
                        else
                        {
                            Invoke("FeedbackBeforeNextBtn", 1f);
                        }
                        //if(guiManager.Instance.gameType == guiManager.GameType.gameStart)
                         uiUpdated.Instance.rightAnsCounter += databaseManager.instance.star(0);
                        // databaseManager.instance.star(0);
                        guiManager.Instance.notBiasedButton.GetComponent<Image>().sprite = guiManager.Instance.notBiasedBtnSprites[2];
                        if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
                        {
                            StartCoroutine(databaseManager.instance.submitSentenceAnswer(databaseManager.instance.indicesOnly[questionNo], 0, 0,databaseManager.instance.skill[questionNo]));
                            //Invoke("waitForNextQuestion", 1.5f);
                        }
                        else if (guiManager.Instance.gameType == guiManager.GameType.tutorial){
                            
                            StartCoroutine(databaseManager.instance.submitTutorial(tutorialManager.Instance.tutorialLevelNo-1+""+questionNo,0));
                        }
                        if (tutorialManager.Instance.tutorialLevelNo == 1)
                        {
                            oneWordManager.Instance.SpawnOneWordResult();
                            oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
                            oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = tutorialQuestions[questionNo];
                        }
                    }
                }
                if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
                {
                    StartCoroutine(databaseManager.instance.submitSentenceProgress(databaseManager.instance.indicesOnly[questionNo]));
                }
            }
            uiUpdated.Instance.headerCoinsText.text = inAppEarning.Instance.earnDollar.ToString();
            annotationBtnTapped = true;
            if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
            {
                databaseManager.instance.compareWords(databaseManager.instance.wordsAnswer, oneWordManager.Instance.selectedWords);
            }else if (tutorialManager.Instance.tutorialLevelNo==5){
                databaseManager.instance.compareWordsTutorial(databaseManager.instance.wordsAnswer, oneWordManager.Instance.selectedWords);
            }
        }
    }
    public void waitForNextQuestion()
    {

        oneWordManager.Instance.emptyParentRows();
        oneWordManager.Instance.rowNo = 1;
        annotationBtnTapped = false;
        nextBtnClicked = false;
        uiUpdated.Instance.biasedTickMark.SetActive(false);
        uiUpdated.Instance.notBiasedCrossMark.SetActive(false);
        guiManager.Instance.biasedButton.GetComponent<Image>().sprite = guiManager.Instance.biasedBtnSprites[0];
        guiManager.Instance.notBiasedButton.GetComponent<Image>().sprite = guiManager.Instance.notBiasedBtnSprites[0];
        if (questionNo < totalQuestions)
        {
            if (guiManager.Instance.gameModeText == "publish-mode")
            {
                oneWordManager.Instance.showSentences();
            }
            else if (guiManager.Instance.gameModeText == "sentence-mode")
            {
                oneWordManager.Instance.showSentences();
            }
            else if (guiManager.Instance.gameModeText == "quickwords-mode")
            {
                oneWordManager.Instance.showSentences();
            }
            else if (guiManager.Instance.gameModeText == "breakingnews-mode")
            {
                oneWordManager.Instance.showSentences();
            }
        }
        else if (questionNo >= totalQuestions)
        {
            guiManager.Instance.showStaricPanel();
            guiManager.Instance.whiteFeedBackPanel.SetActive(false);
            guiManager.Instance.biasedButton.SetActive(false);
            guiManager.Instance.notBiasedButton.SetActive(false);
            StartCoroutine(databaseManager.instance.submitTopicDailyProgress(databaseManager.instance.TopicNo));
            StartCoroutine(databaseManager.instance.updateXPValue(databaseManager.instance.globalXP +databaseManager.instance.XP.Sum()));
            StartCoroutine(databaseManager.instance.calculateGlobalSkill());
            //tutorialManager.Instance.tutorialLevelUnlock();
            guiManager.Instance.levelNoInResultTxt.text=(tutorialManager.Instance.tutorialLevelNo+1).ToString();
            topicManager.Instance.topicCompleteFun();
        }
        questionNo += 1;
        // if (questionNo != 1)
        // {
        //     if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
        //     {
        //         StartCoroutine(databaseManager.instance.getWords(databaseManager.instance.indicesOnly[questionNo]));//new
        //     }
        // }
        gameCard.GetComponent<Image>().color = Color.white;
        //inAppEarning.Instance.hideEarning();
        guiManager.Instance.whiteFeedBackPanel.SetActive(false);
        if (guiManager.Instance.gameMode != guiManager.GameMode.quickWordMode)
        {
            guiManager.Instance.biasedButton.SetActive(true);
            guiManager.Instance.notBiasedButton.SetActive(true);
        }

        guiManager.Instance.sentenceModeMotivationMessage.SetActive(false);
        PlayerPrefs.SetInt("saveDoller", inAppEarning.Instance.earnDollar);
    }
    public void FeedbackWithNextButton()
    {
        nextBtnClicked = true;
        gameCard.GetComponent<Image>().color = Color.white;
        guiManager.Instance.whiteFeedBackPanel.SetActive(true);
        guiManager.Instance.biasedButton.SetActive(false);
        guiManager.Instance.notBiasedButton.SetActive(false);
        if (guiManager.Instance.gameMode == guiManager.GameMode.sentenceMode)
        {
            guiManager.Instance.sentenceModeMotivationMessage.SetActive(true);
        }
    }
    public void FeedbackNextBtnClick()
    {

        //oneWordManager.Instance.emptyParentRows();
        if (nextBtnClicked)
        {
            inAppEarning.Instance.showEarning();
            //Invoke("waitForNextQuestion", 1.0f);
            waitForNextQuestion();
            Invoke("hideEarning", 1.5f);
        }
        nextBtnClicked = false;

    }
    public void hideEarning(){
        inAppEarning.Instance.hideEarning();
    }

    public void FeedbackBeforeNextBtn()
    {

        if (wholeScreenTouchCount == 1)
        {
            if (ansRight)
            {
                gameCard.GetComponent<Image>().color = new Color32(137, 226, 25, 255);//green color
                //uiUpdated.Instance.biasedTickMark.SetActive(true);
            }
            else
            {
                gameCard.GetComponent<Image>().color = new Color32(217, 57, 50, 255);//red color
            }
            wholeScreenTouchCount++;
        }
        else if (wholeScreenTouchCount == 2)
        {
            Invoke("FeedbackWithNextButton", .1f);
            if (ansRight)
            {
                //Invoke("FeedbackWithNextButton", .1f);
            }
            else
            {
                //Invoke("waitForNextQuestion", .1f);
            }
            //guiManager.Instance.publishModeWholeScreenBtn.SetActive(false);
            wholeScreenTouchCount = 0;
        }
        //
    }
    public void assignTutorialQuestions()
    {
        if (guiManager.Instance.gameType == guiManager.GameType.tutorial)
        {
            for (int i = 0; i < tutorialQuestions.Length; i++)
            {
                if (guiManager.Instance.gameMode == guiManager.GameMode.breakingnewsMode)
                {
                    if (tutorialManager.Instance.tutorialLevelNo == 1)
                    {
                        tutorialQuestions[i] = intenseWords[i];
                        tutorialSentenceAnswers[i] = intenseWordsAnswer[i];
                    }
                    else
                    {
                        tutorialQuestions[i] = spinSentences[i];
                        tutorialSentenceAnswers[i] = spinSentencesAnswers[i];

                        //databaseManager.instance.wordsAnswer[i]=spinWordAnswers[i];

                    }
                }
                else if (guiManager.Instance.gameMode == guiManager.GameMode.sentenceMode)
                {
                    tutorialQuestions[i] = sentenceModeQuestions[i];
                    tutorialSentenceAnswers[i] = sentenceModeAnswers[i];
                }
                else if (guiManager.Instance.gameMode == guiManager.GameMode.quickWordMode)
                {
                    tutorialQuestions[i] = quickWordsSentences[i];
                    // tutorialSentenceAnswers[i]=quickWordsAnswers[i];
                }
                else if (guiManager.Instance.gameMode == guiManager.GameMode.publishSentenceMode)
                {
                    tutorialQuestions[i] = publishModeQuestions[i];
                    tutorialSentenceAnswers[i] = publishModeAnswers[i];
                }
            }
            //Assigning Word based Answers to DBManger for Tutorial mode only

        }

    }
}
