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

    private float ForwardSpeed = 2.75F;
    private float EscapeSpeed = -3.0F;

    private int turning = 0;

    private bool beenPaused = false;

    // Use this for initialization
    void Start () {
        playerTransform = this.gameObject.transform;
        playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        playerRigidBody.velocity = new Vector2(0, ForwardSpeed) * forwardSpeedMultiplier;
        cameraFollow = camera.GetComponent<CameraFollow>();

        sidewaysSpeedMultiplier = DataAndAchievementManager.instance.upgrades.CurrentShoesEffect;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = DataAndAchievementManager.instance.currentSkin;
    }
	// Update is called once per frame
	void Update () {

        if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.collecting)
        {
            if ( beenPaused)
            { 
                playerRigidBody.velocity = new Vector2(0, ForwardSpeed) * forwardSpeedMultiplier;
                beenPaused = false;
            }

            if (playerTransform.position.y > (6.75 * (GameplayManager.Instance.createdSections - 2)))
            {
                GameplayManager.Instance.CreateNewSection();
            }
        }
        else if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.turning)
        {
            if (turning > 100)
            {
                GameplayManager.Instance.StartEsacpe();
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
        else if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.escaping)
        {
            if (beenPaused)
            {
                playerRigidBody.velocity = new Vector2(0, EscapeSpeed) * forwardSpeedMultiplier;
                beenPaused = false;
            }

            if (playerRigidBody.position.y <= 0.5)
            {
                GameplayManager.Instance.PlayerEscaped();
            }
        }
        else if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.gameOver)
        {
            playerRigidBody.velocity = new Vector2(0, 0);
        }
        else if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.escaped)
        {
            playerRigidBody.velocity = new Vector2(0, 0);
            playerRigidBody.rotation += 1.8F;
        }
        else if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.paused)
        {
            playerRigidBody.velocity = new Vector2(0, 0);
            beenPaused = true;
        }


            var pos = transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, -2.75F, 2.75F);
        transform.localPosition = pos;
    }
    public void StartMoveSideways(int direction)
    {
        if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.collecting)
        {
            playerRigidBody.velocity = new Vector2(2.0F * direction * sidewaysSpeedMultiplier, ForwardSpeed) * forwardSpeedMultiplier;
        }
        else if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.turning || GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.paused || GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.gameOver)
        {
            playerRigidBody.velocity = new Vector2(0, 0);
        }
        else if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.escaping)
        {
            playerRigidBody.velocity = new Vector2(2.0F * direction * sidewaysSpeedMultiplier, EscapeSpeed) * forwardSpeedMultiplier;
              
        }       
    }
    public void StopMoveSideways(int direction)
    {       
        if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.collecting)
        {
            playerRigidBody.velocity = new Vector2(0, ForwardSpeed) * forwardSpeedMultiplier;
        }
        else if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.turning)
        {
            playerRigidBody.velocity = new Vector2(0, 0);
        }
        else if (GameplayManager.Instance.GetCurrentGamePhase() == GamePhase.escaping)
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
