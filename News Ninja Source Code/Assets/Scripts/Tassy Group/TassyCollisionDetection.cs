using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TassyCollisionDetection : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "rightCollider")
        {
            print("OnTriggerEnter: Right ");
            annotationManager.Instance.isTrigger=true;
        }
        else if (collision.gameObject.name == "downCollider")
        {
            print("OnTriggerEnter: Down ");  
        }
        else if (collision.gameObject.name == "leftCollider")
        {
            print("OnTriggerEnter: Left");
        }

    }
}
