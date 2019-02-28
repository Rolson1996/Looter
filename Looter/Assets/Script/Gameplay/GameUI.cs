using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{

    public GameObject UITextCapacity;
    public GameObject UITextLootCollected;

    public GameObject PrototypeGameOverText;
    public GameObject PrototypeEscapedText;

    public GameObject EscapedPanel;
    public GameObject GameOverPanel;


    private int lootValue;
    private List<LootType> collectedLoot;
    private float metersRan;

    // Use this for initialization
    void Start()
    {
        PrototypeGameOverText.SetActive(false);
        PrototypeEscapedText.SetActive(false);
        EscapedPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void SetNumberOfLoot(int Num)
    {
        UITextLootCollected.GetComponent<Text>().text = Num.ToString();
    }
    public void SetBackpackSize(int Num)
    {
        UITextCapacity.GetComponent<Text>().text = Num.ToString();
    }

    public void Escaped(int LootCount, int LootValue, List<LootType> CollectedLoot, float MetersRan)
    {
        lootValue = LootValue;
        collectedLoot = CollectedLoot;
        metersRan = MetersRan;

        PrototypeEscapedText.SetActive(true);
        EscapedPanel.SetActive(true);
        PrototypeEscapedText.GetComponent<Text>().text = "You escaped, congratulations. \nYou escaped with " + LootCount + " piece(s) of loot. \nAmount of money earned: " + LootValue;
    }

    public void GameOver()
    {
        PrototypeGameOverText.SetActive(true);
        GameOverPanel.SetActive(true);
    }

    public void UpdateLoot(int IncreaseValue = 1)
    {
        DataAndAchievementManager.instance.PlayerEscaped(metersRan, lootValue * IncreaseValue, collectedLoot);
    }

    public void ReturnMenu()
    {
        UpdateLoot();
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {
        UpdateLoot();
        SceneManager.LoadScene(1);
    }

    public void ShowAdvert()
    {
        //Load advert

        UpdateLoot(2);
        SceneManager.LoadScene(1);
    }
}
