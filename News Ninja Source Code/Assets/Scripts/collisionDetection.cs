using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "rightCollider")
        {
            if (Input.GetMouseButtonUp(0))
            {
                print("Button Up");
                swipeLogic.Instance.setIntialPos();
                QuestionsManager.Instance.annotationBtn("Biased");
                //prototypeManager.Instance.biasedBtn("biased");
                QuestionsManager.Instance.annotationBtnTapped = true;
            }else{
                 //print("ButtonDown");
            }

        }
        else if (collision.gameObject.name == "downCollider")
        {
            print("OnTriggerEnter: Down ");
        }
        else if (collision.gameObject.name == "leftCollider")
        {
            if (Input.GetMouseButtonUp(0))
            {
                print("ButtonUp");
                swipeLogic.Instance.setIntialPos();
                QuestionsManager.Instance.annotationBtn("Non-biased");
                //prototypeManager.Instance.biasedBtn("factual");
                QuestionsManager.Instance.annotationBtnTapped = true;
            }
            else{
                //kprint("ButtonDown");
            }

        }
    }


}