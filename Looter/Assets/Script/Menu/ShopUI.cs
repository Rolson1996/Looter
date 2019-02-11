using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameObject CoinsGainedText;

    // Use this for initialization
    void Start()
    {

    }

    public void SetCashNumber(string TotalCash)
    {
        CoinsGainedText.GetComponent<Text>().text = TotalCash;
    }

}
