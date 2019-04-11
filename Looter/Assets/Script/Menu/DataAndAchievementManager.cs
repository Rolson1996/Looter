using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


public class DataAndAchievementManager : MonoBehaviour
{

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

    private StatsData statsData;
    public PurchaseableUpgrades upgrades;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SUI = Canvas.GetComponent<ShopUI>();
            AUI = Canvas.GetComponent<AchievementsUI>();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStats = null;

            try
            {
                fileStats = File.Open(Application.persistentDataPath + "/statsData.data", FileMode.Open);
            }
            catch
            {

            }

            if (fileStats != null)
            {
                statsData = (StatsData)bf.Deserialize(fileStats);
                fileStats.Close();

                NumberOfLootTypesCollected.Add(LootType.Coin_1, statsData.GetLootTypeCounters()[0]);
                NumberOfLootTypesCollected.Add(LootType.CoinStack_5, statsData.GetLootTypeCounters()[1]);
                NumberOfLootTypesCollected.Add(LootType.CoinBag_10, statsData.GetLootTypeCounters()[2]);
                NumberOfLootTypesCollected.Add(LootType.Diamond_30, statsData.GetLootTypeCounters()[3]);
                NumberOfLootTypesCollected.Add(LootType.DiamondStack_75, statsData.GetLootTypeCounters()[4]);
                NumberOfLootTypesCollected.Add(LootType.DiamondBag_125, statsData.GetLootTypeCounters()[5]);

                TotalMetersRan = statsData.GetTotalMeters();
                FurthestRan = statsData.GetFurthest();
                TotalCashCollected = statsData.GetTotalLoot();
                MostCashCollected = statsData.GetMostLoot();
                AttemptedRaids = statsData.GetRaids();
                TotalEsacpes = statsData.GetEscape();

                CurrentCash = statsData.GetCurrentCash();
            }
            else
            {
                NumberOfLootTypesCollected.Add(LootType.Coin_1, 0);
                NumberOfLootTypesCollected.Add(LootType.CoinStack_5, 0);
                NumberOfLootTypesCollected.Add(LootType.CoinBag_10, 0);
                NumberOfLootTypesCollected.Add(LootType.Diamond_30, 0);
                NumberOfLootTypesCollected.Add(LootType.DiamondStack_75, 0);
                NumberOfLootTypesCollected.Add(LootType.DiamondBag_125, 0);

                statsData = new StatsData();
            }

            FileStream fileUpgrades = null;

            try
            {
                fileUpgrades = File.Open(Application.persistentDataPath + "/upgradesData.data", FileMode.Open);
            }
            catch
            {

            }
            if (fileUpgrades != null)
            {
                upgrades = (PurchaseableUpgrades)bf.Deserialize(fileUpgrades);
                fileUpgrades.Close();
            }
            else
            {
                upgrades = new PurchaseableUpgrades();
            }


            RefreshUi();

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

        if (FurthestRan < MetersRan)
        {
            FurthestRan = MetersRan;
        }
        if (MostCashCollected < CashCollected)
        {
            MostCashCollected = CashCollected;
        }
        TotalEsacpes = TotalEsacpes + 1;
        AttemptedRaids = AttemptedRaids + 1;

        if (LootTypes != null)
        {
            foreach (LootType loot in LootTypes)
            {
                NumberOfLootTypesCollected[loot]++;
            }
        }

        SaveDataToFile();
    }
    public void PlayerCaught(float MetersRan)
    {
        AttemptedRaids = AttemptedRaids + 1;
        TotalMetersRan = TotalMetersRan + (MetersRan * 2);

        if (FurthestRan < MetersRan)
        {
            FurthestRan = MetersRan;
        }
        SaveDataToFile();
    }

    public void RefreshUi()
    {
        SUI.UpdateCashNumber();

        AUI.SetTextAttemptedRaids(AttemptedRaids.ToString());
        AUI.SetTextFurthest(((int)FurthestRan).ToString());
        AUI.SetTextMostCash(MostCashCollected.ToString());
        AUI.SetTextTotalEsacpes(TotalEsacpes.ToString());
        AUI.SetTextTotalCash(TotalCashCollected.ToString());
        AUI.SetTextTotalMeters(((int)TotalMetersRan).ToString());

        int typeNum = 0;

        foreach (KeyValuePair<LootType, int> kvp in NumberOfLootTypesCollected)
        {
            AUI.SetCountOfLootType(typeNum, kvp.Value);
            typeNum++;
        }
    }

    public void SaveDataToFile()
    {
        //Save Data to file
        statsData.SetCurrentCash(CurrentCash);
        statsData.SetAttemptedRaids(AttemptedRaids);
        statsData.SetFurthest(FurthestRan);
        statsData.SetMostCash(MostCashCollected);
        statsData.SetTotalCash(TotalCashCollected);
        statsData.SetTotalEsacpes(TotalEsacpes);
        statsData.SetTotalMeters(TotalMetersRan);

        int typeNum = 0;
        foreach (KeyValuePair<LootType, int> kvp in NumberOfLootTypesCollected)
        {
            statsData.SetCountOfLootType(typeNum, kvp.Value);
            typeNum++;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStats = File.Create(Application.persistentDataPath + "/statsData.data");
        bf.Serialize(fileStats, statsData);
        fileStats.Close();

        FileStream fileUpgrades = File.Create(Application.persistentDataPath + "/upgradesData.data");
        bf.Serialize(fileUpgrades, upgrades);
        fileUpgrades.Close();
    }

    public int GetCurrentCash()
    {
        return CurrentCash;
    }

    public void SpendCash(int cost)
    {
        CurrentCash = CurrentCash - cost;
    }
}
