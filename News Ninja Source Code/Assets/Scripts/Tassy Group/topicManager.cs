using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class topicManager : MonoBehaviour
{
    public int unlockTopicNo;
    public GameObject[] topicLockBtn;
    public Text firstPopUpMessage;
    public Text popUpMessage;
    public Text secondPopUpMessage;
    public GameObject[] topicShopContainers;
    public Sprite topicBtnYellowBg;
    public GameObject[] topicBoxContainer;
    public int topicNoCounter;

    public GameObject topicBoxPrefab;
    public GameObject topicBoxParent;
    public GameObject[] spawnTopicBox;
    public GameObject  buyBtn;
    //public List<int> topicsForBuying;
    public int selectedTopicToBuy;
    public String[] unlockedTopicsList;
    //GameObject spawnTopicBox;

    private static topicManager instance;
    public static topicManager Instance
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
        unlockTopicNo = 6;
        unlockTopicNo = PlayerPrefs.GetInt("unlockTopic", unlockTopicNo);
        PlayerPrefs.SetInt("unlockTopic", unlockTopicNo);
        disableBoughtTopic();
        topicNoCounter = PlayerPrefs.GetInt("topicComplete", topicNoCounter);
        PlayerPrefs.SetInt("topicComplete", topicNoCounter);
        for (int i = 0; i < PlayerPrefs.GetInt("topicComplete", topicNoCounter); i++)
        {
            topicBoxContainer[i].transform.GetChild(0).GetComponent<Image>().sprite = topicBtnYellowBg;
            topicBoxContainer[i].transform.GetChild(1).gameObject.SetActive(false);
            topicBoxContainer[i].transform.GetChild(3).gameObject.SetActive(true);
        }
        //Invoke("assignValueToTopicBuyList",0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        assignValueToTopicBuyList();
        if(spawnTopicBox.Length>6){
            for(int i=0;i<databaseManager.instance.dailyTopics.Length;i++){
                if(PlayerPrefs.GetString("saveTopicRefill"+Int32.Parse(databaseManager.instance.dailyTopics[i]),databaseManager.instance.refillTopic[Int32.Parse(databaseManager.instance.dailyTopics[i])])=="yes"){
                spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(0).GetComponent<Button>().interactable=false;
                spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(1).gameObject.SetActive(false);
                //spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(2).gameObject.SetActive(true);//attempted questions text
                spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(3).gameObject.SetActive(true);//tick mark with refill button
                spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(4).gameObject.SetActive(true);//refill box
                }
                else if(PlayerPrefs.GetString("saveTopicRefill"+Int32.Parse(databaseManager.instance.dailyTopics[i]),databaseManager.instance.refillTopic[Int32.Parse(databaseManager.instance.dailyTopics[i])])=="no"){
                spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(0).GetComponent<Button>().interactable=true;
                spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(1).gameObject.SetActive(true);
                //spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(2).gameObject.SetActive(true);//attempted questions text
                spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(3).gameObject.SetActive(false);//tick mark with refill button
                spawnTopicBox[Int32.Parse(databaseManager.instance.dailyTopics[i])].transform.GetChild(4).gameObject.SetActive(false);//refill box
                }
            }
        //
    }
    }
    public void unlockTopic()
    {
        // for (int i = 0; i < PlayerPrefs.GetInt("unlockTopic", unlockTopicNo); i++)
        // {
        //     topicLockBtn[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
        //     topicLockBtn[i].transform.GetChild(1).gameObject.SetActive(true);
        //     topicLockBtn[i].transform.GetChild(2).gameObject.SetActive(false);
        //     topicShopContainers[i].SetActive(false);
        // }
    }
    public void buyTopic()
    {
        if (PlayerPrefs.GetInt("saveDoller", inAppEarning.Instance.earnDollar) >= 1000)
        {
            
            inAppEarning.Instance.earnDollar -= 1000;
            inAppEarning.Instance.headerCashText.text=inAppEarning.Instance.earnDollar.ToString();
            PlayerPrefs.SetInt("saveDoller", inAppEarning.Instance.earnDollar);
            unlockTopicNo += 1;
            PlayerPrefs.SetInt("unlockTopic", unlockTopicNo);
            PlayerPrefs.Save();
            for(int i=0;i<unlockedTopicsList.Length;i++){
            unlockedTopicsList[topicManager.Instance.selectedTopicToBuy]="Yes";
            PlayerPrefs.SetString("buyTopic"+ i,unlockedTopicsList[i]);
            PlayerPrefs.Save();
            
        }
        disableBoughtTopic();
        }
        //unlockedTopicsList[6+inAppEarning.Instance.selectedTopicToBuy]="Yes";
        
        firstPopUpMessage.gameObject.SetActive(false);
        secondPopUpMessage.gameObject.SetActive(true);

    }
    void assignValueToTopicBuyList(){
        unlockedTopicsList=new String[databaseManager.instance.Topics.Length];
        for(int i=0;i<unlockedTopicsList.Length;i++){
            if(i>4){
                unlockedTopicsList[i]=PlayerPrefs.GetString("buyTopic"+i,"No");
                PlayerPrefs.SetString("buyTopic"+i,unlockedTopicsList[i]);
            }
            else{
                unlockedTopicsList[i]="Already Unlocked";
            }
        }
    }
    public void topicPopUpMessage(string message)
    {
         if (PlayerPrefs.GetInt("saveDoller", inAppEarning.Instance.earnDollar) < 1000)
        {
            firstPopUpMessage.text = "You don't have enough money to unlock this topic.";
            buyBtn.SetActive(false);
        }else{
            buyBtn.SetActive(true);
            firstPopUpMessage.text = "Do you want to buy Topic" + " " + message + " " + "for 1000$?";
        }
        secondPopUpMessage.text = "Topic" + " " + message + " " + "has been unlocked!";
        secondPopUpMessage.gameObject.SetActive(false);
    }
    public void disableBoughtTopic(){
        // for(int i=0;i<PlayerPrefs.GetInt("unlockTopic");i++){
        //     topicShopContainers[i].SetActive(false);
        // }
        for (int j = 0; j < topicManager.Instance.unlockedTopicsList.Length; j++)
        {
        if(topicManager.Instance.unlockedTopicsList[j]=="Already Unlocked" || topicManager.Instance.unlockedTopicsList[j]=="Yes")
            {
               inAppEarning.Instance.spawnBuyTopicBoxes[j].SetActive(false);
            }
        }  

    }
    public void topicCompleteFun()
    {
        if (topicNoCounter > PlayerPrefs.GetInt("topicComplete", topicNoCounter))
        {
            PlayerPrefs.SetInt("topicComplete", topicNoCounter);
            PlayerPrefs.Save();
        }
        for(int i=0;i<databaseManager.instance.refillTopic.Length;i++){
            databaseManager.instance.refillTopic[databaseManager.instance.refillTopicNo]="yes";
            PlayerPrefs.SetString("saveTopicRefill"+i,databaseManager.instance.refillTopic[i]);
            PlayerPrefs.Save();
        }
    }
    public void assignTopicsName()
    {
        spawnTopicBox = new GameObject[databaseManager.instance.Topics.Length];

        for (int i = 0; i < databaseManager.instance.Topics.Length; i++)
        {
        spawnTopicBox[i] = Instantiate(topicBoxPrefab, transform.position, Quaternion.identity);
        spawnTopicBox[i].transform.SetParent(topicBoxParent.transform, false);
        spawnTopicBox[i].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = databaseManager.instance.Topics[i].ToUpper();
        spawnTopicBox[i].transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = databaseManager.instance.Topics[i].ToUpper();
        topicBoxPrefabScript.Instance.topicNo = i;
        // if (topicBoxPrefabScript.Instance.topicNo > PlayerPrefs.GetInt("unlockTopic", unlockTopicNo))
        // {
        //     spawnTopicBox[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
        //     spawnTopicBox[i].transform.GetChild(1).gameObject.SetActive(false);
        //     spawnTopicBox[i].transform.GetChild(2).gameObject.SetActive(true);
        //}
        }
        for (int j = 0; j < topicManager.Instance.unlockedTopicsList.Length; j++)
        {
        if(topicManager.Instance.unlockedTopicsList[j]=="Already Unlocked" || topicManager.Instance.unlockedTopicsList[j]=="Yes")
            {
                spawnTopicBox[j].transform.GetChild(0).GetComponent<Button>().interactable = true;
                spawnTopicBox[j].transform.GetChild(1).gameObject.SetActive(true);
                spawnTopicBox[j].transform.GetChild(2).gameObject.SetActive(false);
            }else{
                spawnTopicBox[j].transform.GetChild(0).GetComponent<Button>().interactable = false;
                spawnTopicBox[j].transform.GetChild(1).gameObject.SetActive(false);
                spawnTopicBox[j].transform.GetChild(2).gameObject.SetActive(true);
            }
        }  
    }
}
