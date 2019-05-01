using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IPlayerCollides {

    private LootType TypeOfLoot;
    private int LootLevel;
    
	// Use this for initialization
	void Start () {

        LootLevel = GenerateLootValue(GameplayManager.Instance.createdSections - 2);

        switch (LootLevel)
        {
            case 1:
                TypeOfLoot = LootType.Coin_1;
                break;
            case 2:
                TypeOfLoot = LootType.CoinStack_5;
                break;
            case 3:
                TypeOfLoot = LootType.CoinBag_10;
                break;
            case 4:
                TypeOfLoot = LootType.Diamond_30;
                break;
            case 5:
                TypeOfLoot = LootType.DiamondStack_75;
                break;
            case 6:
                TypeOfLoot = LootType.DiamondBag_125;
                break;           
        }
        //set sprite of pick up
        this.GetComponent<SpriteRenderer>().sprite = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PickUpEnum>().GetLootSprite(TypeOfLoot);

        GameplayManager.Instance.AddPickUpToList(this.gameObject);
        AlarmManager.E_AlarmStart += DestoryPickUp;
    }
	
    private int GenerateLootValue(int currentLevel)
    {
        int lootBound = currentLevel / 3;

        if (lootBound == 1)
        {
            return 1;
        }
        else if (lootBound == 2)
        {
            return Random.Range(1, 3);
        }
        else if (lootBound == 7)
        {
            return Random.Range(5, 7);
        }
        else if (lootBound > 8)
        {
            return 6;
        }
        else
        {
            return Random.Range(lootBound - 2, lootBound + 1);
        }
    }

    public void CollideWithPlayer()
    {
        CollisionManager.LootCollides(this.gameObject, new LootCollideEventArgs(this, TypeOfLoot));
        AlarmManager.E_AlarmStart -= DestoryPickUp;
        Destroy(this.gameObject);
    }

    public void DestoryPickUp(GameObject sender, AlarmEventArgs args) //Attach to OnStartTurning Event Dispatcher
    {
        AlarmManager.E_AlarmStart -= DestoryPickUp;
        Destroy(this.gameObject);
    }
}
