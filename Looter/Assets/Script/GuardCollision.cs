using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCollision : MonoBehaviour {

    private void Start()
    {
        //starting moving towards waypoint if waypoint exists
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Guard Collide");

        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerCollisonFunctions>().PlayerCollideWithEnemy();
        }

        this.gameObject.SetActive(false);
    }
}
