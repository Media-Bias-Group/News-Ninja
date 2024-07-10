using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public Text hardCashText;
    public Slider healthBarSlider;
    private static ProfileManager instance;
    public static ProfileManager Instance
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
    public int hardCash;
    public int playerLevel;
    public float playerProgress;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
