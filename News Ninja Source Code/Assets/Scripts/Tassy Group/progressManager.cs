using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressManager : MonoBehaviour
{
    public GameObject questionPanel;
    public GameObject topicCompletePanel;
    public GameObject resultPanel;
    public GameObject[] resultQuestionsContainer;
    //public Text[] resultQuestionsTitle;
    public GameObject showNextButton;
    public GameObject unlockNextLevelButton;
    public GameObject congratulationPanel;
    public Text[] resultQuestionsText;
    public Text topicCompleteTitleText;
    public GameObject topicCompletStars;
    public Sprite[] starSprites;
    public Text topicRightAnswersText;
    public Text moneyText;
    public Text moneyTextHome;
    public int moneyCounter;
    public ParticleSystem topicPassedParticle;
    public Canvas mainCanvas;
    private static progressManager instance;
    public static progressManager Instance
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
        moneyCounter=PlayerPrefs.GetInt("moneyCounter",moneyCounter);
        moneyTextHome.text=moneyCounter.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    public void showResultPanel(){
        questionPanel.SetActive(false);
        resultPanel.SetActive(true);
        topicCompletePanel.SetActive(false);
         PlayerPrefs.SetInt("moneyCounter",progressManager.Instance.moneyCounter);
    }
    public void unlockNewLevel(){
       // guiManager.Instance.screenNo=2;
       // guiManager.Instance.goToTopicListPanel();
        resultPanel.SetActive(false);
        questionPanel.SetActive(true);
        for(int i=0;i<resultQuestionsContainer.Length;i++){
            resultQuestionsContainer[i].transform.GetChild(1).gameObject.SetActive(false);
            resultQuestionsContainer[i].transform.GetChild(2).gameObject.SetActive(false);
        }
        mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        
    }
    public void showCongratulationScreen(){
        congratulationPanel.SetActive(true);
    }
}
