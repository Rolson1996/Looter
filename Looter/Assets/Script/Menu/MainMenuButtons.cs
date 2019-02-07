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
	void Start ()
    {
        AnimStore = StorePanel.GetComponent<Animation>();
        AnimAchieve = AchievePanel.GetComponent<Animation>();
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
            AnimStore.Play("OpenStorePanel");
            AnimAchieve.Play("HideAchieveTab");
            SidePanelOpen = true;
        }
    }
    public void CloseStorePanel()
    {
        if (SidePanelOpen)
        {
            //Push store panel to the right
            AnimStore.Play("CloseStorePanel");
            AnimAchieve.Play("ShowAchieveTab");
            SidePanelOpen = false;
        }
    }

    public void OpenAchievementAndQuests()
    {
        if (!SidePanelOpen)
        {
            //pull over achievement Panel From right          
            AnimAchieve.Play("OpenAchievePanel");
            AnimStore.Play("HideStoreTab");
            SidePanelOpen = true;
        }
    }
    public void CloseAchievementAndQuests()
    {
        if (SidePanelOpen)
        {
            //Push achievement Panel to the right
            AnimAchieve.Play("CloseAchievePanel");
            AnimStore.Play("ShowStoreTab");
            SidePanelOpen = false;
        }
    }
}
