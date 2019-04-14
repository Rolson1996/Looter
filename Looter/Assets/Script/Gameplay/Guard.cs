using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour, IPlayerCollides {

    private float PatrolSpeed = 2.0F;
    public List<GameObject> Waypoints;
    private int TargetWaypoint = 0;
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
        if (GameplayManager.Instance.GetCurrentGamePhase() != GamePhase.turning && GameplayManager.Instance.GetCurrentGamePhase() != GamePhase.paused)
        {
            transform.position = Vector2.MoveTowards(transform.position, Waypoints[TargetWaypoint].transform.position, PatrolSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, Waypoints[TargetWaypoint].transform.position) < 0.1)
            {
                Debug.Log("Waypoint");
                //test = false;
                TargetWaypoint++;
                if(TargetWaypoint >= Waypoints.Count)
                {
                    TargetWaypoint = 0;
                }
            }
        }
    }

    private void IncreaseGuardSpeed(GameObject sender, AlarmEventArgs args)
    {
        PatrolSpeed = 3.0F;
        AlarmManager.E_AlarmStart -= IncreaseGuardSpeed;
    }
}
