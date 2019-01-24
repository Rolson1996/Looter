using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    
    public GameManager gameManager;
    public Camera camera;

    private CameraFollow cameraFollow;
   
    public float forwardSpeedMultiplier = 1.0F;
    public float sidewaysSpeedMultiplier = 1.0F;

    private Transform playerTransform;
    private Rigidbody2D playerRigidBody;

    private float ForwardSpeed = 2.0F;
    private float EscapeSpeed = -2.5F;

    private int turning = 0;

    // Use this for initialization
    void Start () {
        playerTransform = this.gameObject.transform;
        playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        playerRigidBody.velocity = new Vector2(0, ForwardSpeed) * forwardSpeedMultiplier;
        cameraFollow = camera.GetComponent<CameraFollow>();
    }
	
	// Update is called once per frame
	void Update () {

        if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.collecting)
        {

            if (playerTransform.position.y > (6.75 * (gameManager.createdSections - 2)))
            {
                gameManager.CreateNewSection();
            }

        }
        else if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.turning)
        {
            if(turning > 100)
            {
                gameManager.StartEsacpe();
                playerRigidBody.rotation = 180F;
                cameraFollow.UpdateCameraOffset(-0.05F);
            }
            else
            {
                turning++;
                playerRigidBody.velocity = new Vector2(0, 0);
                playerRigidBody.rotation += 1.8F;
                cameraFollow.UpdateCameraOffset(-0.05F);
            }
        }
        else if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.escaping)
        {
            if (playerRigidBody.position.y <= 0)
            {
                gameManager.PlayerEscaped();
            }
        }
        else if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.gameOver)
        {      
            playerRigidBody.velocity = new Vector2(0, 0);           
        }
        else if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.escaped)
        {                     
            playerRigidBody.velocity = new Vector2(0, 0);
            playerRigidBody.rotation += 1.8F;
        }
    }

    public void StartMoveSideways(int direction)
    {
        //direction -1 = left || 1 = rightplayer
        //if (direction == -1)
        //{
        //    holdingLeft = true;
        //}
        //else if (direction == 1)
        //{
        //    holdingRight = true;
        //}


        //if (holdingLeft && holdingRight)
        //{
        //    playerRigidBody.velocity = new Vector2(0, 0);
        //}
        //else
        {
            if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.collecting)
            {
                playerRigidBody.velocity = new Vector2(2.0F * direction * sidewaysSpeedMultiplier, ForwardSpeed) * forwardSpeedMultiplier;
            }
            else if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.turning)
            {
                playerRigidBody.velocity = new Vector2(0, 0);
            }
            else if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.escaping)
            {
                playerRigidBody.velocity = new Vector2(2.0F * direction * sidewaysSpeedMultiplier, EscapeSpeed) * forwardSpeedMultiplier;
              
            }
        }
    }

    public void StopMoveSideways(int direction)
    {
        //if (direction == -1)
        //{
        //    holdingLeft = false;

        //    if(holdingRight)
        //    {
        //        StartMoveSideways(1);
        //    }
        //}
        //else if (direction == 1)
        //{
        //    holdingRight = false;

        //    if (holdingLeft)
        //    {
        //        StartMoveSideways(-1);
        //    }
        //}

        if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.collecting)
        {
            playerRigidBody.velocity = new Vector2(0, ForwardSpeed) * forwardSpeedMultiplier;
        }
        else if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.turning)
        {
            playerRigidBody.velocity = new Vector2(0, 0);
        }
        else if (gameManager.GetCurrentGamePhase() == GameManager.GamePhase.escaping)
        {
            playerRigidBody.velocity = new Vector2(0, EscapeSpeed) * forwardSpeedMultiplier;
        }
    }

    public void SetVelocityForEscape()
    {
        playerRigidBody.velocity = new Vector2(0, EscapeSpeed) * forwardSpeedMultiplier;
    }

}
