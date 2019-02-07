using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum GamePhase
{
    collecting,
    turning,
    escaping,
    gameOver,
    escaped
}

public class GameplayManager : MonoBehaviour {

    public static GameplayManager Instance = null;

    private GamePhase currentGamePhase = GamePhase.collecting;

    public GameObject player;   
    public int createdSections = 2;

    public List<GameObject> SectionPrefabs;

    private List<GameObject> PickUpsOnMap = new List<GameObject>();

    public List<LootType> CollectedLoot = new List<LootType>();

    public int SizeOfBackpack = 5;  

    public GameUI UI;


    private float MetersRan = 0;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            new CollisionManager();
            new AlarmManager();

            UI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameUI>();
            UI.SetBackpackSize(SizeOfBackpack);
            UI.SetNumberOfLoot(0);

            CreateNewSection();

            CollisionManager.E_GuardCollides += AlertGuards;
            CollisionManager.E_LootCollides += PickedUpLoot;
        }
        else
        {
            Instance.ResetValues();
            Destroy(this.gameObject);
        }
              
        
        //Attach Tracking for achievments, quest and tracking of currency?
    }
    public void ResetValues()
    {
        //reset variables
        PickUpsOnMap.Clear();
        CollectedLoot.Clear();

        currentGamePhase = GamePhase.collecting;
        MetersRan = 0;
        createdSections = 2;

        player = GameObject.FindGameObjectWithTag("Player");

        UI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameUI>();
        UI.SetBackpackSize(SizeOfBackpack);
        UI.SetNumberOfLoot(0);

        CreateNewSection();
        CollisionManager.E_GuardCollides -= DropLoot;
        CollisionManager.E_GuardCollides += AlertGuards;
    }
    // Update is called once per frame
    void Update ()
    {
        		
	}
    
    public void StartTurning()
    {
        currentGamePhase = GamePhase.turning;
        MetersRan = player.transform.position.y;
    }

    public void StartEsacpe()
    {
        currentGamePhase = GamePhase.escaping;
        player.GetComponent<Player>().SetVelocityForEscape();
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

    private void RemovePickUpsFromMap()
    {
        foreach (GameObject p in PickUpsOnMap)
        {
            Destroy(p);
        }
    }
    //Game Ends Methods
    public void PlayerEscaped()
    {
        CollisionManager.E_GuardCollides -= AlertGuards;
        CollisionManager.E_GuardCollides -= DropLoot;

        Debug.Log("Player Escaped");
        SetGamePhase(GamePhase.escaped);
        
        int lootValue = 0;
        foreach (LootType l in CollectedLoot)
        {
            lootValue = lootValue + PickUpEnum.GetLootValue(l);
        }

        DataAndAchievementManager.instance.PlayerEscaped(MetersRan, lootValue, CollectedLoot);

        UI.Escaped(CollectedLoot.Count, lootValue);

        SceneManager.LoadScene(0);
    }
    private void GameOver()
    {
        CollisionManager.E_GuardCollides -= AlertGuards;
        CollisionManager.E_GuardCollides -= DropLoot;

        Debug.Log("Game Over");
        SetGamePhase(GamePhase.gameOver);
        UI.GameOver();

        SceneManager.LoadScene(0);
    }


    //Event Dispatcher Methods
    void PickedUpLoot(GameObject pickUp, LootCollideEventArgs args)
    {
        CollectedLoot.Add(args.lootType);
        
        UI.SetNumberOfLoot(CollectedLoot.Count);

        if (CollectedLoot.Count >= SizeOfBackpack)
        {
            StartTurning();
            RemovePickUpsFromMap();
        }
    }

    public void DropLoot(GameObject guardHit, GuardCollideEventArgs args)
    {
        if (CollectedLoot.Count > 1)
        {
            CollectedLoot.RemoveAt(CollectedLoot.Count - 1);
            UI.SetNumberOfLoot(CollectedLoot.Count);
        }
        else
        {
            GameOver();
        }
    }

    void AlertGuards(GameObject guardHit, GuardCollideEventArgs args)
    {
        if (GetCurrentGamePhase() == GamePhase.collecting)
        {
            if (CollectedLoot.Count == 0)
            {
                GameOver();
            }
            StartTurning();
        }
        else
        {
            if (CollectedLoot.Count <= 1)
            {
                GameOver();
            }
        }
        AlarmManager.AlarmStarts(guardHit, new AlarmEventArgs());

        CollisionManager.E_GuardCollides -= AlertGuards;
        CollisionManager.E_GuardCollides += DropLoot;
    }       
}
