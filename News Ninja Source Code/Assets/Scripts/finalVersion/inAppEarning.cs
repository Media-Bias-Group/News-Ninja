using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inAppEarning : MonoBehaviour
{
    public int earnDollar;
    public float earnSkills;
    public int earnXP;

    public Text earnDollarText;
    public Text earnSkillText;
    public Text earnXPText;
    public Text earnDollarSentenceModeText;
    public Text headerCashText;
    public Slider homeGlobalSkillsSlider;

    public GameObject refillBox;
    public Text refillBoxText;
    public GameObject refillBuyBtn;
    public GameObject refillBackBtn;
    public GameObject earningContainer;

    public GameObject buyTopicBoxPrefab;
    public GameObject buyTopicParent;
    public GameObject[] spawnBuyTopicBoxes;
    

    private static inAppEarning instance;
    public static inAppEarning Instance
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
        earnDollarSentenceModeText.text="";
        //earnDollar=25000;
        earnDollar=PlayerPrefs.GetInt("saveDoller", earnDollar);
        PlayerPrefs.SetInt("saveDoller", earnDollar);
        headerCashText.text=earnDollar.ToString();
        for(int i=0;i<databaseManager.instance.refillTopic.Length;i++){
            databaseManager.instance.refillTopic[i]=PlayerPrefs.GetString("saveTopicRefill"+i,"no");
            PlayerPrefs.SetString("saveTopicRefill"+i,databaseManager.instance.refillTopic[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void showEarning()
    {
        if(guiManager.Instance.gameType == guiManager.GameType.gameStart){
            if (guiManager.Instance.gameMode == guiManager.GameMode.publishSentenceMode)
        {
            earnDollar += 10;
            // earnSkills += 0.8f;
            // earnXP += 80;
            earningContainer.SetActive(true);
            earnDollarText.text = "+10$";
            earnSkillText.text = databaseManager.instance.skill[QuestionsManager.Instance.questionNo] + " " + "Skill";
            earnXPText.text = databaseManager.instance.XP[QuestionsManager.Instance.questionNo] + " " +  "XP";
            

        }
        else if (guiManager.Instance.gameMode == guiManager.GameMode.sentenceMode)
        {
            earnDollar += 10;
            earnDollarSentenceModeText.text = "+10$";
        }
        //soundManager.Instance.starCollectSound();
        }

    }
    public void hideEarning()
    {
        earnDollarSentenceModeText.text="";
        earningContainer.SetActive(false);
    }
    public void showRefillBox(){
        refillBox.SetActive(true);
        if(PlayerPrefs.GetInt("saveDoller")>=20){
            refillBoxText.text="Refill this topic for 20$!";
        }else{
            refillBoxText.text="You need 20$ to refill this topic.";
            refillBuyBtn.SetActive(false);
        }
    }
    public void refillBackBtnFun(){
        refillBox.SetActive(false);
    }
    public void refillBuyBtnFun(){
        for(int i=0;i<databaseManager.instance.refillTopic.Length;i++){
            databaseManager.instance.refillTopic[databaseManager.instance.refillTopicNo]="no";
            PlayerPrefs.SetString("saveTopicRefill"+i,databaseManager.instance.refillTopic[i]);
            PlayerPrefs.Save();
        }
        earnDollar -= 20;
        headerCashText.text=inAppEarning.Instance.earnDollar.ToString();
        PlayerPrefs.SetInt("saveDoller", inAppEarning.Instance.earnDollar);
        refillBackBtnFun();
    }
    public void showAvailableTopicsToBuy()
    {
        //spawnBuyTopicBoxes = new GameObject[databaseManager.instance.Topics.Length - topicManager.Instance.unlockTopicNo-1];
        spawnBuyTopicBoxes = new GameObject[databaseManager.instance.Topics.Length];
        for (int i = 0; i < spawnBuyTopicBoxes.Length; i++)
        {
         //   if(topicManager.Instance.unlockedTopicsList[i]=="No" || topicManager.Instance.unlockedTopicsList[i]=="Already Unlocked")
         //   {
                spawnBuyTopicBoxes[i] = Instantiate(buyTopicBoxPrefab, transform.position, Quaternion.identity);
                spawnBuyTopicBoxes[i].transform.SetParent(buyTopicParent.transform, false);
                topicBoxPrefabScript.Instance.topicNo = i;
                //spawnBuyTopicBoxes[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = databaseManager.instance.Topics[i+topicManager.Instance.unlockTopicNo+1].ToUpper();
                spawnBuyTopicBoxes[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = databaseManager.instance.Topics[i].ToUpper();
                if(i<=4){
                    spawnBuyTopicBoxes[i].SetActive(false);
                }else{
                    if(topicManager.Instance.unlockedTopicsList[i]=="No"){
                        spawnBuyTopicBoxes[i].SetActive(true);
                    }else{
                        spawnBuyTopicBoxes[i].SetActive(false);
                    }
                    
                }
          //  }
        }
    }
}
