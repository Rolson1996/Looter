using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public GameObject UITextCapacity;
    public GameObject UITextLootCollected;

    public GameObject PrototypeGameOverText;
    public GameObject PrototypeEscapedText;

    // Use this for initialization
    void Start () {
        PrototypeGameOverText.SetActive(false);
        PrototypeEscapedText.SetActive(false);
    }

    public void SetNumberOfLoot(int Num)
    {
        UITextLootCollected.GetComponent<Text>().text = Num.ToString();
    }
    public void SetBackpackSize(int Num)
    {
        UITextCapacity.GetComponent<Text>().text = Num.ToString();
    }

    public void Escaped(int LootCount, int LootValue)
    {
        PrototypeEscapedText.SetActive(true);
        PrototypeEscapedText.GetComponent<Text>().text = "You escaped, congratulations. \nYou escaped with " + LootCount + " piece(s) of loot. \nAmount of money earned: " + LootValue;
    }

    public void GameOver()
    {
        PrototypeGameOverText.SetActive(true);
    }
}
