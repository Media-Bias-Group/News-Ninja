using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class oneWordManager : MonoBehaviour
{
    public Button prefab;
    public string[] splitText;

    Button spawnObj;
    public GameObject[] parentRows;
    public int rowNo;
    public Sprite selectedWordBackground;
    public List<string> selectedWords;
    GameObject mainCanvas;
    public float oneWordDeviceWidth;
    public Sprite wordCardRight;
    public Sprite wordCardWrong;
    public Sprite oneWordBgSprite;
    public string[] oneWordLevel5Answers;
    public string[] oneWordLvel1Answers;

    public GameObject oneWordResultParent;
    public GameObject oneWordResultPrefab;
    public GameObject spawnResultBox;
    RectTransform gameCardBg;
    public databaseManager DBManager;
    [System.Serializable]
    public struct Words
    {
        [SerializeField] public string[] word;
    }
    public Words[] Level5Answers;
    public Words[] Level3Answers;

    public string[] wrongBiasedPublishWords;
    private static oneWordManager instance;
    public static oneWordManager Instance
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
    public string[] biasedWords;
    void Start()
    {

        rowNo = 1;
        mainCanvas = guiManager.Instance.mainCanvas;
        if (Screen.width <= 480)
        {
            oneWordDeviceWidth = Screen.width - 150;
            for (int i = 0; i < parentRows.Length; i++)
            {
                parentRows[i].transform.GetComponent<HorizontalLayoutGroup>().padding.top = 5;
                parentRows[i].transform.parent.GetComponent<VerticalLayoutGroup>().padding.left = 10;
                parentRows[i].transform.parent.GetComponent<VerticalLayoutGroup>().padding.top = 0;
            }
        }
        else if (Screen.width <= 720)
        {
            oneWordDeviceWidth = Screen.width - 250;
        }
        else if (Screen.width <= 1080)
        {
            oneWordDeviceWidth = Screen.width - 410;
        }
        else if (Screen.width <= 1440)
        {
            oneWordDeviceWidth = Screen.width - 600;
        }
        gameCardBg = QuestionsManager.Instance.gameCard.GetComponent<RectTransform>();
        // Debug.Log("OffsetMin:" + gameCardBg.sizeDelta);
    }

    // Update is called once per frame
    void Update()
    {
        if (parentRows[0].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 2;
        }
        if (parentRows[1].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 3;
        }
        if (parentRows[2].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 4;
        }
        if (parentRows[3].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 5;
        }
        if (parentRows[4].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 6;
        }
        if (parentRows[5].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 7;
        }
        if (parentRows[6].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 8;
        }
        if (parentRows[7].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 9;
        }
        if (parentRows[8].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 10;
        }
        if (parentRows[9].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 11;
        }
        if (parentRows[10].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 12;
        }
        if (parentRows[11].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 13;
        }
        if (parentRows[12].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 14;
        }
        if (parentRows[13].GetComponent<RectTransform>().rect.width > oneWordDeviceWidth)
        {
            rowNo = 15;
        }
        if (tutorialManager.Instance.tutorialLevelNo == 1 && parentRows[0].transform.childCount == 2)
        {
            oneWordManager.Instance.parentRows[0].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().fontSize = 80;
        }
    }

    public void showSentences()
    {
        emptyParentRows();
        mainCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        uiUpdated.Instance.topicNameText.GetComponent<Text>().fontSize = 35;
        uiUpdated.Instance.topicNameBottomLineText.GetComponent<Text>().fontSize = 35;
        uiUpdated.Instance.topicNameQuickWordsText.GetComponent<Text>().fontSize = 35;
        quickWordTimer.Instance.showRemainingTimer.GetComponent<Text>().fontSize = 25;
        if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
        {
            Invoke("DoCheckPublic", 0.3f);
            // if(guiManager.Instance.gameModeText == "quickwords-mode"){
            //     Invoke("DoCheckPublic", 0.3f);
            // }
            // else
            // {
            //     //StartCoroutine("DoCheck");
            // }
        }
        else
        {
            //StartCoroutine("DoCheck");
            
            Invoke("DoCheckPublic", 0.3f);
            
        }
        //StartCoroutine("DoCheck");
    }
    public void DoCheckPublic()
    {
        //DoCheck();
        
        StartCoroutine("DoCheck");

    }
    IEnumerator DoCheck()
    {
        if (QuestionsManager.Instance.questionNo < 30)
        {
            emptyParentRows();
            if (guiManager.Instance.gameType == guiManager.GameType.tutorial)
            {
                splitText = QuestionsManager.Instance.tutorialQuestions[QuestionsManager.Instance.questionNo].Split();
                if(guiManager.Instance.gameMode != guiManager.GameMode.quickWordMode){
                    uiUpdated.Instance.attemptedQuestions[QuestionsManager.Instance.questionNo].text = QuestionsManager.Instance.tutorialQuestions[QuestionsManager.Instance.questionNo];
                }
                QuestionsManager.Instance.assignWordsToDBScript();
            }
            else
            {
              if (guiManager.Instance.gameMode != guiManager.GameMode.quickWordMode)
            {
                uiUpdated.Instance.attemptedQuestions[QuestionsManager.Instance.questionNo].text =
                    databaseManager.instance.sentencesOnly[QuestionsManager.Instance.questionNo];
            }
            if (QuestionsManager.Instance.questionNo == 0 && guiManager.Instance.gameMode == guiManager.GameMode.quickWordMode)
            {
                yield return new WaitForSeconds(0.7f);
            }
            splitText = databaseManager.instance.sentencesOnly[
                QuestionsManager.Instance.questionNo
            ].Split();
            if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
            {
                StartCoroutine(
                    databaseManager.instance.getWords(
                        databaseManager.instance.indicesOnly[QuestionsManager.Instance.questionNo]
                    )
                ); //new
            }
            }
            for (int i = 0; i < splitText.Length; i++)
            {
                yield return new WaitForSeconds(0.01f);
                spawnObj = Instantiate(prefab, transform.position, transform.rotation);
                spawnObj.transform.SetParent(parentRows[rowNo - 1].transform, true);
                spawnObj.GetComponentInChildren<Text>().text = splitText[i] + " ";
                spawnObj.name = wordCleaner(splitText[i]);
            }

        }
        selectedWords.Clear();
        if(guiManager.Instance.gameMode == guiManager.GameMode.quickWordMode){
            Invoke("annotateNextButtonActive",.3f);
        }

    }
    //Quick Word mode annotate button active
    public void annotateNextButtonActive(){
        guiManager.Instance.quickWordNextButton.GetComponent<Button>().interactable=true;
    }
    public void annotateNextButtonDeActive(){
        guiManager.Instance.quickWordNextButton.GetComponent<Button>().interactable=false;
    }
    public GameObject[] childText;
    public void emptyParentRows()
    {
        rowNo = 1;
        childText = GameObject.FindGameObjectsWithTag("oneWordPrefab");
        for (int i = 0; i < childText.Length; i++)
        {
            Destroy(childText[i]);
        }
    }

    public void SpawnOneWordResult()
    {
        spawnResultBox = Instantiate(oneWordResultPrefab, transform.position, transform.rotation);
        spawnResultBox.transform.parent = oneWordResultParent.transform;
    }
    
    public string wordCleaner(string word)
    {
        string pattern = @"([\w\-]+)";
        var match = Regex.Match(word, pattern);
        return match.Groups[1].Value;
    }
}
