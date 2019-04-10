using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameObject CoinsGainedText;

    public GameObject BackpackLevelLabel;
    public GameObject ShoesLevelLabel;

    private Text BackpackLevelText;
    private Text ShoesLevelText;

    // Use this for initialization
    void Start()
    {

    }

    public void SetCashNumber(string TotalCash)
    {
        CoinsGainedText.GetComponent<Text>().text = TotalCash;
    }


    public void LevelUpBackpack()
    {
        //if amount of currentcash is equal to or greater than cost of upgrade

        //increase level of upgrade by 1
        //change the text in the shop
        //deduct amount of cash

        //else
        //display error message
    }


}
