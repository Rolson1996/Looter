using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmUI : MonoBehaviour {

    public bool AlarmActive = false;
    private Animation AnimAlarm;

    // Use this for initialization
    void Start () {
        AlarmManager.E_AlarmStart += StartAlarmUI;
        AnimAlarm = this.GetComponent<Animation>();
    }

    private void StartAlarmUI(GameObject sender, AlarmEventArgs args)
    {
        if(AnimAlarm == null)
        {
            AnimAlarm = this.GetComponent<Animation>();
        }
        AlarmActive = true;
        AnimAlarm.wrapMode = WrapMode.Loop;
        AnimAlarm.Play();
        AlarmManager.E_AlarmStart -= StartAlarmUI;
    }
}
