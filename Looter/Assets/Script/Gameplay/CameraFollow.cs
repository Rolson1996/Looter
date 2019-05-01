using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

    private float cameraOffset = 2.5F;
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = new Vector3(0, player.transform.position.y + cameraOffset, -10);
        this.transform.position = new Vector3(-500, Mathf.Clamp(this.transform.position.y, 2, this.transform.position.y), -10);
    }

    public void UpdateCameraOffset(float f)
    {
        cameraOffset += f;
    }
}
