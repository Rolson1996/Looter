using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class StatsData
{
    int CurrentCash;

    float TotalMetersRan;
    float FurthestTraveledIntoVault;
    int TotalLootCollected;
    int MostLootCollected;
    int TotalAttemptedVaultRaids;
    int TotalVaultEscapes;
    int[] LootTypeCounters = new int[6];


    public int GetCurrentCash()
    {
        return CurrentCash;
    }
    public float GetTotalMeters()
    {
        return TotalMetersRan;
    }
    public float GetFurthest()
    {
        return FurthestTraveledIntoVault;
    }
    public int GetTotalLoot()
    {
        return TotalLootCollected;
    }
    public int GetMostLoot()
    {
        return MostLootCollected;
    }
    public int GetRaids()
    {
        return TotalAttemptedVaultRaids;
    }
    public int GetEscape()
    {
        return TotalVaultEscapes;
    }
    public int[] GetLootTypeCounters()
    {
        return LootTypeCounters;
    }
    public void SetCurrentCash(int newValue)
    {
        CurrentCash = newValue;
    }
    public void SetTotalMeters(float newValue)
    {
        TotalMetersRan = newValue;
    }
    public void SetFurthest(float newValue)
    {
        FurthestTraveledIntoVault = newValue;
    }
    public void SetTotalCash(int newValue)
    {
        TotalLootCollected = newValue;
    }
    public void SetMostCash(int newValue)
    {
        MostLootCollected = newValue;
    }
    public void SetAttemptedRaids(int newValue)
    {
        TotalAttemptedVaultRaids = newValue;
    }
    public void SetTotalEsacpes(int newValue)
    {
        TotalVaultEscapes= newValue;
    }
    public void SetCountOfLootType(int type, int count)
    {
        LootTypeCounters[type] = count;
    }

}

