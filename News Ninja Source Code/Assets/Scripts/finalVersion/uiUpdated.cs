using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiUpdated : MonoBehaviour
{
    public Text topicNameText;
    public Text topicNameBottomLineText;
    public Text topicNameQuickWordsText;
    public Text levelNoText;
    public int leveUpdated;
    public int levelStartingNo;
    public GameObject biasedTickMark;
    public GameObject notBiasedCrossMark;
    public Text[] attemptedQuestions;
    public GameObject[] BiasedIconsInResult;
    public Text[] showPercentageInResult;
    public GameObject[] rightAnsSign;
    public GameObject[] wrongAnsSign;
    public Text headerCoinsText;
    public int rightAnsCounter;
    public Image staricImage;
    public Sprite[] allStaricImages;
    public Text showStaricCounterText;
    public GameObject staricAndCoinsContainer;
    public GameObject showFoundWordCounter;


    private static uiUpdated instance;
    public static uiUpdated Instance
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
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void updateTopicName(string topicName)
    {
        topicNameText.text = topicName.ToString();


    }
    public void selectTopic(int topicNO)
    {
        StartCoroutine(databaseManager.instance.getSentencePackage(databaseManager.instance.Topics[topicNO]));
        // Invoke("loadWords", .5f);


    }

    public void levelNoUpdatedFun(int levelNo)
    {
        //leveUpdated=levelNo;
        levelNoText.text = levelNo.ToString();
        topicManager.Instance.topicNoCounter = levelNo;
    }
    public void levelNoStartingFun(int levelStarting)
    {
        levelStartingNo = levelStarting;
    }
    public void swipingCanvas()
    {
        if (tutorialManager.Instance.tutorialLevelNo <= 5){
            guiManager.Instance.mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            //guiManager.Instance.mainCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        }
        
    }
    public void showBiasedIconsInResult()
    {
        if (guiManager.Instance.gameType == guiManager.GameType.tutorial)
        {
            for (int i = 0; i < QuestionsManager.Instance.tutorialSentenceAnswers.Length; i++)
            {
                if (QuestionsManager.Instance.tutorialSentenceAnswers[i] == "Biased")
                {
                    BiasedIconsInResult[i].SetActive(true);
                }
                else
                {
                    BiasedIconsInResult[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < databaseManager.instance.resultsOnly.Length; i++)
            {
                if (databaseManager.instance.resultsOnly[i] == "Biased")
                {
                    BiasedIconsInResult[i].SetActive(true);
                }
                else
                {
                    BiasedIconsInResult[i].SetActive(false);
                }
            }
        }
    }

}
