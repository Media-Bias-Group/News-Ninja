using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mananger : MonoBehaviour
{
    public Button prefab;
    public string[] splitText;
    Button spawnObj;
    public string[] sentenceTextTopic1;
    public string[] topic1Answers;
    public string[] sentenceTextTopic2;
    public string[] topic2Answers;
    public GameObject[] parentRows;
    public int rowNo;
    public Sprite selectedWordBackground;
    private static mananger instance;
    public static mananger Instance
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
        rowNo = 1;

    }

    // Update is called once per frame
    void Update()
    {
        // if (parentRows[0].transform.position.x > 300)
        // {
        //     rowNo = 2;
        // }
        // if (parentRows[1].transform.position.x > 300)
        // {
        //     rowNo = 3;
        // }
        // if (parentRows[2].transform.position.x > 300)
        // {
        //     rowNo = 4;
        // }
        // if (parentRows[3].transform.position.x > 300)
        // {
        //     rowNo = 5;
        // }
        // if (parentRows[4].transform.position.x > 300)
        // {
        //     rowNo = 6;
        // }
        // if (parentRows[5].transform.position.x > 300)
        // {
        //     rowNo = 7;
        // }
        // if (parentRows[6].transform.position.x > 300)
        // {
        //     rowNo = 8;
        // }
        // if (parentRows[7].transform.position.x > 300)
        // {
        //     rowNo = 9;
        // }
        // if (parentRows[8].transform.position.x > 300)
        // {
        //     rowNo = 10;
        // }
        // if (parentRows[9].transform.position.x > 300)
        // {
        //     rowNo = 11;
        // }
    }
    public void showSentences()
    {
        StartCoroutine("DoCheck");
    }
    IEnumerator DoCheck()
    {
        if (annotationManager.Instance.sentenceCounter <= 9)
        {
            for (int i = 0; i < splitText.Length; i++)
            {
                yield return new WaitForSeconds(.01f);
                spawnObj = Instantiate(prefab, transform.position, transform.rotation);
                spawnObj.transform.SetParent(parentRows[rowNo - 1].transform, true);
                spawnObj.GetComponentInChildren<Text>().text = splitText[i] + " ";
            }
        }

    }
}
