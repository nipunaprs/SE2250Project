using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerV2 : MonoBehaviour
{

    //Connects to player rigidbody and animator
    private Rigidbody2D myrigidbody;
    private Animator myanim;

    //Variables to point to the UI images
    //public Image teleImage;
    //public Image invImage;
    public Image imagePower;

    //Image objects and booleans to control images
    private GameObject lvl1image;
    private GameObject lvl2image;
    private GameObject controlsimage;
    private GameObject deathimage;
    private GameObject endimage;
    private GameObject storyone;
    private GameObject storytwo;
    private bool reachlvl2 = false;
    private bool reachend = false;
    private bool isdead = false;
    private bool resetme = false;
    private bool resetme2 = false;

    //Helps track movement speed
    public float movementSpeed;

    //Stores various states of the player
    private bool facingUp = false;
    private bool facingRight = false;
    private bool facingLeft = false;
    private bool facingDown = true;

    private bool attackRight;
    private bool attackDown;
    private bool attackUp;
    private bool attackLeft;

    private bool canTeleport;
    private bool canInvincible;
    private bool bombhurt = false;
    private bool isInvincible;
    private int playerChoice;

    private bool throwKnife;
    public GameObject knifePrefab;
    public Sprite invul;
    public Sprite tele;

    public XPBar xpbar;

    public int maxHealth = 150; //Set player max value
    public int currentHealth = 150; //tracks current health

    public HealthBar healthBar; //Gets healthbar

    //Time variable
    private float timestore = 5f, teleTime,invTime,powerTime;

    
    private bool gotKey = false;


    // Start is called before the first frame update
    void Start()
    {
        
        myrigidbody = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();
        playerChoice = PlayerPrefs.GetInt("SavedInteger");

        //Ensures currenthealth is at max health
        currentHealth = maxHealth;

        //Sets healthbar
        healthBar.SetMaxHealth(maxHealth);

        //Ensures player spawns with ability to teleport
        canTeleport = true;
        canInvincible = true;

        //Set the invtime to 5
        invTime = timestore;

        //Get image objects
        lvl1image = GameObject.Find("LvlOne");
        lvl2image = GameObject.Find("LvlTwo");
        controlsimage = GameObject.Find("Controls");
        deathimage = GameObject.Find("Death");
        endimage = GameObject.Find("End");
        storyone = GameObject.Find("StoryOne");
        storytwo = GameObject.Find("StoryTwo");

        //Set everything except story1 image to false
        lvl2image.SetActive(false);
        controlsimage.SetActive(false);
        deathimage.SetActive(false);
        endimage.SetActive(false);
        lvl1image.SetActive(false);
        storytwo.SetActive(false);

        if (playerChoice == 1) {
            imagePower.sprite = invul;
        }
        if (playerChoice == 2) {
            imagePower.sprite = tele;
        }

    }

    private void Update()
    {
        HandleInput();

        //If canTeleport is false, then start the timer
        if(canTeleport == false)
        {
            HandleTeleTime();
            
        }

        //If canTeleport is false, then start the timer
        if(canInvincible == false)
        {
            HandleInvTime();
        }

        HandleImage();

    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        HandleMovement(horizontal,vertical);
        Flip(horizontal, vertical);
        HandleWalk();
        HandleAttacks();
        ResetValues();
    }

    private void HandleImage()
    {
        

        //If Escape is pressed, disable all screens
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            lvl1image.SetActive(false);
            lvl2image.SetActive(false);
            controlsimage.SetActive(false);
            deathimage.SetActive(false);
            endimage.SetActive(false);
            storyone.SetActive(false);
            storytwo.SetActive(false);

            reachlvl2 = false;
        }

        if (storyone.activeInHierarchy == false && resetme ==false)
        {
            storytwo.SetActive(true);
            resetme = true;
        }

        if(storytwo.activeInHierarchy == false && storyone.activeInHierarchy == false && resetme2 ==false)
        {
            lvl1image.SetActive(true);
            resetme2 = true;
        }


        //Enable controls screen if C is pressed
        if(Input.GetKeyDown(KeyCode.C))
        {
            controlsimage.SetActive(true);
        }

        //If reached level2 show the lvl2
        if(reachlvl2 == true)
        {
            lvl2image.SetActive(true);
        }

        //Show end screen after reached end is true and disable player
        if(reachend==true)
        {
            endimage.SetActive(true);
            gameObject.SetActive(false);
        }

        //Show death screen after player health is less than or equal to 0
        if(isdead==true)
        {
            deathimage.SetActive(true);
            gameObject.SetActive(false);
        }


    }
    

    //Handles the invisibility time duration and cool down
    private void HandleInvTime()
    {
        
        //This condition occurs if the player is currently invincible
        if (isInvincible) {
            //Decrement the power duration time
            powerTime -= Time.deltaTime;
            
            //If the invincible duratino runs outs the player no longer is invincible
            if (powerTime < 0) {
                isInvincible=false;
            }
        }
        else {
            
            //If time is greater than 0, then start reducing time on the cooldown
            if (invTime > 0)
            {
                //Take off a second, every update;
                invTime -= Time.deltaTime;

                //We set the UI image so it indicates to the player that their powerup is currently coolign down
                Color c = imagePower.color;

                if (invTime <= 5 && invTime > 3) {
                    
                    c.a = 0.0f;
                }
                if (invTime <= 3 && invTime > 1) {
                    
                    c.a = 0.5f;
                }
                if (invTime <= 1 && invTime > 0) {
                    
                    c.a = 1.0f;
                }
                
                imagePower.color = c;
                
            }
            else
            {
               //After cool down set the capability of becoming invinsible to true
                canInvincible = true;
                invTime = timestore; 
            }
        }
        
    }

    private void HandleTeleTime()
    {
        //If time is greater than 0, then start reducing time
        if (teleTime > 0)
        {
            //Take off a second, every update;
            teleTime -= Time.deltaTime;
            Color c = imagePower.color;

            if (teleTime <= 5 && teleTime > 3) {
                
                c.a = 0.0f;
            }
            if (teleTime <= 3 && teleTime > 1) {
                
                c.a = 0.5f;
            }
            if (teleTime <= 1 && teleTime > 0) {
                
                c.a = 1.0f;
            }
            
            imagePower.color = c;
            
        }
        else
        {
            //After reaching end of timer, set canTeleport to true
            canTeleport = true;
            teleTime = timestore; 
        }
    }

    //Method to handle movement of player using velocity
    private void HandleMovement(float horizontal,float vertical)
    {
        //Only allow movement if any Attack tag animation isn't playing
        if(!this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myrigidbody.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);

        }

    }

    //Method to handle walking animations and set them depending on the facing direction
    private void HandleWalk()
    {
        //If facingright, then switch to walk right animation
        if (facingRight == true) myanim.SetBool("walkright", true);
        else myanim.SetBool("walkright", false);

        //If facingleft, then switch to walk left animation
        if (facingLeft == true) myanim.SetBool("walkleft", true);
        else myanim.SetBool("walkleft", false);

        //If facingup, then switch to walk up animation
        if (facingUp == true) myanim.SetBool("walkup", true);
        else myanim.SetBool("walkup", false);

        //If facingdown, then switch to walk down animation
        if (facingDown == true) myanim.SetBool("walkdown", true);
        else myanim.SetBool("walkdown", false);

    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (facingDown) attackDown = true;
            if (facingRight) attackRight = true;
            if (facingLeft) attackLeft = true;
            if (facingUp) attackUp = true;
            
        }
        if (Input.GetKey("1")) {

            //Only if canTeleport is true, allow teleportation
            if (canTeleport && playerChoice==2)
            {
                HandleTeleport();
                canTeleport = false;
            }
             //Only if canInvisble is true, allow invincibility
            else if (canInvincible && playerChoice==1)
            {
                
                //Player becomes invincible, reset the powerup duration, and don't allow the player to use the powerup currently
                isInvincible = true;
                powerTime = 5.0f;
                canInvincible = false;
            }
        }

        //Throw knife when press V
        if(Input.GetKeyDown(KeyCode.Z))
        {
            //Play same knife attack animation
            if (facingDown) attackDown = true;
            if (facingRight) attackRight = true;
            if (facingLeft) attackLeft = true;
            if (facingUp) attackUp = true;
            throwKnife = true;
            ThrowKnife(0);

            
        }

        //REMOVE AFTERWARDS, TRANSPORT TO LEVEL2 QUICK
        if(Input.GetKeyDown(KeyCode.B))
        {
            transform.position = new Vector3(0, 50, 0);
        }

        //Sprint when pressing Q is pressed down
        if(Input.GetKey(KeyCode.Q))
        {
            myanim.SetFloat("runmultiplier", 3);
            movementSpeed = 8;
        }
        else
        {
            ResetRun();
        }
        
        


    }

    //Resets all attack values every FixedUpdate at the end
    private void ResetValues()
    {
        attackRight = false;
        attackDown = false;
        attackUp = false;
        attackLeft = false;
        throwKnife = false;
 
    }

    private void ResetRun()
    {
        myanim.SetFloat("runmultiplier", 1);
        movementSpeed = 5;
    }
    
    public void ThrowKnife(int value)
    {
        //If facingright and after any Attack animation is done, then allow to throw another knife
        if(facingRight && !this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //Create a knife, set it to temp gameobject
            GameObject tmp = (GameObject) Instantiate(knifePrefab, transform.position, Quaternion.Euler(new Vector3(0,0, 180)));
            //Pass the correct direction to send the knife
            tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else if (facingLeft && !this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) 
        {
            //Create a knife, set it to temp gameobject
            GameObject tmp = (GameObject) Instantiate(knifePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //Pass the correct direction to send the knife
            tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
        else if (facingUp && !this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //Create a knife, set it to temp gameobject
            GameObject tmp = (GameObject) Instantiate(knifePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            //Pass the correct direction to send the knife
            tmp.GetComponent<Knife>().Initialize(Vector2.up);
        }
        else if (facingDown && !this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //Create a knife, set it to temp gameobject
            GameObject tmp = (GameObject) Instantiate(knifePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            //Pass the correct direction to send the knife
            tmp.GetComponent<Knife>().Initialize(Vector2.down);
        }

    }

    private void HandleAttacks()
    {
        //If attackRight is true and we're not already attacking
        if(attackRight && !this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //Call attack
            myanim.SetTrigger("attackright");
            //When attacking set velocity to zero
            myrigidbody.velocity = Vector2.zero;


        }

        //If attackLeft is true and we're not already attacking
        if (attackLeft && !this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //Call attack
            myanim.SetTrigger("attackleft");
            //When attacking set velocity to zero
            myrigidbody.velocity = Vector2.zero;
        }

        //If attackUp is true and we're not already attacking
        if (attackUp && !this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //Call attack
            myanim.SetTrigger("attackup");
            //When attacking set velocity to zero
            myrigidbody.velocity = Vector2.zero;
        }

        //If attackDown is true and we're not already attacking
        if (attackDown && !this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //Call attack
            myanim.SetTrigger("attackdown");
            //When attacking set velocity to zero
            myrigidbody.velocity = Vector2.zero;
        }
    }

    //Method to flip the facing direction (up,down,left,right) boolean values
    private void Flip(float horizontal, float vertical)
    {
        //If going to the right and not facing right or going to left and facing right, then invert the values
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            
        }

        //If going to the left and not facing left or going to right and facing left, then invert the values
        if (horizontal < 0 && !facingLeft || horizontal > 0 && facingLeft)
        {
            facingLeft = !facingLeft;

        }

        //If going up and not facing up, or if going down and facing up, then invert the values 
        if (vertical > 0 && !facingUp || vertical < 0 && facingUp)
        {
            facingUp = !facingUp;

        }

        //If going down and not facing down, or if going up and facing down, then invert the values 
        if (vertical < 0 && !facingDown || vertical > 0 && facingDown)
        {
            facingDown = !facingDown;

        }

        //If going up and to right or going up and to left, set only facing horizontal values
        if (vertical > 0 && horizontal > 0 || vertical > 0 && horizontal < 0)
        {
            if (horizontal > 0)
            {
                facingRight = true;
                facingUp = false;
            }
            if (horizontal < 0)
            {
                facingLeft = true;
                facingUp = false;
            }
        }

        //If going down and to right or going down and to left, set only facing horizontal values
        if (vertical < 0 && horizontal > 0 || vertical < 0 && horizontal < 0)
        {
            if (horizontal > 0)
            {
                facingRight = true;
                facingDown = false;
            }
            if (horizontal < 0)
            {
                facingLeft = true;
                facingDown = false;
            }
        }

        //If no movement in vertical, set those values to false
        if (vertical == 0)
        {
            
            facingUp = false;
            facingDown = false;
            
        }

        //If no movement in horizontal, set those values to false
        if(horizontal == 0)
        {
            facingRight = false;
            facingLeft = false;
        }

    }

    //Teleports player
    private void HandleTeleport()
    {

        //Depending on the direction the player faces they will be teleported in that direction a number of units
        if (facingRight)
        {
            GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position = new Vector2(GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position.x + 4, GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position.y);
        }
        if (facingDown)
        {
            GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position = new Vector2(GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position.x, GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position.y - 4);
        }
        if (facingUp)
        {
            GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position = new Vector2(GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position.x, GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position.y + 4);
        }
        if (facingLeft)
        {
            GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position = new Vector2(GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position.x - 4, GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position.y);
        }


    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible) {
            //Remove health
            currentHealth = currentHealth - damage;

            //Update health
            healthBar.SetHealth(currentHealth);
            if(currentHealth<=0) {
                //Destroy(gameObject); 
                isdead = true;
                
                
            }
        }
        
    }

    private void increaseHealth() {
        currentHealth = currentHealth + 10;
        maxHealth = maxHealth + 10;

        healthBar.SetHealth(currentHealth);
        healthBar.SetMaxHealth(maxHealth);

    }

    //This handles the collision action
    void OnCollisionEnter2D(Collision2D collision)
     {
         //Player takes damage if they touch enemy
         if( collision.gameObject.tag.Equals("Enemy") == true ) {

            TakeDamage(2);

         }

         //Player takes damage if they touch the bat
        if (collision.gameObject.tag.Equals("bat") == true)
        {
            TakeDamage(2);
        }


        /*//If in range of bomb and the blow up animation is playing then do damage
        if (collision.gameObject.tag.Equals("bomb") == true && bombhurt == false)//&& col.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Blow") == true)
        {

            if (collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Blow"))
            {
                TakeDamage(5);
                bombhurt = true;
            }
            
        }*/


    }

    //If player gets hit with a projectile they take damage
    void OnTriggerEnter2D(Collider2D col)
    {

        //Checks to see if the tag is projectile
        if (col.tag == "projectile") {
            TakeDamage(1);
        }

        if (col.tag == "Heart") {
            increaseHealth(); 
            col.gameObject.SetActive(false);
           
        }

        //Teleport to second level
        if(col.tag == "teleport")
        {
            print("touched");
            this.transform.position = new Vector3(0,50,0);
            reachlvl2 = true;

        }

        if(col.tag == "bat")
        {
            TakeDamage(1);
        }

        //Touch the lava, do 10 damage
        if(col.tag == "lava")
        {
            TakeDamage(10);
        }

        

        //Got the key, destroy the key and set gotKey to true
        if(col.tag == "key")
        {
            gotKey = true;
            Destroy(col.gameObject);
            GameObject prefab = GameObject.FindGameObjectWithTag("Heart");
            Instantiate(prefab, new Vector2(GameObject.FindGameObjectWithTag("key").transform.position.x,GameObject.FindGameObjectWithTag("key").transform.position.y), Quaternion.identity);
            xpbar.IncrementXP(30);
            xpbar.ResetXP();
            
        }

        //Finish the game when touch the final flag and you got the key
        if(col.gameObject.name == "finalflag" && gotKey == true)
        {
            //Show finished game screen
            reachend = true;
            
        }

        //If in range of bomb and the blow up animation is playing then do damage
        if (col.gameObject.tag.Equals("bomb") == true && bombhurt == false)
        {
            
            if (col.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Blow"))
            {
                TakeDamage(5);
                bombhurt = true;
            }

        }


    }



}
