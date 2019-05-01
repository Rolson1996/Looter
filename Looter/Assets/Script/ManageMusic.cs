using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMusic : MonoBehaviour {

    // Use this for initialization
    public AudioSource musicSource;

	void Start ()
    {
        musicSource.Play();
        ChangeVolume();
	}
    public void ChangeVolume()
    {
        if(PlayerPrefs.HasKey("MusicActive") && PlayerPrefs.GetInt("MusicActive") == 0)
        {
            musicSource.volume = 0;
        }
        else
        {
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }
}
