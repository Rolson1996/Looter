using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

    private PickUpEnum.LootType pickUpType;
    private int lootLevel;
    private GameManager gameManager;


	// Use this for initialization
	void Start () {

        gameManager = GameObject.Find("EventSystem").GetComponent<GameManager>();

        lootLevel = ValueLoot(gameManager.createdSections - 2);


        switch (lootLevel)
        {
            case 1:
                pickUpType = PickUpEnum.LootType.Cash_1;
                break;
            case 2:
                pickUpType = PickUpEnum.LootType.CashStack_5;
                break;
            case 3:
                pickUpType = PickUpEnum.LootType.Goldbar_10;
                break;
            case 4:
                pickUpType = PickUpEnum.LootType.CashBag_20;
                break;
            case 5:
                pickUpType = PickUpEnum.LootType.GoldStack_40;
                break;
            case 6:
                pickUpType = PickUpEnum.LootType.Diamond_75;
                break;
            case 7:
                pickUpType = PickUpEnum.LootType.DiamondBag_200;
                break;
        }
        //set sprite of pick up


        gameManager.AddPickUpToList(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		//animation?
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pick Up Collide");

        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerCollisonFunctions>().PlayerCollideWithPickUp(pickUpType);
            this.gameObject.SetActive(false);
        }
    }
   
    private int ValueLoot(int currentLevel)
    {
        int lootBound = currentLevel / 5;

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
}
