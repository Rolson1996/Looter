using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmUI : MonoBehaviour {

    public bool AlarmActive = false;
    public float AlarmActiveTime = 0;
    public List<GameObject> UIAlarms;
    public GameObject AlarmBackgroundGrey;
    public GameObject AlarmBackgroundBlue;
    public GameObject AlarmBackgroundRed;

    // Use this for initialization
    void Start () {
        AlarmManager.E_AlarmStart += StartAlarmUI;
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

    private void StartAlarmUI(GameObject sender, AlarmEventArgs args)
    {
        AlarmActive = true;
        AlarmBackgroundGrey.SetActive(false);
    }
}
