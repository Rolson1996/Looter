using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour, IPlayerCollides {
    public void CollideWithPlayer()
    {
        //Event Dispacter
        CollisionManager.GuardCollides(this.gameObject, new GuardCollideEventArgs(this));
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
