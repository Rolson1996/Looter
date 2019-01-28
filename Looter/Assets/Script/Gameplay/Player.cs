using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
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

        if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.collecting)
        {

            if (playerTransform.position.y > (6.75 * (GameplayManager.instance.createdSections - 2)))
            {
                GameplayManager.instance.CreateNewSection();
            }

        }
        else if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.turning)
        {
            if(turning > 100)
            {
                GameplayManager.instance.StartEsacpe();
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
        else if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.escaping)
        {
            if (playerRigidBody.position.y <= 0)
            {
                GameplayManager.instance.PlayerEscaped();
            }
        }
        else if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.gameOver)
        {      
            playerRigidBody.velocity = new Vector2(0, 0);           
        }
        else if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.escaped)
        {                     
            playerRigidBody.velocity = new Vector2(0, 0);
            playerRigidBody.rotation += 1.8F;
        }
    }

    public void StartMoveSideways(int direction)
    {      
        
        if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.collecting)
        {
            playerRigidBody.velocity = new Vector2(2.0F * direction * sidewaysSpeedMultiplier, ForwardSpeed) * forwardSpeedMultiplier;
        }
        else if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.turning)
        {
            playerRigidBody.velocity = new Vector2(0, 0);
        }
        else if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.escaping)
        {
            playerRigidBody.velocity = new Vector2(2.0F * direction * sidewaysSpeedMultiplier, EscapeSpeed) * forwardSpeedMultiplier;
              
        }       
    }

    public void StopMoveSideways(int direction)
    {       
        if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.collecting)
        {
            playerRigidBody.velocity = new Vector2(0, ForwardSpeed) * forwardSpeedMultiplier;
        }
        else if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.turning)
        {
            playerRigidBody.velocity = new Vector2(0, 0);
        }
        else if (GameplayManager.instance.GetCurrentGamePhase() == GamePhase.escaping)
        {
            playerRigidBody.velocity = new Vector2(0, EscapeSpeed) * forwardSpeedMultiplier;
        }
    }

    public void SetVelocityForEscape()
    {
        playerRigidBody.velocity = new Vector2(0, EscapeSpeed) * forwardSpeedMultiplier;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayerCollides interactable = collision.GetComponent<IPlayerCollides>();

        if (interactable != null)
        {
            interactable.CollideWithPlayer();
        }        
    }

}
