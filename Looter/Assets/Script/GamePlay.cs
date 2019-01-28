using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GamePhase
{
    collecting,
    turning,
    escaping,
    gameOver,
    escaped
}

public class GamePlay : MonoBehaviour {

    


    private GamePhase currentGamePhase;
    public bool AlarmActive = false;

    public GameObject player;   
    public int createdSections = 2;

    public List<GameObject> SectionPrefabs;

    private List<GameObject> PickUpsOnMap = new List<GameObject>();

    public List<PickUpEnum.LootType> CollectedLoot = new List<PickUpEnum.LootType>();

    public float AlarmActiveTime = 0;

    public int SizeOfBackpack = 5;

    public List<GameObject> UIAlarms;
    public GameObject AlarmBackgroundGrey;
    public GameObject AlarmBackgroundBlue;
    public GameObject AlarmBackgroundRed;

    public GameObject UITextCapacity;
    public GameObject UITextLootCollected;

    public GameObject PrototypeGameOverText;
    public GameObject PrototypeEscapedText;


    void Awake()
    {
        new CollisionManager();
        currentGamePhase = GamePhase.collecting;
        player.GetComponent<Player>().gameManager = this;
        //player.GetComponent<PlayerCollisonFunctions>().gameManager = this;

        UITextCapacity.GetComponent<Text>().text = SizeOfBackpack.ToString();
        UITextLootCollected.GetComponent<Text>().text = "0";
     
        CreateNewSection();

        AlarmBackgroundBlue.SetActive(false);
        AlarmBackgroundRed.SetActive(false);

        PrototypeGameOverText.SetActive(false);
        PrototypeEscapedText.SetActive(false);


        CollisionManager.E_GuardCollides += AlertGuards;
        CollisionManager.E_GuardCollides += HitGuard;
        
    }

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		if (AlarmActive)
        {
            AlarmActiveTime += Time.fixedDeltaTime;

            if (AlarmActiveTime >= 0 && AlarmActiveTime < 1)
            {
                UIAlarms[0].SetActive(true);
                UIAlarms[1].SetActive(false);
                //Set Background red
                AlarmBackgroundBlue.SetActive(false);
                AlarmBackgroundRed.SetActive(true);

            }
            else if (AlarmActiveTime >= 1 && AlarmActiveTime < 2)
            {
                UIAlarms[0].SetActive(true);
                UIAlarms[1].SetActive(false);
                //Set Background blue
                AlarmBackgroundBlue.SetActive(true);
                AlarmBackgroundRed.SetActive(false);
            }
            else if (AlarmActiveTime >= 2 && AlarmActiveTime < 3)
            {
                UIAlarms[0].SetActive(false);
                UIAlarms[1].SetActive(true);
                //Set Background red
                AlarmBackgroundBlue.SetActive(false);
                AlarmBackgroundRed.SetActive(true);
            }
            else if (AlarmActiveTime >= 3 && AlarmActiveTime < 4)
            {
                UIAlarms[0].SetActive(false);
                UIAlarms[1].SetActive(true);
                //Set Background blue
                AlarmBackgroundBlue.SetActive(true);
                AlarmBackgroundRed.SetActive(false);
            }
            else if (AlarmActiveTime > 4)
            {
                AlarmActiveTime = 0;
            }

        }
	}
    
    public void StartTurning()
    {
        currentGamePhase = GamePhase.turning;
    }

    public void StartEsacpe()
    {
        currentGamePhase = GamePhase.escaping;
        player.GetComponent<Player>().SetVelocityForEscape();
    }

    public void StartAlarm()
    {
        if (!AlarmActive)
        {
            Debug.Log("Alarm On");
            AlarmActive = true;
            RemovePickUpsFromMap();
            AlarmBackgroundGrey.SetActive(false);
            //Start Alarm Sound
        }
    }

    public void SetGamePhase(GamePhase newPhase)
    {
        currentGamePhase = newPhase;
    }

    public GamePhase GetCurrentGamePhase()
    {
        return currentGamePhase;
    }

    public void CreateNewSection()
    {
        if (currentGamePhase == GamePhase.collecting)
        {
            int r = Random.Range(0, SectionPrefabs.Count);
           
            Instantiate(SectionPrefabs[r], new Vector3(-500, 6.75F * createdSections, 1), new Quaternion(0, 0, 0, 0));
            createdSections++;
        }
    }
 
    public void AddPickUpToList(GameObject p)
    {
        PickUpsOnMap.Add(p);
    }

    public void PlayerEscaped()
    {
        Debug.Log("Player Escaped");
        SetGamePhase(GamePhase.escaped);
        PrototypeEscapedText.SetActive(true);

        int lootValue = 0;
        foreach (PickUpEnum.LootType l in CollectedLoot)
        {
            lootValue = lootValue + PickUpEnum.GetLootValue(l);
        }

        PrototypeEscapedText.GetComponent<Text>().text = "You escaped, congratulations. \nYou escaped with " + CollectedLoot.Count + " piece(s) of loot. \nAmount of money earned: " + lootValue;
    }

    public void PickedUpLoot(PickUpEnum.LootType Loot)
    {
        CollectedLoot.Add(Loot);
        UITextLootCollected.GetComponent<Text>().text = CollectedLoot.Count.ToString();
        //change loot counter on UI

        if (CollectedLoot.Count >= SizeOfBackpack)
        {
            StartTurning();
            RemovePickUpsFromMap();
        }
    }

    public void DropLoot(GameObject guardHit, GuardCollideEventArgs args)
    {
        if (CollectedLoot.Count > 0)
        {
            CollectedLoot.RemoveAt(CollectedLoot.Count - 1);
            UITextLootCollected.GetComponent<Text>().text = CollectedLoot.Count.ToString();

            if (CollectedLoot.Count <= 0)
            {
                //GameOver
                GameOver();
            }
        }
        else
        {
            GameOver();
        }
    }

    private void RemovePickUpsFromMap()
    {
        foreach (GameObject p in PickUpsOnMap)
        {
            Destroy(p);
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        SetGamePhase(GamePhase.gameOver);
        PrototypeGameOverText.SetActive(true);
    }

    void AlertGuards(GameObject guardHit, GuardCollideEventArgs args)
    {
        if (GetCurrentGamePhase() == GamePhase.collecting)
        {
            StartTurning();
        }
        StartAlarm();
        RemovePickUpsFromMap();

        CollisionManager.E_GuardCollides -= AlertGuards;
        CollisionManager.E_GuardCollides += DropLoot;

    }

    void HitGuard(GameObject guardHit, GuardCollideEventArgs args)
    {
        Destroy(guardHit);
    }
}
