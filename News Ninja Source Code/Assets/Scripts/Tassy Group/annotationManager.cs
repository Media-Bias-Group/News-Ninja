using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class annotationManager : MonoBehaviour
{
    //public Text senteceTxt;
    public int sentenceCounter;
    public GameObject Level1Intro_1;
    public GameObject Level1Intro_2;
    public GameObject Level2Intro_1;
    public GameObject infoPopuoLevel1;
    public Slider progressSlider;
    public GameObject[] destroyObjects;
    public int rightAnswers;
    public GameObject biasedRightBtnIcon;
    public GameObject biasedWrongBtnIcon;
    public GameObject factualRightBtnIcon;
    public GameObject factualWronBtnIcon;
    public GameObject sentenceContainer;
    public Sprite[] feedbackSprites;
    public Vector3 cardInitialPos;
    public bool isTrigger;
    public bool isTapped;
    private static annotationManager instance;
    public static annotationManager Instance
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
        cardInitialPos=gameObject.transform.position;
        isTrigger=false;

    }

    // Update is called once per frame
    void Update()
    {
        if (sentenceCounter > 9)
        {
            //sentenceCounter = 0;
        }
        destroyObjects = GameObject.FindGameObjectsWithTag("oneWordPrefab");
         if (Input.GetMouseButton(0) && isTapped)
        {
            gameObject.transform.position = Input.mousePosition;
            //cardInitialPos=Input.mousePosition;
        }else if(!isTapped){
             gameObject.transform.position=cardInitialPos;

        }
    }
    public void destroyPreviousSentence()
    {
        if (destroyObjects != null)
        {
            for (int i = 0; i < destroyObjects.Length; i++)
            {
                Destroy(destroyObjects[i]);
                mananger.Instance.rowNo = 1;
            }
        }
    }
    public void annotaionButton(string functionName)
    {
        Invoke("destroyPreviousSentence", 1.5f);


        progressSlider.value += 1;

        if (sentenceCounter <= 9)
        {

            if (functionName == "biased")
            {
                if (mananger.Instance.topic1Answers[sentenceCounter] == "biased")
                {
                    Debug.Log("Your Answers is correct");
                    rightAnswers += 1;
                    progressManager.Instance.resultQuestionsContainer[sentenceCounter].transform.GetChild(1).gameObject.SetActive(true);
                    biasedRightBtnIcon.SetActive(true);
                    sentenceContainer.GetComponent<Image>().sprite = feedbackSprites[1];
                    progressManager.Instance.moneyCounter += 20;
                    progressManager.Instance.moneyText.text = progressManager.Instance.moneyCounter.ToString();
                }
                else
                {
                    Debug.Log("Your Answers is Wrong");
                    progressManager.Instance.resultQuestionsContainer[sentenceCounter].transform.GetChild(2).gameObject.SetActive(true);
                    biasedWrongBtnIcon.SetActive(true);
                    sentenceContainer.GetComponent<Image>().sprite = feedbackSprites[2];
                }
            }
            else if (functionName == "factual")
            {
                if (mananger.Instance.topic1Answers[sentenceCounter] == "factual")
                {
                    Debug.Log("Your Answers is correct");
                    rightAnswers += 1;
                    progressManager.Instance.resultQuestionsContainer[sentenceCounter].transform.GetChild(1).gameObject.SetActive(true);
                    factualRightBtnIcon.SetActive(true);
                    sentenceContainer.GetComponent<Image>().sprite = feedbackSprites[1];
                    progressManager.Instance.moneyCounter += 20;
                    progressManager.Instance.moneyText.text = progressManager.Instance.moneyCounter.ToString();
                }
                else
                {
                    Debug.Log("Your Answers is Wrong");
                    progressManager.Instance.resultQuestionsContainer[sentenceCounter].transform.GetChild(2).gameObject.SetActive(true);
                    factualWronBtnIcon.SetActive(true);
                    sentenceContainer.GetComponent<Image>().sprite = feedbackSprites[2];
                }

            }
            Invoke("waitUntilHideFeedback", 1.5f);

        }

    }
    public void waitUntilHideFeedback()
    {
        destroyPreviousSentence();
        sentenceCounter += 1;
        mananger.Instance.showSentences();
        if (sentenceCounter > 9)
        {
            sentenceCounter = 0;
            progressManager.Instance.topicCompletePanel.SetActive(true);
            if (rightAnswers >= 6)
            {
                progressManager.Instance.mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                progressManager.Instance.topicPassedParticle.Play();
            }
            progressManager.Instance.topicCompletStars.GetComponent<Image>().sprite = progressManager.Instance.starSprites[rightAnswers - 1];
            progressManager.Instance.topicRightAnswersText.text = rightAnswers + "/10";
        }
        biasedRightBtnIcon.SetActive(false);
        biasedWrongBtnIcon.SetActive(false);
        factualRightBtnIcon.SetActive(false);
        factualWronBtnIcon.SetActive(false);
        sentenceContainer.GetComponent<Image>().sprite = feedbackSprites[0];

    }
    public void Lvl1okayButton()
    {
        Level1Intro_1.SetActive(false);
        Level1Intro_2.SetActive(true);

    }
    public void Lvl1yesButton()
    {
        Level1Intro_1.SetActive(false);
        Level1Intro_2.SetActive(false);
        infoPopuoLevel1.SetActive(false);
    }
    public void resetTopic()
    {
        progressSlider.value = 0;

    }
    public void mouseDown(){
        isTapped=true;
    }
    public void mouseUp(){
        isTapped=false;
    }
}
