using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class prototypeManager : MonoBehaviour
{

    public GameObject[] mainUIPanels;
    public GameObject[] insideUIPanels;
    public GameObject[] insideUIChildPanels;

    public GameObject newHeader;

    //Survey answers that will be saved in List
    //List of survey answers
    [Header("Answers of Survey Questions")]
    public string gender = "";
    public string ageFactor = "";
    public string levelOfEducation = "";
    public string englishProficiency = "";
    public string thinkingBehaviour = "";
    public string averageNewsCheck = "";
    public List<int> followedNewsOutlet = new List<int>();
    public Button userAgeButton;
    public GameObject[] newsChannelOptions;

    public InputField userAgeInput;
    public Slider thinkingSlider;
    public Slider surveyHealthBar;


    public Text hardCashInsideLevel;
    public Text hardCashInsideTopicPanel;
    public Text hardCashHomePanel;
    public Text userLevel;
    public Text userLevelTopicPanel;
    public string userMoney;
    public int hardCashNumb;
    public string selectedOutlets;
    string singleSelectURL = "http://165.232.114.94/api/singleSelect";

    string submitTutorialURL = "http://165.232.114.94/api/submitTutorial";
    string level6URL = "http://165.232.114.94/api/getLevel6";
    string level7URL = "http://165.232.114.94/api/getLevel7";

    //Level gameobjects;
    public GameObject[] level1Questions;
    public GameObject[] level2Questions;
    public GameObject[] level3Questions;
    public GameObject[] level4Questions;
    public GameObject[] level5Questions;
    public string[] level6IDs;
    public string[] level6Questions;
    public string[] level6Result;
    public Text level6Question;

    public string[] level7IDs;
    public string[] level7Questions;
    public string[] level7Result;
    public GameObject[] Level1SelectedAnswers;
    public GameObject[] Level2SelectedAnswers;
    public GameObject[] Level3SelectedAnswers;
    public GameObject[] Level4SelectedAnswers;
    public GameObject[] Level5SelectedAnswers;
    public GameObject[] SelectedAnswers;
    public int showQuestionNo;

    public int DBQuestionNo;
    public Slider progressSlider;
    public GameObject questionPanel;
    public GameObject levelCompletePanel;
    public GameObject resultPanel1;
    public GameObject resultPanel2;
    public GameObject resultPanel3;
    public GameObject resultPanel4;
    public GameObject resultPanel5;
    public GameObject resultPanel6;

    //Topic Complete Screen more Objects
    public Text titleOfCompletedTask;
    public GameObject[] starsOfCompletedTask;
    public Text rankOfCompletedTask;
    public int rightAnsCounter;

    public GameObject[] resultPanelNextBtn;
    public GameObject[] resultPanelUnlockBtn;
    public GameObject plantImg;
    public Sprite[] plantSprites;


    public TextMeshProUGUI levelFinished;
    public int unlockInt;
    public GameObject[] lockedImages;

    public Slider[] topicSliders;
    public Text[] topicSliderTexts;
    public GameObject neutralButton;
    public ParticleSystem topicCompleteParticle;
    public GameObject mainCanvas;

    //Inside Game Header Objects
    public Text levelNoText;
    public int levelNo;
    public Text coinText;

    //Topic Instant Feedback Images
    public Sprite[] bigCardFeedbackSprites;
    public Sprite[] smallCardFeedbackSprites;
    public GameObject level1Container;
    public GameObject level2Container;
    public GameObject level3Container;
    public GameObject level4Container;
    public GameObject level5Container;
    public GameObject level6_7Container;
    public GameObject biasedRightIcon;
    public GameObject biasedWrongIcon;
    public GameObject factualRightIcon;
    public GameObject factualWrongIcon;

    public Sprite selectedWordBackground;
    public GameObject[] destroyObjects;
    public double sentenceScore;

    public bool annotationBtnTapped;
    public string annotationBtnState;

    public GameObject[] lastLevelResultButtons;
    public GameObject surveyEndPanel;
    public GameObject alertBtn6;
    public GameObject alertBtn7;
    public GameObject registrationPopUp;

    public enum LevelState
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7
    }
    public LevelState levelState;
    private static prototypeManager instance;
    public static prototypeManager Instance
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
        // unlockInt = 6;

        annotationBtnTapped = false;
        StartCoroutine(CheckInternetConnection(isConnected =>
    {
        if (isConnected)
        {
            // Debug.Log("Internet Available!");
        }
        else
        {
            // Debug.Log("Internet Not Available");
        }
    }));

        // PlayerPrefs.DeleteAll();
        unlockInt = 1;
        unlockInt = PlayerPrefs.GetInt("UnlockLevel", unlockInt);

        for (int i = 0; i < PlayerPrefs.GetInt("UnlockLevel", unlockInt); i++)
        {
            if (i < 7)
            {
                //                lockedImages[i].SetActive(false);
            }

        }
        //StartCoroutine(getMoney("users", "money", "id", firebaseManager.instance.UserId));
        //StartCoroutine(getLevel("users", "level", "id", firebaseManager.instance.UserId));
        StartCoroutine(tutorialQuestions());
        //        hardCashNumb = PlayerPrefs.GetInt("storeMoney", hardCashNumb);
        //        PlayerPrefs.SetInt("storeMoney", hardCashNumb);
        //       hardCashHomePanel.text = hardCashNumb.ToString();
        //        hardCashInsideTopicPanel.text = hardCashNumb.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        destroyObjects = GameObject.FindGameObjectsWithTag("oneWordPrefab");
        //        coinText.text = hardCashNumb.ToString();
        if (unlockInt == 7)
        {
            alertBtn6.SetActive(true);
        }
        else if (unlockInt > 7)
        {
            alertBtn6.SetActive(true);
            alertBtn7.SetActive(true);
        }

        for (int i = 0; i < unlockInt - 1; i++)
        {
            topicSliders[i].value = 10;
            topicSliderTexts[i].text = "10/10";
        }
        if (mainUIPanels[0].activeSelf)
        {
            //            newHeader.SetActive(false);
            //            levelNoText.text = "";
        }
        else
        {
            //           newHeader.SetActive(true);
            // levelNoText.text = levelNo.ToString();
            int number = unlockInt;
            //   userLevelTopicPanel.text = number.ToString();

        }


        if (showQuestionNo > 9)
        {
            if (levelState == LevelState.Level1 || levelState == LevelState.Level2 || levelState == LevelState.Level3 || levelState == LevelState.Level4 || levelState == LevelState.Level5)
            {
                if (rightAnsCounter >= 6)
                {
                    resultPanelUnlockBtn[levelNo - 1].SetActive(true);
                    resultPanelNextBtn[levelNo - 1].SetActive(false);
                }
                else
                {
                    resultPanelUnlockBtn[levelNo - 1].SetActive(false);
                    resultPanelNextBtn[levelNo - 1].SetActive(true);
                }
            }



            if (rightAnsCounter >= 6)
            {
                topicCompleteParticle.Play();
                soundManager.Instance.levelSuccessSound();
                plantImg.GetComponent<Image>().sprite = plantSprites[0];
            }
            else
            {
                plantImg.GetComponent<Image>().sprite = plantSprites[1];
            }

            questionPanel.SetActive(false);
            mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            mainCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            levelCompletePanel.SetActive(true);
            if (levelState == LevelState.Level5)
            {
                Invoke("registration_popup", 1.0f);
            }

            //registration_popup();

            if (rightAnsCounter <= 0)
            {
                starsOfCompletedTask[0].SetActive(true);
            }
            else
            {
                starsOfCompletedTask[rightAnsCounter - 1].SetActive(true);
            }


            switch (levelState)
            {
                case LevelState.Level1:
                    // resultPanel1.SetActive(true);
                    titleOfCompletedTask.text = "\"Intense\" " + "completed";
                    rankOfCompletedTask.text = rightAnsCounter + "/" + "10";

                    break;
                case LevelState.Level2:
                    // resultPanel2.SetActive(true);
                    titleOfCompletedTask.text = "\"You spin me round\" " + "completed";
                    rankOfCompletedTask.text = rightAnsCounter + "/" + "10";
                    break;
                case LevelState.Level3:
                    //resultPanel3.SetActive(true);
                    titleOfCompletedTask.text = "\"Controversial\" " + "completed";
                    rankOfCompletedTask.text = rightAnsCounter + "/" + "10";
                    break;
                case LevelState.Level4:
                    //resultPanel4.SetActive(true);
                    titleOfCompletedTask.text = "\"My opinion is a FACT\" " + "completed";
                    rankOfCompletedTask.text = rightAnsCounter + "/" + "10";
                    break;
                case LevelState.Level5:
                    //resultPanel5.SetActive(true);
                    titleOfCompletedTask.text = "\"That’s sensational!\" " + "completed";
                    rankOfCompletedTask.text = rightAnsCounter + "/" + "10";
                    break;
                case LevelState.Level6:
                    // resultPanel6.SetActive(true);
                    titleOfCompletedTask.text = "\"To battle\" " + "completed";
                    rankOfCompletedTask.text = rightAnsCounter + "/" + "10";

                    break;
                case LevelState.Level7:
                    //resultPanel6.SetActive(true);
                    titleOfCompletedTask.text = "\"Final round\" " + "completed";
                    rankOfCompletedTask.text = rightAnsCounter + "/" + "10";
                    break;
            }
            showQuestionNo = 0;
            biasedRightIcon.SetActive(false);
            biasedWrongIcon.SetActive(false);
            factualRightIcon.SetActive(false);
            factualWrongIcon.SetActive(false);
        }

        // if(levelState==LevelState.Level5){
        //     neutralButton.SetActive(true);
        // }else{
        //     neutralButton.SetActive(false);
        // }
        if (userAgeInput.text != "")
        {
            userAgeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            userAgeButton.GetComponent<Button>().interactable = false;
        }

    }
    public void showPanel(GameObject showObject)
    {
        showObject.SetActive(true);
        soundManager.Instance.simpleBtnSound();
    }
    public void hidePanel(GameObject hideObject)
    {
        hideObject.SetActive(false);

    }
    public void showProgressReport()
    {
        switch (levelState)
        {
            case LevelState.Level1:
                resultPanel1.SetActive(true);
                break;
            case LevelState.Level2:
                resultPanel2.SetActive(true);
                break;
            case LevelState.Level3:
                resultPanel3.SetActive(true);
                break;
            case LevelState.Level4:
                resultPanel4.SetActive(true);
                break;
            case LevelState.Level5:
                resultPanel5.SetActive(true);
                break;
            case LevelState.Level6:
                resultPanel6.SetActive(true);
                break;
            case LevelState.Level7:
                resultPanel6.SetActive(true);
                lastLevelResultButtons[1].SetActive(true);
                lastLevelResultButtons[0].SetActive(false);
                break;
        }
        PlayerPrefs.SetInt("storeMoney", hardCashNumb);
        hardCashHomePanel.text = hardCashNumb.ToString();
        hardCashInsideTopicPanel.text = hardCashNumb.ToString();
    }
    public void showSurveyEndPanel()
    {
        surveyEndPanel.SetActive(true);
    }

    public void main_ScreenChange(GameObject showPanel)
    {
        for (int i = 0; i < mainUIPanels.Length; i++)
        {
            mainUIPanels[i].SetActive(false);
        }
        showPanel.SetActive(true);
    }
    public void inside_ScreenChange(GameObject showPanel)
    {
        for (int i = 0; i < insideUIPanels.Length; i++)
        {
            insideUIPanels[i].SetActive(false);
        }
        showPanel.SetActive(true);
    }
    public void insideChild_ScreenChange(GameObject showPanel)
    {
        for (int i = 0; i < insideUIChildPanels.Length; i++)
        {
            insideUIChildPanels[i].SetActive(false);
        }
        showPanel.SetActive(true);
    }
    //Survey Answers Functions
    public void userGender(string genderName)
    {
        gender = genderName;
        surveyHealthBar.value += 1;
        tutorialManager.Instance.surveyQuestionNo += 1;
    }

    //Survey Age
    public void userAge()
    {

        ageFactor = userAgeInput.text;
        surveyHealthBar.value += 1;
        tutorialManager.Instance.surveyQuestionNo += 1;
    }
    //Survey Level of Education
    public void userEducation(string Education)
    {
        levelOfEducation = Education;
        surveyHealthBar.value += 1;
        tutorialManager.Instance.surveyQuestionNo += 1;
    }
    //Survey level of english proficiency
    public void userEnglishLevel(string englishLevel)
    {
        englishProficiency = englishLevel;
        surveyHealthBar.value += 1;
        tutorialManager.Instance.surveyQuestionNo += 1;
    }
    //Survey Do you consider yourself to be liberal, conservative, or somewhere in bteween?
    public void userThinking()
    {
        if (thinkingSlider.value < 10)
        {
            thinkingBehaviour = "-" + thinkingSlider.value.ToString();
        }
        else
        {
            thinkingBehaviour = thinkingSlider.value.ToString();
        }

        surveyHealthBar.value += 1;
        tutorialManager.Instance.surveyQuestionNo += 1;

    }
    //Survey How often on average do you check the news?
    public void userNewsUpdation(string UserNews)
    {
        averageNewsCheck = UserNews;
        surveyHealthBar.value += 1;
        tutorialManager.Instance.surveyQuestionNo += 1;

    }
    //Survey select news outlets that you follow
    public void usersNewsChannel(int newsChannel)
    {
        if (newsChannelOptions[newsChannel - 1].GetComponent<Image>().color == Color.white)
        {
            followedNewsOutlet.Add(newsChannel);
            newsChannelOptions[newsChannel - 1].GetComponent<Image>().color = Color.gray;
        }
        else if (newsChannelOptions[newsChannel - 1].GetComponent<Image>().color == Color.gray)
        {
            followedNewsOutlet.Remove(newsChannel);
            newsChannelOptions[newsChannel - 1].GetComponent<Image>().color = Color.white;
        }
    }
    public void usersNewsSubmit()
    {
        tutorialManager.Instance.surveyCompleted = 1;
        PlayerPrefs.SetInt("surveyCompleted", tutorialManager.Instance.surveyCompleted);
        PlayerPrefs.Save();
        foreach (int s in followedNewsOutlet)
        {
            selectedOutlets += s + ",";
        }
        selectedOutlets = selectedOutlets.Remove(selectedOutlets.Length - 1, 1);
        surveyHealthBar.value += 1;
        tutorialManager.Instance.surveyQuestionNo += 1;

        StartCoroutine(databaseManager.instance.submitSurvey(gender, ageFactor, levelOfEducation, englishProficiency, thinkingBehaviour, averageNewsCheck, selectedOutlets));

    }
    public IEnumerator getMoney(string tableName, String col, string condition, string conditionValue)
    {



        WWWForm form = new WWWForm();
        form.AddField("tableName", tableName);
        form.AddField("col", col);
        form.AddField("condition", condition);
        form.AddField("conditionValue", conditionValue);

        UnityWebRequest www = UnityWebRequest.Post(singleSelectURL, form);
        yield return www.SendWebRequest();
        userMoney = www.downloadHandler.text;
        //    hardCashNumb = Int32.Parse(userMoney);//commented by sultan
        //    hardCash.text = hardCashNumb.ToString();//commented by sultan


    }
    public IEnumerator getLevel(string tableName, String col, string condition, string conditionValue)
    {

        WWWForm form = new WWWForm();
        form.AddField("tableName", tableName);
        form.AddField("col", col);
        form.AddField("condition", condition);
        form.AddField("conditionValue", conditionValue);

        UnityWebRequest www = UnityWebRequest.Post(singleSelectURL, form);
        yield return www.SendWebRequest();
        string Level = www.downloadHandler.text;
        userLevel.text = Level.ToString();


    }


    public IEnumerator submitTutorial(int DBQuestionNo, string answer)
    {


        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("DBQuestionNo", DBQuestionNo);
        form.AddField("answer", answer);
        UnityWebRequest www = UnityWebRequest.Post(submitTutorialURL, form);
        yield return www.SendWebRequest();
    }
    IEnumerator CheckInternetConnection(Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            // Debug.Log("Error");
            action(false);
        }
        else
        {
            // Debug.Log("Success");
            action(true);
        }
    }
    public IEnumerator tutorialQuestions()
    {


        //getting level 6 questions
        UnityWebRequest www = UnityWebRequest.Get(level6URL);
        yield return www.SendWebRequest();
        string questions = www.downloadHandler.text;
        string[] questionsArray = questions.Split('|');
        questionsArray = questionsArray.Take(questionsArray.Count() - 1).ToArray();
        level6Questions = questionsArray.Where((x, i) => i % 3 == 0).ToArray();
        level6IDs = questionsArray.Where((x, i) => i % 3 == 1).ToArray();
        level6Result = questionsArray.Where((x, i) => i % 3 == 2).ToArray();
        //getting level 7 questions
        www = UnityWebRequest.Get(level7URL);
        yield return www.SendWebRequest();
        questions = www.downloadHandler.text;
        questionsArray = questions.Split('|');
        questionsArray = questionsArray.Take(questionsArray.Count() - 1).ToArray();
        level7Questions = questionsArray.Where((x, i) => i % 3 == 0).ToArray();
        level7IDs = questionsArray.Where((x, i) => i % 3 == 1).ToArray();
        level7Result = questionsArray.Where((x, i) => i % 3 == 2).ToArray();
        // foreach (string q in questionOnly)
        // {
        //     Debug.Log(q);
        // }

        // foreach (string i in indicesOnly)
        // {
        //     Debug.Log(i);
        // }
    }
    public void selectTopic1()
    {

        levelState = LevelState.Level1;
        levelNo = 1;
        level1Questions[0].SetActive(true);
        levelNoText.text = levelNo.ToString();
        levelReset();
        level1Container.GetComponent<Image>().sprite = smallCardFeedbackSprites[0];
        DBQuestionNo = 1;

    }
    public void selectTopic2()
    {
        levelState = LevelState.Level2;
        levelNo = 2;
        level2Questions[0].SetActive(true);
        levelNoText.text = levelNo.ToString();
        levelReset();
        level2Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
        DBQuestionNo = 11;
    }
    public void selectTopic3()
    {
        levelState = LevelState.Level3;
        levelNo = 3;
        level3Questions[0].SetActive(true);
        levelNoText.text = levelNo.ToString();
        levelReset();
        level3Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
        // for (int i = 0; i < topic3FeedbackImages.Length; i++)
        // {
        //     topic3FeedbackImages[i].SetActive(false);
        // }
        DBQuestionNo = 21;

    }
    public void selectTopic4()
    {
        levelState = LevelState.Level4;
        levelNo = 4;
        level4Questions[0].SetActive(true);
        levelNoText.text = levelNo.ToString();
        levelReset();
        level4Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
        DBQuestionNo = 31;
    }
    public void selectTopic5()
    {
        levelState = LevelState.Level5;
        levelNo = 5;
        level5Questions[0].SetActive(true);
        levelNoText.text = levelNo.ToString();
        levelReset();
        level5Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
        DBQuestionNo = 41;
    }
    public void selectTopic6()
    {


        levelState = LevelState.Level6;
        levelNo = 6;
        levelNoText.text = levelNo.ToString();
        levelReset();
        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
        StartCoroutine(databaseManager.instance.getWords(level6IDs[showQuestionNo]));
    }
    public void selectTopic7()
    {
        levelState = LevelState.Level7;
        levelNo = 7;
        levelNoText.text = levelNo.ToString();
        levelReset();
        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
        StartCoroutine(databaseManager.instance.getWords(level7IDs[showQuestionNo]));


    }
    public void levelReset()
    {
        mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        showQuestionNo = 0;
        progressSlider.value = 0;
        rightAnsCounter = 0;
        levelCompletePanel.SetActive(false);
        Invoke("destroyPreviousSentence", 1f);
        for (int i = 0; i < starsOfCompletedTask.Length; i++)
        {
            starsOfCompletedTask[i].SetActive(false);
        }
        resultPanel1.SetActive(false);
        resultPanel2.SetActive(false);
        resultPanel3.SetActive(false);
        resultPanel4.SetActive(false);
        resultPanel5.SetActive(false);
        annotationBtnTapped = false;
    }
    public void unlockNextLevel()
    {

        switch (levelState)
        {
            case LevelState.Level1:
                if (topicSliders[1].value < 10)
                {
                    unlockInt = 2;
                }
                break;
            case LevelState.Level2:
                if (topicSliders[2].value < 10)
                {
                    unlockInt = 3;
                }
                break;
            case LevelState.Level3:
                if (topicSliders[3].value < 10)
                {
                    unlockInt = 4;
                }
                break;
            case LevelState.Level4:
                if (topicSliders[4].value < 10)
                {
                    unlockInt = 5;
                }
                break;
            case LevelState.Level5:
                if (topicSliders[5].value < 10)
                {
                    unlockInt = 6;
                }
                break;
            case LevelState.Level6:
                if (topicSliders[6].value < 10)
                {
                    unlockInt = 7;
                }
                break;
            case LevelState.Level7:
                if (topicSliders[7].value < 10)
                {
                    unlockInt = 8;
                }
                topicSliders[6].value = 10;
                topicSliderTexts[6].text = "10/10";
                break;
        }
        PlayerPrefs.SetInt("UnlockLevel", unlockInt);
        for (int i = 0; i < PlayerPrefs.GetInt("UnlockLevel", unlockInt); i++)
        {
            lockedImages[i].SetActive(false);

        }



    }
    public void destroyPreviousSentence()
    {
        if (destroyObjects != null)
        {
            for (int i = 0; i < destroyObjects.Length; i++)
            {
                Destroy(destroyObjects[i]);
                oneWordManager.Instance.rowNo = 1;
            }
        }
        oneWordManager.Instance.selectedWords.Clear();
    }
    public void biasedBtn(string functionName)
    {
        if (!annotationBtnTapped)
        {
            progressSlider.value += 1;
            if (levelState == LevelState.Level1)
            {

                if (showQuestionNo <= 9)
                {
                    if (functionName == "biased")
                    {
                        annotationBtnState = "biased";
                        if (level1Questions[showQuestionNo].tag == "biased")
                        {
                            Debug.Log("Your answer is correct");
                            rightAnsCounter += 1;
                            hardCashNumb += 20;
                            level1Container.GetComponent<Image>().sprite = smallCardFeedbackSprites[1];
                            biasedRightIcon.SetActive(true);
                            soundManager.Instance.starCollectSound();
                            Level1SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level1SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            Debug.Log("Your answer is Wrong");
                            level1Container.GetComponent<Image>().sprite = smallCardFeedbackSprites[2];
                            biasedWrongIcon.SetActive(true);
                            soundManager.Instance.wrongAnsSound();
                            Level1SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level1SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }
                    else if (functionName == "factual")
                    {
                        annotationBtnState = "factual";
                        if (level1Questions[showQuestionNo].tag == "factual")
                        {
                            Debug.Log("Your answer is correct");
                            rightAnsCounter += 1;
                            hardCashNumb += 20;
                            level1Container.GetComponent<Image>().sprite = smallCardFeedbackSprites[1];
                            factualRightIcon.SetActive(true);
                            soundManager.Instance.starCollectSound();
                            Level1SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level1SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            Debug.Log("Your answer is Wrong");
                            level1Container.GetComponent<Image>().sprite = smallCardFeedbackSprites[2];
                            factualWrongIcon.SetActive(true);
                            soundManager.Instance.wrongAnsSound();
                            Level1SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level1SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }
                    Invoke("waitForNextQuestion", 1);


                }
            }
            else if (levelState == LevelState.Level2)
            {

                if (showQuestionNo <= 9)
                {
                    if (functionName == "biased")
                    {
                        annotationBtnState = "biased";
                        if (level2Questions[showQuestionNo].tag == "biased")
                        {
                            Debug.Log("Your answer is correct");
                            level2Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];
                            biasedRightIcon.SetActive(true);
                            rightAnsCounter += 1;
                            hardCashNumb += 20;
                            soundManager.Instance.starCollectSound();
                            Level2SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level2SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            Debug.Log("Your answer is Wrong");
                            level2Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];
                            biasedWrongIcon.SetActive(true);
                            soundManager.Instance.wrongAnsSound();
                            Level2SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level2SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }
                    else if (functionName == "factual")
                    {
                        annotationBtnState = "factual";
                        if (level2Questions[showQuestionNo].tag == "factual")
                        {
                            Debug.Log("Your answer is correct");
                            level2Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];
                            factualRightIcon.SetActive(true);
                            rightAnsCounter += 1;
                            hardCashNumb += 20;
                            soundManager.Instance.starCollectSound();
                            Level2SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level2SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            Debug.Log("Your answer is Wrong");
                            level2Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];
                            factualWrongIcon.SetActive(true);
                            soundManager.Instance.wrongAnsSound();
                            Level2SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level2SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }
                    Invoke("waitForNextQuestion", 1);

                }

            }
            else if (levelState == LevelState.Level3)
            {
                if (showQuestionNo <= 9)
                {
                    if (functionName == "biased")
                    {
                        annotationBtnState = "biased";
                        if (level3Questions[showQuestionNo].tag == "biased")
                        {
                            Debug.Log("Your answer is correct");
                            level3Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];
                            soundManager.Instance.starCollectSound();
                            biasedRightIcon.SetActive(true);
                            sentenceScore = 0.5;
                            hardCashNumb += 20;
                            Level3SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level3SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            sentenceScore = 0;
                            Debug.Log("Your answer is Wrong");
                            level3Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];
                            biasedWrongIcon.SetActive(true);
                            soundManager.Instance.wrongAnsSound();
                            Level3SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level3SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }
                    else if (functionName == "factual")
                    {
                        annotationBtnState = "factual";
                        if (level3Questions[showQuestionNo].tag == "factual")
                        {
                            Debug.Log("Your answer is correct");
                            level3Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];
                            factualRightIcon.SetActive(true);
                            soundManager.Instance.starCollectSound();
                            sentenceScore = 0.5;
                            hardCashNumb += 20;
                            Level3SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level3SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            sentenceScore = 0;
                            Debug.Log("Your answer is Wrong");
                            level3Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];
                            factualWrongIcon.SetActive(true);
                            soundManager.Instance.wrongAnsSound();
                            Level3SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level3SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }

                    // rightAnsCounter += databaseManager.instance.star(sentenceScore, databaseManager.instance.calculateWordScore(oneWordManager.Instance.Level3Answers[showQuestionNo].word, oneWordManager.Instance.selectedWords));

                    Invoke("waitForNextQuestion", 1);

                }

            }
            else if (levelState == LevelState.Level4)
            {

                if (showQuestionNo <= 9)
                {
                    if (functionName == "biased")
                    {
                        annotationBtnState = "biased";
                        if (level4Questions[showQuestionNo].tag == "biased")
                        {
                            Debug.Log("Your answer is correct");
                            level4Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];
                            biasedRightIcon.SetActive(true);
                            rightAnsCounter += 1;
                            hardCashNumb += 20;
                            soundManager.Instance.starCollectSound();
                            Level4SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level4SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            Debug.Log("Your answer is Wrong");
                            level4Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];
                            biasedWrongIcon.SetActive(true);
                            soundManager.Instance.wrongAnsSound();
                            Level4SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level4SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }
                    else if (functionName == "factual")
                    {
                        annotationBtnState = "factual";
                        if (level4Questions[showQuestionNo].tag == "factual")
                        {
                            Debug.Log("Your answer is correct");
                            level4Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];
                            factualRightIcon.SetActive(true);
                            rightAnsCounter += 1;
                            hardCashNumb += 20;
                            soundManager.Instance.starCollectSound();
                            Level4SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level4SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            Debug.Log("Your answer is Wrong");
                            level4Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];
                            factualWrongIcon.SetActive(true);
                            soundManager.Instance.wrongAnsSound();
                            Level4SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level4SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }
                    Invoke("waitForNextQuestion", 1);

                }

            }
            else if (levelState == LevelState.Level5)
            {
                if (showQuestionNo <= 9)
                {
                    if (functionName == "biased")
                    {
                        annotationBtnState = "biased";
                        if (level5Questions[showQuestionNo].tag == "biased")
                        {
                            Debug.Log("Your answer is correct");
                            level5Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];
                            biasedRightIcon.SetActive(true);
                            soundManager.Instance.starCollectSound();
                            // rightAnsCounter += 1;
                            sentenceScore = 0.5;
                            hardCashNumb += 20;
                            Level5SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level5SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            Debug.Log("Your answer is Wrong");
                            level5Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];
                            biasedWrongIcon.SetActive(true);
                            sentenceScore = 0;
                            soundManager.Instance.wrongAnsSound();
                            Level5SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level5SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }
                    else if (functionName == "factual")
                    {
                        annotationBtnState = "factual";
                        if (level5Questions[showQuestionNo].tag == "factual")
                        {
                            Debug.Log("Your answer is correct");
                            level5Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];
                            factualRightIcon.SetActive(true);
                            soundManager.Instance.starCollectSound();
                            //rightAnsCounter += 1;
                            sentenceScore = 0.5;
                            hardCashNumb += 20;
                            Level5SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                            Level5SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                            StartCoroutine(submitTutorial(DBQuestionNo, "correct"));
                        }
                        else
                        {
                            Debug.Log("Your answer is Wrong");
                            level5Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];
                            factualWrongIcon.SetActive(true);
                            sentenceScore = 0;
                            soundManager.Instance.wrongAnsSound();
                            Level5SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                            Level5SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);
                            StartCoroutine(submitTutorial(DBQuestionNo, "incorrect"));
                        }

                    }

                    // rightAnsCounter += databaseManager.instance.star(sentenceScore, databaseManager.instance.calculateWordScore(oneWordManager.Instance.Level5Answers[showQuestionNo].word, oneWordManager.Instance.selectedWords));
                    Invoke("waitForNextQuestion", 1);


                }


            }
            else if (levelState == LevelState.Level6)
            {
                oneWordManager.Instance.rowNo = 1;
                Debug.Log("Row No:" + oneWordManager.Instance.rowNo);
                Invoke("destroyPreviousSentence", 1.5f);


                if (functionName == "biased")
                {
                    annotationBtnState = "biased";
                    if (level6Result[showQuestionNo] == "Biased")
                    {
                        Debug.Log("Your answer is correct");
                        sentenceScore = 0.5;
                        biasedRightIcon.SetActive(true);
                        hardCashNumb += 20;
                        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];

                        SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                        SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                        soundManager.Instance.starCollectSound();

                    }
                    else
                    {
                        Debug.Log("Your answer is Wrong");
                        biasedWrongIcon.SetActive(true);
                        sentenceScore = 0;
                        soundManager.Instance.wrongAnsSound();
                        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];

                        SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                        SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);

                    }
                    StartCoroutine(databaseManager.instance.updateOrInsert("answers", new string[] { "sentence_id", "user_id" }, "annotation", new System.Object[] { level6IDs[showQuestionNo], SystemInfo.deviceUniqueIdentifier, 1 }));

                }
                else if (functionName == "factual")
                {
                    annotationBtnState = "factual";
                    if (level6Result[showQuestionNo] == "Non-biased")
                    {
                        Debug.Log("Your answer is correct");
                        factualRightIcon.SetActive(true);
                        sentenceScore = 0.5;
                        hardCashNumb += 20;
                        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];

                        SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                        SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                        soundManager.Instance.starCollectSound();

                    }
                    else
                    {
                        sentenceScore = 0;
                        Debug.Log("Your answer is Wrong");
                        factualWrongIcon.SetActive(true);
                        soundManager.Instance.wrongAnsSound();
                        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];

                        SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                        SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);

                    }
                    StartCoroutine(databaseManager.instance.updateOrInsert("answers", new string[] { "sentence_id", "user_id" }, "annotation", new System.Object[] { level6IDs[showQuestionNo], SystemInfo.deviceUniqueIdentifier, -1 }));
                }
                StartCoroutine(databaseManager.instance.increment("sentences", "SentenceCount", "id", level6IDs[showQuestionNo]));
                // rightAnsCounter += databaseManager.instance.star(sentenceScore, databaseManager.instance.calculateWordScore(databaseManager.instance.wordsAnswer, oneWordManager.Instance.selectedWords));
                Invoke("waitForNextQuestion", 1.5f);


            }
            else if (levelState == LevelState.Level7)
            {
                oneWordManager.Instance.rowNo = 1;
                Invoke("destroyPreviousSentence", 1.5f);
                if (functionName == "biased")
                {
                    annotationBtnState = "biased";
                    if (level7Result[showQuestionNo] == "Biased")
                    {
                        Debug.Log("Your answer is correct");
                        biasedRightIcon.SetActive(true);
                        sentenceScore = 0.5;
                        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];

                        SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                        SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                        soundManager.Instance.starCollectSound();

                    }
                    else
                    {
                        sentenceScore = 0;
                        Debug.Log("Your answer is Wrong");
                        biasedWrongIcon.SetActive(true);
                        soundManager.Instance.wrongAnsSound();
                        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];

                        SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                        SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);

                    }
                    StartCoroutine(databaseManager.instance.updateOrInsert("answers", new string[] { "sentence_id", "user_id" }, "annotation", new System.Object[] { level7IDs[showQuestionNo], SystemInfo.deviceUniqueIdentifier, 1 }));
                }
                else if (functionName == "factual")
                {
                    annotationBtnState = "factual";
                    if (level7Result[showQuestionNo] == "Non-biased")
                    {
                        Debug.Log("Your answer is correct");
                        sentenceScore = 0.5;
                        factualRightIcon.SetActive(true);
                        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[1];

                        SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(true);
                        SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(false);
                        soundManager.Instance.starCollectSound();

                    }
                    else
                    {
                        sentenceScore = 0;
                        Debug.Log("Your answer is Wrong");
                        factualWrongIcon.SetActive(true);
                        soundManager.Instance.wrongAnsSound();
                        level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[2];

                        SelectedAnswers[showQuestionNo].transform.GetChild(1).gameObject.SetActive(false);
                        SelectedAnswers[showQuestionNo].transform.GetChild(2).gameObject.SetActive(true);

                    }
                    StartCoroutine(databaseManager.instance.updateOrInsert("answers", new string[] { "sentence_id", "user_id" }, "annotation", new System.Object[] { level7IDs[showQuestionNo], SystemInfo.deviceUniqueIdentifier, -1 }));
                }

                StartCoroutine(databaseManager.instance.increment("sentences", "SentenceCount", "id", level7IDs[showQuestionNo]));
                // rightAnsCounter += databaseManager.instance.star(sentenceScore, databaseManager.instance.calculateWordScore(databaseManager.instance.wordsAnswer, oneWordManager.Instance.selectedWords));
                Invoke("waitForNextQuestion", 1.5f);


            }
            annotationBtnTapped = true;

        }
    }

    public void waitForNextQuestion()
    {
        annotationBtnTapped = false;
        switch (levelState)
        {
            case LevelState.Level1:
                for (int i = 0; i < level1Questions.Length; i++)
                {
                    level1Questions[i].SetActive(false);
                }
                showQuestionNo += 1;
                level1Questions[showQuestionNo].SetActive(true);
                level1Container.GetComponent<Image>().sprite = smallCardFeedbackSprites[0];
                progressSlider.value = showQuestionNo;
                DBQuestionNo++;

                break;
            case LevelState.Level2:
                for (int i = 0; i < level2Questions.Length; i++)
                {
                    level2Questions[i].SetActive(false);
                }
                showQuestionNo += 1;
                level2Questions[showQuestionNo].SetActive(true);
                level2Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
                progressSlider.value = showQuestionNo;
                DBQuestionNo++;
                break;
            case LevelState.Level3:
                for (int i = 0; i < level3Questions.Length; i++)
                {
                    level3Questions[i].SetActive(false);
                }
                showQuestionNo += 1;
                level3Questions[showQuestionNo].SetActive(true);
                level3Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
                progressSlider.value = showQuestionNo;
                DBQuestionNo++;
                break;
            case LevelState.Level4:
                for (int i = 0; i < level4Questions.Length; i++)
                {
                    level4Questions[i].SetActive(false);
                }
                showQuestionNo += 1;
                level4Questions[showQuestionNo].SetActive(true);
                level4Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
                progressSlider.value = showQuestionNo;
                DBQuestionNo++;
                break;
            case LevelState.Level5:
                for (int i = 0; i < level5Questions.Length; i++)
                {
                    level5Questions[i].SetActive(false);
                }
                showQuestionNo += 1;
                level5Questions[showQuestionNo].SetActive(true);
                level5Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
                progressSlider.value = showQuestionNo;

                DBQuestionNo++;
                break;
            case LevelState.Level6:
                showQuestionNo++;
                progressSlider.value = showQuestionNo;
                if (showQuestionNo <= 9)
                {
                    StartCoroutine(databaseManager.instance.getWords(level6IDs[showQuestionNo]));
                    oneWordManager.Instance.showSentences();
                }
                level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
                break;
            case LevelState.Level7:
                showQuestionNo++;
                progressSlider.value = showQuestionNo;
                if (showQuestionNo <= 9)
                {
                    StartCoroutine(databaseManager.instance.getWords(level7IDs[showQuestionNo]));
                    oneWordManager.Instance.showSentences();
                }
                level6_7Container.GetComponent<Image>().sprite = bigCardFeedbackSprites[0];
                //showQuestionNo++;
                break;
        }

        biasedRightIcon.SetActive(false);
        biasedWrongIcon.SetActive(false);
        factualRightIcon.SetActive(false);
        factualWrongIcon.SetActive(false);

    }
    public void goToSurveyWebsite(string url)
    {
        unlockInt = 8;
        PlayerPrefs.SetInt("UnlockLevel", unlockInt);
        Application.OpenURL(url);
    }
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void registration_popup()
    {
        registrationPopUp.SetActive(true);
    }
}