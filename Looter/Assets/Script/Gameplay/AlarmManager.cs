using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour {

    public static AlarmManager instance = null;
    static bool AlarmActive = false;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public delegate void AlarmStartInteraction(GameObject sender, AlarmEventArgs args);

    public static event AlarmStartInteraction E_AlarmStart;

    public static void AlarmStarts(GameObject sender, AlarmEventArgs args)
    {
        if (E_AlarmStart != null)
        {
            E_AlarmStart(sender, args);
            AlarmActive = true;
            Debug.Log("Alarm On");
        }
    }
}

public class AlarmEventArgs
{  
    public AlarmEventArgs()
    {

    }
}
