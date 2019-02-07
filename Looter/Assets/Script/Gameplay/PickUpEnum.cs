﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum LootType
{
    Coin_1,
    CoinStack_5,
    CoinBag_10,
    Diamond_30,
    DiamondStack_75,
    DiamondBag_125
}
public class PickUpEnum : MonoBehaviour {
 
    public static int GetLootValue(LootType loot)
    {
        switch (loot)
        {
            case LootType.Coin_1:
                return 1;             
            case LootType.CoinStack_5:
                return 5;
            case LootType.CoinBag_10:
                return 10;
            case LootType.Diamond_30:
                return 30;               
            case LootType.DiamondStack_75:
                return 75;               
            case LootType.DiamondBag_125:
                return 125;             
        }
        return 1;
    }

}
