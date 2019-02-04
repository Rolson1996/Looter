using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

    public static AchievementManager instance = null;


    private float TotalMetersRan = 0;
    private int TotalCashCollected = 0;

    public GameObject MetersText;
    public GameObject CoinsGainedText;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
        //GameObject.Find();
        RefreshUi();
    }

    public void PlayerEscaped(float MetersRan, int CashCollected)
    {
        TotalCashCollected = TotalCashCollected + CashCollected;
        TotalMetersRan = TotalMetersRan + MetersRan;
    }

    public void RefreshUi()
    {
        CoinsGainedText.GetComponent<Text>().text = TotalCashCollected.ToString();
    }
    
}
