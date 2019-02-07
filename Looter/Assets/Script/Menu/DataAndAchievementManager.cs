using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataAndAchievementManager : MonoBehaviour {

    public static DataAndAchievementManager instance = null;

    private float TotalMetersRan = 0;
    private float FurthestRan = 0;
    private int TotalCashCollected = 0;
    private Dictionary<LootType, int> NumberOfLootTypesCollected = new Dictionary<LootType, int>();
   

    public GameObject Canvas;
    private ShopUI SUI;
    private AchievementsUI AUI;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SUI = Canvas.GetComponent<ShopUI>();
            AUI = Canvas.GetComponent<AchievementsUI>();
            RefreshUi();
            NumberOfLootTypesCollected.Add(LootType.Coin_1, 0);
            NumberOfLootTypesCollected.Add(LootType.CoinStack_5, 0);
            NumberOfLootTypesCollected.Add(LootType.CoinBag_10, 0);
            NumberOfLootTypesCollected.Add(LootType.Diamond_30, 0);
            NumberOfLootTypesCollected.Add(LootType.DiamondStack_75, 0);
            NumberOfLootTypesCollected.Add(LootType.DiamondBag_125, 0);
        }
        else
        {
            Canvas = GameObject.FindGameObjectWithTag("Canvas");
            SUI = Canvas.GetComponent<ShopUI>();
            AUI = Canvas.GetComponent<AchievementsUI>();

            RefreshUi();
            Destroy(this.gameObject);
        }
        
        
    }

    public void PlayerEscaped(float MetersRan, int CashCollected, List<LootType> LootTypes)
    {
        TotalCashCollected = TotalCashCollected + CashCollected;
        TotalMetersRan = TotalMetersRan + MetersRan;

        if(FurthestRan < MetersRan)
        {
            FurthestRan = MetersRan;
        }

        foreach (LootType loot in LootTypes)
        {
            NumberOfLootTypesCollected[loot]++;
        }
    }

    public void RefreshUi()
    {
        SUI.SetCashNumber(TotalCashCollected);
    }
    
}
