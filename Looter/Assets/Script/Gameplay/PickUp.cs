using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IPlayerCollides {

    private LootType TypeOfLoot;
    private int LootLevel;
    


	// Use this for initialization
	void Start () {

        LootLevel = GenerateLootValue(GameplayManager.instance.createdSections - 2);


        switch (LootLevel)
        {
            case 1:
                TypeOfLoot = LootType.Cash_1;
                break;
            case 2:
                TypeOfLoot = LootType.CashStack_5;
                break;
            case 3:
                TypeOfLoot = LootType.Goldbar_10;
                break;
            case 4:
                TypeOfLoot = LootType.CashBag_20;
                break;
            case 5:
                TypeOfLoot = LootType.GoldStack_40;
                break;
            case 6:
                TypeOfLoot = LootType.Diamond_75;
                break;
            case 7:
                TypeOfLoot = LootType.DiamondBag_200;
                break;
        }
        //set sprite of pick up


        GameplayManager.instance.AddPickUpToList(this.gameObject);
        AlarmManager.E_AlarmStart += DestoryPickUp;
    }
	
	// Update is called once per frame
	void Update ()
    {
		//animation?
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
        else if (lootBound == 9)
        {
            return Random.Range(6, 8);
        }
        else if (lootBound > 9)
        {
            return 7;
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

    private void DestoryPickUp(GameObject sender, AlarmEventArgs args) //Attach to OnStartTurning Event Dispatcher
    {
        AlarmManager.E_AlarmStart -= DestoryPickUp;
        Destroy(this.gameObject);
    }
}
