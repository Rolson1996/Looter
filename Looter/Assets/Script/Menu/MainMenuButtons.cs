using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenStorePanel()
    {
        //Pull over store panel from left
    }

    public void OpenAchievementAndQuests()
    {
        //pull over achievement Panel From right
    }
}
