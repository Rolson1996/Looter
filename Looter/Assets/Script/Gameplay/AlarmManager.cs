using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour {

    public static AlarmManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    

    public delegate void AlarmStartInteraction(GameObject sender, AlarmEventArgs args);

    public static event AlarmStartInteraction E_AlarmStart;

    public static void AlarmStarts(GameObject sender, AlarmEventArgs args)
    {
        if (E_AlarmStart != null)
        {
            E_AlarmStart(sender, args);
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
