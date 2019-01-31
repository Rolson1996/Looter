using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

    public GameObject StorePanel;
    private Animation AnimStore;

    public GameObject AchievePanel;
    private Animation AnimAchieve;

    public bool SidePanelOpen = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadLevel()
    {
        if (!SidePanelOpen)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OpenStorePanel()
    {
        if (!SidePanelOpen)
        {
            //Pull over store panel from left
            AnimStore = StorePanel.GetComponent<Animation>();
            AnimStore.Play("OpenStorePanel");
            SidePanelOpen = true;
        }
    }
    public void CloseStorePanel()
    {
        if (SidePanelOpen)
        {
            //Push store panel to the right
            AnimStore = StorePanel.GetComponent<Animation>();
            AnimStore.Play("CloseStorePanel");
            SidePanelOpen = false;
        }
    }

    public void OpenAchievementAndQuests()
    {
        if (!SidePanelOpen)
        {
            //pull over achievement Panel From right
            AnimAchieve = AchievePanel.GetComponent<Animation>();
            AnimAchieve.Play("OpenAchievePanel");
            SidePanelOpen = true;
        }
    }
    public void CloseAchievementAndQuests()
    {
        if (SidePanelOpen)
        {
            //Push achievement Panel to the right
            AnimAchieve = AchievePanel.GetComponent<Animation>();
            AnimAchieve.Play("CloseAchievePanel");
            SidePanelOpen = false;
        }
    }
}
