using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//using UnityEngine.Advertisements;
using UnityEngine.Monetization;
using UnityEngine.Analytics;

public class GameUI : MonoBehaviour
{

    public GameObject ShowAdButton;

    public GameObject GameOverCharacter;

    public GameObject UITextCapacity;
    public GameObject UITextLootCollected;

    public GameObject EscapedPanel;
    public GameObject GameOverPanel;

    public GameObject LootValueCollectedEscaped;
    private Text LootValueText;
    private int ShownLootValue;
    public GameObject LootToSafe;
    public GameObject LootToSafeStartLocation;
    public Animation AnimShowLoot = null;

    private int lootValue;
    private List<LootType> collectedLoot;
    private float metersRan;


    private PickUpEnum pickUpEnum;
    private int lootShown = 0;

    public GameObject PausePanel;
    private GamePhase phaseBeforePause;

    public string placementId = "rewardedVideo";

#if UNITY_IOS
   private string gameId = "3125490";
#elif UNITY_ANDROID
    private string gameId = "3125491";
#endif

    // Use this for initialization
    void Start()
    {       
        EscapedPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
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

            if (LootValueText == null)
            {
                LootValueText = LootValueCollectedEscaped.GetComponent<Text>();
                LootValueText.text = "0";
                ShownLootValue = 0;
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

        //Analytics
        Dictionary<string, object> eventData = new Dictionary<string, object>();
        eventData.Add("Amount Collected", LootValue);
        eventData.Add("Distance Ran", MetersRan);

        AnalyticsEvent.Custom("PlayerEsacped", eventData);

        EscapedPanel.SetActive(true);       
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        GameOverCharacter.GetComponent<Image>().sprite = DataAndAchievementManager.instance.currentSkin;
    }

    public void UpdateLoot()
    {
        DataAndAchievementManager.instance.PlayerEscaped(metersRan, lootValue, collectedLoot);
    }
    public void WatchedAd()
    {
        //Analytics
        AnalyticsEvent.AdComplete(true, AdvertisingNetwork.None);

        DataAndAchievementManager.instance.PlayerEscaped(0, lootValue, null);
        LootValueText.text = (ShownLootValue * 2).ToString();

        ShowAdButton.SetActive(false);
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
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            // Reward the player
            WatchedAd();
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
            AnalyticsEvent.Custom("Ad Skipped");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
            AnalyticsEvent.Custom("Show Ad Failed");
        }
    }

    private void ShowLoot()
    {
        LootToSafe.GetComponent<Image>().sprite = pickUpEnum.GetLootSprite(collectedLoot[lootShown]);
        LootToSafe.transform.position = LootToSafeStartLocation.transform.position;
        AnimShowLoot.Play();

        ShownLootValue = ShownLootValue + PickUpEnum.GetLootValue(collectedLoot[lootShown]);
        LootValueText.text = ShownLootValue.ToString();
    }

    public void PauseGame()
    {
        phaseBeforePause = GameplayManager.Instance.GetCurrentGamePhase();
        GameplayManager.Instance.SetGamePhase(GamePhase.paused);
        PausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        GameplayManager.Instance.SetGamePhase(phaseBeforePause);
        PausePanel.SetActive(false);
    }  
}
