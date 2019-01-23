using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisonFunctions : MonoBehaviour {


    public GameManager gameManager;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerCollideWithEnemy()
    {
        if(gameManager.CollectedLoot.Count == 0)
        {
            gameManager.DropLoot();
        }
        if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.collecting)
        {
            gameManager.StartTurning();
        }
        if (gameManager.AlarmActive)
        {
            gameManager.DropLoot();         
        }
        gameManager.StartAlarm();
        
    }

    public void PlayerCollideWithPickUp(PickUpEnum.LootType lootPickedUp)
    {
        gameManager.PickedUpLoot(lootPickedUp);

    }
}
