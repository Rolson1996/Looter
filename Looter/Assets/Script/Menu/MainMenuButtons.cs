using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

    public GameObject StorePanel;
    private Animation AnimStore;

    public GameObject AchievePanel;
    private Animation AnimAchieve;

    //public GameObject QuestsPanel;
    //private Animation AnimQuests;

    public bool SidePanelOpen = false;


	// Use this for initialization
	void Start ()
    {
        AchievePanel.SetActive(true);
        //QuestsPanel.SetActive(true);

        AnimStore = StorePanel.GetComponent<Animation>();
        AnimAchieve = AchievePanel.GetComponent<Animation>();
        //AnimQuests = QuestsPanel.GetComponent<Animation>();
        
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
            //AnimQuests.Play("HideQuestsTab");
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
            //AnimQuests.Play("ShowQuestsTab");
            SidePanelOpen = false;
        }
    }

    public void OpenAchievementAndStats()
    {
        if (!SidePanelOpen)
        {
            //pull over achievement Panel From right          
            AnimAchieve.Play("OpenAchievePanel");
            AnimStore.Play("HideStoreTab");
            //AnimQuests.Play("HideQuestsTab");
            SidePanelOpen = true;
        }
    }
    public void CloseAchievementAndStats()
    {
        if (SidePanelOpen)
        {
            //Push achievement Panel to the right
            AnimAchieve.Play("CloseAchievePanel");
            AnimStore.Play("ShowStoreTab");
            //AnimQuests.Play("ShowQuestsTab");
            SidePanelOpen = false;
        }
    }
    //public void OpenQuests()
    //{
    //    if (!SidePanelOpen)
    //    {
    //        //pull over achievement Panel From right          
    //        AnimAchieve.Play("HideAchieveTab");
    //        AnimStore.Play("HideStoreTab");
    //        AnimQuests.Play("OpenQuestsPanel");
    //        SidePanelOpen = true;
    //    }
    //}
    //public void CloseQuests()
    //{
    //    if (SidePanelOpen)
    //    {
    //        //Push achievement Panel to the right
    //        AnimAchieve.Play("ShowAchieveTab");
    //        AnimStore.Play("ShowStoreTab");
    //        AnimQuests.Play("CloseQuestsPanel");
    //        SidePanelOpen = false;
    //    }
    //}
}
