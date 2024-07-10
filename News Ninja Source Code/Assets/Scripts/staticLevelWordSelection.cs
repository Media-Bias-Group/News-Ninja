using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class staticLevelWordSelection : MonoBehaviour
{
    public bool tappedBtn;
    public bool checkClick;
    public bool checkState;
    public float wordScore;
    Vector3 ip;

    GameObject oneWordBtn;
    // Start is called before the first frame update
    private static staticLevelWordSelection instance;
    public static staticLevelWordSelection Instance
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
    void Start()
    {
        ip=gameObject.transform.position;
        tappedBtn = false;
        checkState = true;
        gameObject.transform.name = oneWordManager.Instance.wordCleaner(gameObject.transform.GetChild(0).transform.GetComponent<Text>().text);
         if (Screen.width <= 480)
        {
            gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontSize = 40;
            gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.top = 8;
        }
        else if (Screen.width <= 720)
        {
           gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontSize = 40;
            gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.top = 8;
        }
        else if (Screen.width <= 1080)
        {
            gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontSize = 40;
            gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.top = 13;
        }
        else if (Screen.width <= 1440)
        {
          gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontSize = 40;
            gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.top = 8;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (prototypeManager.Instance.annotationBtnTapped)
        {
            if (prototypeManager.Instance.annotationBtnState == "biased")
            {
                //notSelectBiasedWords();
            }
            if(prototypeManager.Instance.annotationBtnState == "factual")
            {
                wrongWordsSelected();
            }
        }
        // if (checkClick && tappedBtn)
        // {
        //     if(prototypeManager.Instance.levelState!=prototypeManager.LevelState.Level2){
        //             gameObject.transform.position = Input.mousePosition;
        //     } 
        // }
    }
    public void clickOnText()
    {
       
            if (!tappedBtn)
            {
                gameObject.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.selectedWordBackground;
                oneWordManager.Instance.selectedWords.Add(gameObject.transform.GetChild(0).GetComponent<Text>().text);
                gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                givePadding();
                tappedBtn = true;
            }
            else if (tappedBtn)
            {
                gameObject.transform.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                oneWordManager.Instance.selectedWords.Remove(gameObject.transform.GetChild(0).GetComponent<Text>().text);
                gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Normal;
                removePadding();
                gameObject.transform.GetComponent<Image>().sprite = null;
                tappedBtn = false;
            }
        

    }

    public void notSelectBiasedWords()
    {
        if (oneWordManager.Instance.oneWordLevel5Answers.Length > 0)
        {
            for (int i = 0; i < oneWordManager.Instance.oneWordLevel5Answers.Length; i++)
            {
                if (gameObject.transform.name == oneWordManager.Instance.oneWordLevel5Answers[i])
                {
                    if (tappedBtn)
                    {
                        gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardRight;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                        givePadding();
                       // wordScore+=0.3f;
                    }
                    else
                    {
                        gameObject.transform.GetComponent<Image>().color = new Color(50, 50, 50, 255);
                        gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.selectedWordBackground;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                        givePadding();
                    }
                }
            }
        }
        else
        {
            if (tappedBtn)
            {
                gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardWrong;
                gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                givePadding();
            }
        }
    }
    public void wrongWordsSelected()
    {
        if (oneWordManager.Instance.oneWordLevel5Answers.Length > 0)
        {
            for (int i = 0; i < oneWordManager.Instance.oneWordLevel5Answers.Length; i++)
            {
                if (gameObject.transform.name == oneWordManager.Instance.oneWordLevel5Answers[i])
                {
                    if (tappedBtn)
                    {
                        gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardWrong;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                        givePadding();
                       // wordScore+=0.3f;
                    }
                    else
                    {
                        gameObject.transform.GetComponent<Image>().color = new Color(50, 50, 50, 255);
                        gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.selectedWordBackground;
                        gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                        givePadding();
                    }
                }
            }
        }
        else
        {
            if (tappedBtn)
            {
                gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardWrong;
                gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
                givePadding();
            }
        }
    }
    // public void wrongWordsSelected()
    // {

    //     if (oneWordManager.Instance.oneWordLevel5Answers.Length > 0)
    //     {
    //         for (int i = 0; i < oneWordManager.Instance.oneWordLevel5Answers.Length; i++)
    //         {
    //             if (gameObject.transform.GetChild(0).GetComponent<Text>().name == oneWordManager.Instance.oneWordLevel5Answers[i])
    //             {
    //                 if (tappedBtn)
    //                 {
    //                     gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardWrong;
    //                     gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
    //                     givePadding();
    //                 }
    //                 else
    //                 {
    //                     gameObject.transform.GetComponent<Image>().color = new Color(50, 50, 50, 255);
    //                     gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.selectedWordBackground;
    //                     gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
    //                     givePadding();

    //                 }
    //             }
    //         }
    //     }
    //     else
    //     {
    //         if (tappedBtn)
    //         {
    //             gameObject.transform.GetComponent<Image>().sprite = oneWordManager.Instance.wordCardWrong;
    //             gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Bold;
    //             givePadding();
    //         }
    //     }
    // }
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
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.top = 8;
        gameObject.transform.GetComponent<HorizontalLayoutGroup>().padding.bottom = 0;
    }
    public void holdClick()
    {
        checkClick = true;
    }
    public void releaseClick()
    {
        checkClick = false;
    }
    public void invokeResetWord(){
       // Invoke("resetWord",2.0f);
    }
    void OnEnable()
    {
        resetWord();
    }
    public void resetWord()
    {
        
        Debug.Log("One Word");
        gameObject.transform.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        //oneWordManager.Instance.selectedWords.Remove(gameObject.transform.GetChild(0).GetComponent<Text>().text);
        gameObject.transform.GetChild(0).transform.GetComponent<Text>().fontStyle = FontStyle.Normal;
        removePadding();
        gameObject.transform.GetComponent<Image>().sprite = null;
        tappedBtn = false;
    }

}
