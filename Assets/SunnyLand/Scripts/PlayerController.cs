using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public CharacterController2D controller; // The reference for our Character Controller2D component ( class ) 
    public Animator anim; // Reference for our animator, we will use this to manipulate the animations later 
    public BoxCollider2D boxCol; // Reference for our colliders 
    public CircleCollider2D circleCol;
    public float movementSpeed; // The movement speed of our player, the variable is public so that it can be altered in the inspector

    public Text scoreText; // Reference to the UI text object 
    int score; // a private score variable, to keep track of our score and write the value to the UI score object 

    float horizontalMovementSpeed; // The value of our horizontalMov speed
    bool isJumping = false; // Is the player jumping>?
    bool isHurt = false; // Is the player hurt?

    // Use this for initialization
    void Start()
    {

        // Grab the components in case they weren't loaded properly
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();

        // initialize the start values
        isJumping = false;
        isHurt = false;

        score = 0;

    }

    // Update is called once per frame
    void Update()
    {

        //Update the score
        scoreText.text = score.ToString();

        //Get the movement speed value depending on our input multiplied by the movement speed
        horizontalMovementSpeed = Input.GetAxisRaw("Horizontal") * movementSpeed;
        //Cast the value to the animator, so that we can swap animations
        anim.SetFloat("speed", Mathf.Abs(horizontalMovementSpeed));
        //Did the player hit the jump key? 
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }

    }

    //Called when we hit the ground, set isjumping to false, tell the animator to update the animations
    public void OnLanding()
    {
        isJumping = false;
        anim.SetBool("isJumping", false);
    }

    //Check for collisions with other actors 
    void OnTriggerEnter2D(Collider2D col)
    {
        //Are we colliding with a gem? if so, increase our score, destroy the gem.
        if (col.gameObject.tag == "Gem")
        {
            score++;
            Destroy(col.gameObject);
        }
        //Are we colliding with an enemy? 
        else if (col.gameObject.tag == "Enemy")
        {
            if (isJumping) // If we are jumping, destroy the enemy, increase our score.
            {
                Destroy(col.gameObject);
                score++;

            }
            //Else set our hurt animation to true, and disable collisions. 
            else
            {
                anim.SetBool("isHurt", true);
                boxCol.enabled = false;
                circleCol.enabled = false;
            }
        }
    }

    //Used to calculate our movement, fixedupdate will make sure the player moves the same on all machines.
    void FixedUpdate()
    {

        controller.Move(horizontalMovementSpeed * Time.fixedDeltaTime, false, isJumping);

    }
}
