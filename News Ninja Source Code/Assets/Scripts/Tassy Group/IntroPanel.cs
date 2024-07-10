using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPanel : MonoBehaviour
{
    public GameObject[] infoPanel;
    public int infoCounter;
    public Slider introSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void introNextBtn(){
        for(int i=0; i<infoPanel.Length;i++){
            infoPanel[i].SetActive(false);
        }
        infoCounter+=1;
        infoPanel[infoCounter].SetActive(true);
        introSlider.value+=1;
    }
    public void introBackBtn(){
         for(int i=0; i<infoPanel.Length;i++){
            infoPanel[i].SetActive(false);
        }
        infoCounter-=1;
        infoPanel[infoCounter].SetActive(true);
        introSlider.value-=1;
    }
    public void selectedRightOption(){
        guiManager.Instance.goToHomePanel();
    }
    public void selectedWrongOption(){
        infoCounter+=1;
        infoPanel[infoCounter].SetActive(true);
    }
}
