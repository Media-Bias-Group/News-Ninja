using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SwippingManager : MonoBehaviour
{
    string singleSelectURL = "http://localhost/game/singleSelect.php";

    // Start is called before the first frame update
    public GameObject card;
    public bool isMouseOver = false;
    public Vector3 cardPos;
    public GameObject[] popSelectedAnsImg;
    public int biasedAnsArea;
    public int unbiasedAnsArea;
    public int neutralAnsArea;

    public Text hardCash;
    public int hardCashNumb;


    public string clickOnAnsBtns;
    //SpriteRenderer sr;
    public Vector3 cardInitialPos;
    public Text questionsTxt;
    public int showNextQuestion;
    public GameObject levelCompletePanel;
    public bool isTrigger;


    public string userMoney;


    private static SwippingManager instance;
    public static SwippingManager Instance
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

        StartCoroutine(getMoney("users", "money", "id", firebaseManager.instance.UserId));
        //showing money value

        isTrigger = false;
        showNextQuestion = 0;
        for (int i = 0; i < popSelectedAnsImg.Length; i++)
        {
            popSelectedAnsImg[i].SetActive(false);
        }

        //   sr = card.GetComponent<SpriteRenderer>();
        cardPos = card.transform.position;
        //Debug.Log(cardPos);
        cardInitialPos = card.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && isMouseOver && !isTrigger)
        {
            //Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            card.transform.position = Input.mousePosition;
            //card.transform.position =pos;
            //            Debug.Log(card.GetComponent<Transform>().position);
        }
        else
        {
            card.transform.position = cardPos;
        }

        //swapping_B_N_UB();

        //Rendering first question
        if (showNextQuestion == 0)
        {
            questionsTxt.text = databaseManager.instance.questionOnly[showNextQuestion];
        }
        //Level Complete
        else if (showNextQuestion == databaseManager.instance.questionOnly.Length - 1)
        {
            levelCompletePanel.SetActive(true);
            firebaseManager.instance.updateCash(hardCashNumb);
        }
    }
    public void swapping_B_N_UB()
    {
        if (card.transform.position.x > biasedAnsArea)//700
        {
            // if (swippingBool)
            //  {
            //  popSelectedAnsImg[0].SetActive(true);
            //ansSelBtnClk("B");
            card.transform.position = cardInitialPos;
            //   swippingBool = false;
            //    }


        }
        else
        {
            // popSelectedAnsImg[0].SetActive(false);

        }

        if (card.transform.position.y < neutralAnsArea || clickOnAnsBtns == "N") //300
        {
            // popSelectedAnsImg[1].SetActive(true);
            // ansSelBtnClk("N");
            card.transform.position = cardInitialPos;
        }
        else
        {
            // popSelectedAnsImg[1].SetActive(false);
        }

        if (card.transform.position.x < unbiasedAnsArea || clickOnAnsBtns == "UB")//-15
        {
            // popSelectedAnsImg[2].SetActive(true);
            // ansSelBtnClk("UB");
            card.transform.position = cardInitialPos;
        }
        else
        {
            // popSelectedAnsImg[2].SetActive(false);
        }


    }
    public void OnMouseOver()
    {
        isMouseOver = true;
    }
    public void OnMouseExit()
    {
        isMouseOver = false;
    }
    //Click on Buttons for the selection of Answer
    public void ansSelBtnClk(string SelectAnswer)
    {
        clickOnAnsBtns = SelectAnswer;
        hardCashNumb += 5;
        hardCash.text = hardCashNumb.ToString();
        ProfileManager.Instance.healthBarSlider.value += 1;
        //  increment sentence count
        StartCoroutine(databaseManager.instance.increment("sentences", "SentenceCount", "id", databaseManager.instance.indicesOnly[showNextQuestion]));
        if (clickOnAnsBtns == "B")
        {
            popSelectedAnsImg[0].SetActive(true);
            StartCoroutine(databaseManager.instance.updateOrInsert("answers", new string[] { "sentence_id", "user_id" }, "annotation", new System.Object[] { databaseManager.instance.indicesOnly[showNextQuestion], firebaseManager.instance.UserId, 1 }));

        }

        else if (clickOnAnsBtns == "N")
        {
            popSelectedAnsImg[1].SetActive(true);
            StartCoroutine(databaseManager.instance.updateOrInsert("answers", new string[] { "sentence_id", "user_id" }, "annotation", new System.Object[] { databaseManager.instance.indicesOnly[showNextQuestion], firebaseManager.instance.UserId, 0 }));

        }

        else if (clickOnAnsBtns == "UB")
        {
            popSelectedAnsImg[2].SetActive(true);
            StartCoroutine(databaseManager.instance.updateOrInsert("answers", new string[] { "sentence_id", "user_id" }, "annotation", new System.Object[] { databaseManager.instance.indicesOnly[showNextQuestion], firebaseManager.instance.UserId, -1 }));

        }
        //calculate Bias of sentences
        StartCoroutine(databaseManager.instance.calculateBias(databaseManager.instance.indicesOnly[showNextQuestion]));

        goToNextQuestion();
    }
    public void goToNextQuestion()
    {
        Invoke("goToNextQuestionInvoke", .4f);

    }
    public void goToNextQuestionInvoke()
    {
        if (showNextQuestion < databaseManager.instance.questionOnly[showNextQuestion].Length - 1)
        {

            showNextQuestion += 1;
            questionsTxt.text = databaseManager.instance.questionOnly[showNextQuestion];
            for (int i = 0; i < popSelectedAnsImg.Length; i++)
            {
                popSelectedAnsImg[i].SetActive(false);

            }
            //SwippingManager.Instance.swippingBool=true;
            //Invoke("letSwippingTrue",1.5f);
        }
    }

    public void highlightText()
    {
        Debug.Log("Text Highlighted");

    }


    public IEnumerator getMoney(string tableName, String col, string condition, string conditionValue)
    {


        // Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("tableName", tableName);
        form.AddField("col", col);
        form.AddField("condition", condition);
        form.AddField("conditionValue", conditionValue);

        UnityWebRequest www = UnityWebRequest.Post(singleSelectURL, form);
        yield return www.SendWebRequest();
        userMoney = www.downloadHandler.text;
        hardCashNumb = Int32.Parse(userMoney);
        hardCash.text = hardCashNumb.ToString();


    }



}