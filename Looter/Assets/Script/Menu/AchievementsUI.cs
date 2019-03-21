using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour {

    public GameObject TotalMetersRanText;
    public GameObject FurthestTraveledIntoVaultText;
    public GameObject TotalLootCollectedText;
    public GameObject MostLootCollectedText;   
    public GameObject TotalAttemptedVaultRaidsText;
    public GameObject TotalVaultEscapesText;

    public List<GameObject> LootTypeCounters;

    public void SetTextTotalMeters(string newValue)
    {
        TotalMetersRanText.GetComponent<Text>().text = newValue + "M";
    }
    public void SetTextFurthest(string newValue)
    {
        FurthestTraveledIntoVaultText.GetComponent<Text>().text = newValue + "M";
    }
    public void SetTextTotalCash(string newValue)
    {
        TotalLootCollectedText.GetComponent<Text>().text = newValue;
    }
    public void SetTextMostCash(string newValue)
    {
        MostLootCollectedText.GetComponent<Text>().text = newValue;
    }
    public void SetTextAttemptedRaids(string newValue)
    {
        TotalAttemptedVaultRaidsText.GetComponent<Text>().text = newValue + " Raids";
    }
    public void SetTextTotalEsacpes(string newValue)
    {
        TotalVaultEscapesText.GetComponent<Text>().text = newValue + " Escapes";
    }

    public void SetCountOfLootType(int type, int count)
    {
        LootTypeCounters[type].GetComponent<Text>().text = count.ToString();
    }
}
