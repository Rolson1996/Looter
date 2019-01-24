using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

    float cameraOffset = 2.5F;
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = new Vector3(0, player.transform.position.y + cameraOffset, -10);
	}

    public void UpdateCameraOffset(float f)
    {
        cameraOffset += f;
    }
}
