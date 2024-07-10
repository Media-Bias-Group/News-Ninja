using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject[] insideUIPanels;
    public GameObject[] mainUIPanels;
    public GameObject headerPanel;
    public GameObject topicCompletePanel;
    //Screen object variables


    public enum mainState { mainPanel, topicListPanel, sentencePanel, topicCompletePanel, publishedSentencePanel, PS_homePanel, RS_homePanel };
    public mainState currentState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    void Start()
    {
        currentState = mainState.mainPanel;
    }
    void Update()
    {
        if (mainUIPanels[0].activeSelf)
        {
            headerPanel.SetActive(false);
        }
        else
        {
            headerPanel.SetActive(true);
        }

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
    public void publish_Btn()
    {
        currentState = mainState.topicListPanel;
    }
    public void published_Btn()
    {
        currentState = mainState.publishedSentencePanel;
    }
    public void topicSelect_Btn()
    {
        currentState = mainState.sentencePanel;
    }
    public void topicSel_reviewBtn()
    {
        currentState = mainState.PS_homePanel;
    }
    public void rewardAfterLevelDone()
    {
        SwippingManager.Instance.hardCashNumb += 100;
        //topicCompletedScreen.SetActive(false);
    }
}