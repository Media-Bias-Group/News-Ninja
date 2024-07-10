using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class topicBoxPrefabScript : MonoBehaviour
{
    public int topicNo;
    private static topicBoxPrefabScript instance;
    public static topicBoxPrefabScript Instance
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
        //gameObject.transform.GetChild(0).GetComponent<Button>().interactable=true;

    }
    public void openSentencePanel()
    {

        Debug.Log("Clicked on Topic No: " + topicNo);
        databaseManager.instance.TopicNo = topicNo.ToString();
        databaseManager.instance.refillTopicNo=topicNo;
        //guiManager.Instance.goToSentencePanel();
        StartCoroutine(databaseManager.instance.getSentencePackage(databaseManager.instance.Topics[topicNo]));
        Invoke("loadSentenceInvoke", 0.5f);
    }
    public void loadSentenceInvoke()
    {
        //uiUpdated.Instance.breakingNewsStart();
        guiManager.Instance.goToSentencePanel();
        QuestionsManager.Instance.questionNo=0;
        databaseManager.instance.getWords(databaseManager.instance.Topics[topicNo]);
        StartCoroutine(databaseManager.instance.getWords(databaseManager.instance.indicesOnly[QuestionsManager.Instance.questionNo]));
    }
    public void showRefillTopicBox(){
        databaseManager.instance.refillTopicNo=topicNo;
        inAppEarning.Instance.showRefillBox();
    }
    public void showBuyBox(){
        guiManager.Instance.topicShopPopUp.SetActive(true);
        topicManager.Instance.topicPopUpMessage(inAppEarning.Instance.spawnBuyTopicBoxes[topicNo].transform.GetChild (0).transform.GetChild(0).GetComponent<Text>().text);
        topicManager.Instance.selectedTopicToBuy=topicNo;  
    }
}
