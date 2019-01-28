using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum LootType
{
    Cash_1,
    CashStack_5,
    Goldbar_10,
    CashBag_20,
    GoldStack_40,
    Diamond_75,
    DiamondBag_200
}
public class PickUpEnum : MonoBehaviour {
 
    public static int GetLootValue(LootType loot)
    {
        switch (loot)
        {
            case LootType.Cash_1:
                return 1;             
            case LootType.CashStack_5:
                return 5;
            case LootType.Goldbar_10:
                return 10;
            case LootType.CashBag_20:
                return 20;
            case LootType.GoldStack_40:
                return 40;               
            case LootType.Diamond_75:
                return 75;               
            case LootType.DiamondBag_200:
                return 200;             
        }
        return 1;
    }

}
