using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataAndAchievementManager : MonoBehaviour {

    public static DataAndAchievementManager instance = null;

    private int CurrentCash = 0;

    private float TotalMetersRan = 0;
    private float FurthestRan = 0;
    private int TotalCashCollected = 0;
    private int MostCashCollected = 0;
    private int AttemptedRaids = 0;
    private int TotalEsacpes = 0;

    private Dictionary<LootType, int> NumberOfLootTypesCollected = new Dictionary<LootType, int>();

    [HideInInspector]
    public Dictionary<int, bool> UnlockedSkins = new Dictionary<int, bool>();
   

    public GameObject Canvas;
    private ShopUI SUI;
    private AchievementsUI AUI;


    public Sprite currentSkin;
    public int currentSkinNumber = 0;


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

            UnlockedSkins.Add(0, true);
            UnlockedSkins.Add(1, true);
            UnlockedSkins.Add(2, true);
            UnlockedSkins.Add(3, true);
            UnlockedSkins.Add(4, true);

        }
        else
        {
            instance.Canvas = GameObject.FindGameObjectWithTag("Canvas");
            instance.SUI = Canvas.GetComponent<ShopUI>();
            instance.AUI = Canvas.GetComponent<AchievementsUI>();

            instance.RefreshUi();
            Destroy(this.gameObject);
        }
        
        
    }

    public void PlayerEscaped(float MetersRan, int CashCollected, List<LootType> LootTypes)
    {
        CurrentCash = CurrentCash + CashCollected;

        TotalCashCollected = TotalCashCollected + CashCollected;
        TotalMetersRan = TotalMetersRan + (MetersRan * 2);

        if(FurthestRan < MetersRan)
        {
            FurthestRan = MetersRan;
        }
        if (MostCashCollected < CashCollected)
        {
            MostCashCollected = CashCollected;
        }
        TotalEsacpes = TotalEsacpes + 1;
        AttemptedRaids = AttemptedRaids + 1;

        foreach (LootType loot in LootTypes)
        {
            NumberOfLootTypesCollected[loot]++;
        }
    }
    public void PlayerCaught()
    {
        AttemptedRaids = AttemptedRaids + 1;
    }

    public void RefreshUi()
    {
        SUI.SetCashNumber(CurrentCash.ToString());

        AUI.SetTextAttemptedRaids(AttemptedRaids.ToString());
        AUI.SetTextFurthest(((int)FurthestRan).ToString());
        AUI.SetTextMostCash(MostCashCollected.ToString());
        AUI.SetTextTotalEsacpes(TotalEsacpes.ToString());
        AUI.SetTextTotalCash(TotalCashCollected.ToString());
        AUI.SetTextTotalMeters(((int)TotalMetersRan).ToString());
    }
    
}
