using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour, IPlayerCollides {

    private float PatrolSpeed = 2.0F;
    private List<GameObject> Waypoints;

    public void CollideWithPlayer()
    {
        //Event Dispacter
        CollisionManager.GuardCollides(this.gameObject, new GuardCollideEventArgs(this));
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        AlarmManager.E_AlarmStart += IncreaseGuardSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		//Animation

        //Traverse Waypoints
	}

    private void IncreaseGuardSpeed(GameObject sender, AlarmEventArgs args)
    {
        PatrolSpeed = 3.0F;
        AlarmManager.E_AlarmStart -= IncreaseGuardSpeed;
    }
}
