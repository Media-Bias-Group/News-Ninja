using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class oneWordSelection : MonoBehaviour
{
    public bool tappedBtn;
    public bool checkClick;
    public bool checkState;
    public bool wrongWord;
    public int countClick;
    GameObject oneWordBtn;
    public bool wordFound;
    public int loopIteration;
    // Start is called before the first frame update
    void Awake()
    {

    }
    void Start()
    {
        tappedBtn = false;
        checkState = true;
        wrongWord=false;
        wordFound=false;
        //GetComponent<oneWordSelection>().wordFound=false;
        if (Screen.width <= 480)
        {
            gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontSize = 22;
            gameObject.transform.parent.GetComponent<HorizontalLayoutGroup>().padding.top = 10;
            //oneWordDeviceWidth=180;
        }
        else if (Screen.width <= 720)
        {
            gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontSize = 30;
            gameObject.transform.parent.GetComponent<HorizontalLayoutGroup>().padding.left = 10;
            gameObject.transform.parent.GetComponent<HorizontalLayoutGroup>().padding.right = 10;
            gameObject.transform.parent.GetComponent<HorizontalLayoutGroup>().padding.top = 15;
        }
        else if (Screen.width <= 1080)
        {
            gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontSize = 46;
            // oneWordDeviceWidth=430;
        }
        else if (Screen.width <= 1440)
        {
            gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontSize = 74;
            // oneWordDeviceWidth=600;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (QuestionsManager.Instance.annotationBtnTapped)
        {
            if (guiManager.Instance.gameMode == guiManager.GameMode.quickWordMode || guiManager.Instance.gameMode == guiManager.GameMode.publishSentenceMode || guiManager.Instance.gameMode == guiManager.GameMode.breakingnewsMode)
            {
                Invoke("wordFeedback",2f);
            }
        }
        if (tutorialManager.Instance.tutorialLevelNo==2){
            Invoke("wordFeedback",0f);
        }
        if (countClick == 1 && guiManager.Instance.gameMode == guiManager.GameMode.quickWordMode )
        {
            for (int i = 0; i < databaseManager.instance.wordsAnswer.Count; i++)
            {
                if (gameObject.transform.name == databaseManager.instance.wordsAnswer[i] && !wordFound)
                {
                    
                    Debug.Log("true");
                    quickWordTimer.Instance.addTimerPopUp();
                    quickWordTimer.Instance.tappedWordCounter += 1;
                    gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardRight;
                    oneWordManager.Instance.SpawnOneWordResult();
                    oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                    wordFound=true;
                    if(guiManager.Instance.gameType == guiManager.GameType.tutorial){
                       
                    StartCoroutine(databaseManager.instance.submitTutorialWordAnswer(databaseManager.instance.wordsAnswer[i], 1, 1));  
                    }else{
                    StartCoroutine(databaseManager.instance.submitWordAnswer(databaseManager.instance.wordsIds[i], 1, 1));
                    }
                   
                }
               
                
            }
            if (!wordFound && !wrongWord)
                {
                    quickWordTimer.Instance.minusTimerPopUp();
                    gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardWrong;
                    //if (i <= 1)
                    //{
                        oneWordManager.Instance.SpawnOneWordResult();
                        oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
                  //  }
                    wrongWord=true;
                    //break;
                       if(guiManager.Instance.gameType == guiManager.GameType.tutorial){
                       
StartCoroutine(databaseManager.instance.submitTutorialWordAnswer(wordCleaner(gameObject.transform.GetChild(0).GetComponent<Text>().text), 1, 0));  
                    }else{
                     StartCoroutine(databaseManager.instance.insertWrongAnswer(wordCleaner(gameObject.transform.GetChild(0).GetComponent<Text>().text)));
                    }
                  
                }
            gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
            gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = Color.white;
            oneWordManager.Instance.selectedWords.Add(wordCleaner(gameObject.transform.GetChild(0).GetComponent<Text>().text).ToString());
            oneWordManager.Instance.spawnResultBox.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = wordCleaner(gameObject.transform.GetChild(0).GetComponent<Text>().text).ToString();
            givePadding();
            gameObject.GetComponent<Button>().enabled = false;
            countClick = 2;
        }

    }
   
    public void wordFeedback(){
        if (QuestionsManager.Instance.selectedSentenceAnswer == "Biased" || QuestionsManager.Instance.selectedSentenceAnswer == "Non-biased" || tutorialManager.Instance.tutorialLevelNo==2)
        {
            if (tappedBtn && !wordFound)//Correct Word Selected
            {
                for (int i = 0; i < databaseManager.instance.wordsAnswer.Count; i++)
                {
                    if (gameObject.transform.name == databaseManager.instance.wordsAnswer[i])
                    {
                        gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardRight;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = Color.white;
                        givePadding();
                        wordFound=true;
                    }
                 }
            }
            else if (!tappedBtn && !wordFound)//Correct Word not Selected
            {
                for (int i = 0; i < databaseManager.instance.wordsAnswer.Count; i++)
                {
                    if(gameObject.transform.name == databaseManager.instance.wordsAnswer[i])
                    {
                        gameObject.transform.GetComponent<Image>().color = new Color(50, 50, 50, 255);
                        gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.selectedWordBackground;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = Color.white;
                        givePadding();
                        wordFound=true;
                    }
                }
            }
            if (tappedBtn && !wordFound && !wrongWord)//Wrong Word Selected
            {
                for (int i = 0; i < databaseManager.instance.wordsAnswer.Count; i++)
                {
                    loopIteration++;
                    if (gameObject.transform.name != databaseManager.instance.wordsAnswer[i])
                    {
                        if(loopIteration==databaseManager.instance.wordsAnswer.Count-1){
                        wrongWord=true;
                    }
                        gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardWrong;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = Color.white;
                        givePadding();
                        //wordFound=true;
                    }
                 }
                 if(loopIteration<=0){//Word selected when there is not any biased word in sentence
                        gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardWrong;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = Color.white;
                        givePadding();
                 }
            }
        }
    }
    public void clickOnText()
    {

        if (guiManager.Instance.gameMode != guiManager.GameMode.sentenceMode && guiManager.Instance.gameMode != guiManager.GameMode.breakingnewsMode)
        {
            if (!tappedBtn)
            {
                gameObject.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.selectedWordBackground;
                if (guiManager.Instance.gameMode != guiManager.GameMode.quickWordMode)
                {
                    oneWordManager.Instance.selectedWords.Add(gameObject.transform.GetChild(0).GetComponent<Text>().text);
                }
                gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = Color.white;
                givePadding();
                countClick += 1;
                tappedBtn = true;
            }
            else if (tappedBtn)
            {
                if (guiManager.Instance.gameMode != guiManager.GameMode.quickWordMode)
                {
                    gameObject.transform.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    oneWordManager.Instance.selectedWords.Remove(gameObject.transform.GetChild(0).GetComponent<Text>().text);
                    gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Normal;
                    gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = Color.black;
                    removePadding();
                    gameObject.transform.GetComponent<Image>().sprite = null;
                }
                countClick += 1;
                tappedBtn = false;
            }
        }
    }
    void givePadding()
    {
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.left = 10;
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.right = 10;
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.top = 5;
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.bottom = 5;
    }
    void removePadding()
    {
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.left = 0;
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.right = 0;
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.top = 0;
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.bottom = 0;
    }
    public void holdClick()
    {
        if (guiManager.Instance.gameMode != guiManager.GameMode.sentenceMode && guiManager.Instance.gameMode != guiManager.GameMode.breakingnewsMode)
        {
            checkClick = true;
        }
    }
    public void releaseClick()
    {
        if (guiManager.Instance.gameMode != guiManager.GameMode.sentenceMode && guiManager.Instance.gameMode != guiManager.GameMode.breakingnewsMode)
        {
            checkClick = false;
        }
    }
    public string wordCleaner(string word)
    {
        string pattern = @"([\w\-]+)";
        var match = Regex.Match(word, pattern);
        return match.Groups[1].Value;
    }
}