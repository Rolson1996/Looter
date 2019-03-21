using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{

    public GameObject ShowAdButton;

    public GameObject GameOverCharacter;

    public GameObject UITextCapacity;
    public GameObject UITextLootCollected;

    public GameObject EscapedPanel;
    public GameObject GameOverPanel;

    public GameObject LootToSafe;
    public GameObject LootToSafeStartLocation;
    public Animation AnimShowLoot = null;

    private int lootValue;
    private List<LootType> collectedLoot;
    private float metersRan;


    private PickUpEnum pickUpEnum;
    private int lootShown = 0;

    // Use this for initialization
    void Start()
    {       
        EscapedPanel.SetActive(false);
        GameOverPanel.SetActive(false);
              
    }
    void Update()
    {
        if(EscapedPanel.activeSelf)
        {
            if (AnimShowLoot == null)
            {
                AnimShowLoot = LootToSafe.GetComponent<Animation>();
            }

            if(pickUpEnum == null)
            {
                pickUpEnum = this.gameObject.GetComponent<PickUpEnum>();
            }

            if(!AnimShowLoot.isPlaying && lootShown < collectedLoot.Count)
            {
                ShowLoot();
                lootShown++;
            }
        }
        
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

        UpdateLoot();

        //PrototypeEscapedText.SetActive(true);
        EscapedPanel.SetActive(true);
        //PrototypeEscapedText.GetComponent<Text>().text = "You escaped, congratulations. \nYou escaped with " + LootCount + " piece(s) of loot. \nAmount of money earned: " + LootValue;
    }

    public void GameOver()
    {
        //PrototypeGameOverText.SetActive(true);
        GameOverPanel.SetActive(true);
        GameOverCharacter.GetComponent<Image>().sprite = DataAndAchievementManager.instance.currentSkin;
    }

    public void UpdateLoot()
    {
        DataAndAchievementManager.instance.PlayerEscaped(metersRan, lootValue, collectedLoot);
    }
    public void WatchedAd()
    {
        DataAndAchievementManager.instance.PlayerEscaped(0, lootValue, null);
    }

    public void ReturnMenu()
    {      
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {      
        SceneManager.LoadScene(1);
    }

    public void ShowAdvert()
    {
        //Load advert
        WatchedAd();
        ShowAdButton.SetActive(false);      
    }

    private void ShowLoot()
    {
        LootToSafe.GetComponent<Image>().sprite = pickUpEnum.GetLootSprite(collectedLoot[lootShown]);
        LootToSafe.transform.position = LootToSafeStartLocation.transform.position;
        AnimShowLoot.Play();
    }

}
