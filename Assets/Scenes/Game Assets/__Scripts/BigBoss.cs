using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : Enemy
{

    private bool facingLeft;
    private bool facingRight;
    private bool facingDown;
    private bool facingUp;

    //Always start moving left
    private bool moveleft = true;
    private bool moveright = false;

    public float speed = 3f;

    private bool attackNow=false;
    private bool isAttacking;

    public float distanceBtw = 5f;
    public float stoppingDistance = 2;

    private Vector2 direction;
    private Transform player;

    private Rigidbody2D myrigidbody;
    private Animator myanim;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("PC").transform;
        health = 30;

    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        
        
        //If attackNow is false, then just keep running around
        if (attackNow == false)
        {
            
            HandleDirection();
            myrigidbody.velocity = direction * speed;
            CheckDistance();
        }
        else //Otherwise start moving towards and changing directions
        {
            MoveTowards();
            ChangeDirections();
        }

        if(isAttacking ==true)
        {
            HandleAttack();
        }

    }

    void CheckDistance()
    {
        //If player is within distance, then set attackNow to true;
        if (Vector2.Distance(transform.position, player.position) < distanceBtw)
        {
            attackNow = true;
        }
        
    }

    //Handle attack animations and doing damage
    void HandleAttack()
    {
        //Depending on proximity, change attack animations and direction
        bool isClose = (Vector2.Distance(transform.position, player.position) < 1);

        if (player.position.y > transform.position.y && isClose == false)
        {
            myanim.SetBool("attackup", true);
            myanim.SetBool("attackright", false);
            myanim.SetBool("attackleft", false);
            myanim.SetBool("attackdown", false);
        }
        else
        {
            if (player.position.x < transform.position.x)
            {
                myanim.SetBool("attackleft", true);
                myanim.SetBool("attackright", false);
                myanim.SetBool("attackup", false);
                myanim.SetBool("attackdown", false);
            }
            else
            {
                myanim.SetBool("attackright", true);
                myanim.SetBool("attackleft", false);
                myanim.SetBool("attackup", false);
                myanim.SetBool("attackdown", false);
            }
        }

        

    }

    void ChangeDirections()
    {
        //If player is below, play left right animations
        if (player.position.y <= transform.position.y)
        {
            if (player.position.x < transform.position.x)
            {
                myanim.SetBool("walkleft", true);
                myanim.SetBool("walkright", false);

                myanim.SetBool("walkup", false);
                myanim.SetBool("walkdown", false);
            }
            else
            {
                myanim.SetBool("walkleft", false);
                myanim.SetBool("walkright", true);

                myanim.SetBool("walkup", false);
                myanim.SetBool("walkdown", false);
            }
        }
        else //If player is above, then play walk up animation
        { 
            
            myanim.SetBool("walkup", true);
            myanim.SetBool("walkleft", false);
            myanim.SetBool("walkdown", false);
            myanim.SetBool("walkright", false);

        }
    }

    //Method to move towards the player if in range or attacked
    void MoveTowards()
    {
        //Set velocity to zero first
        myrigidbody.velocity = Vector2.zero;
        
        //If player is in attack range start attacking
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance) 
        {

            transform.position = this.transform.position;
            isAttacking = true;

        }
        else
        {
            //Code to move towards players location
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            isAttacking = false;
            ResetValues();
        }

    }

    void ResetValues()
    {
        myanim.SetBool("attackup", false);
        myanim.SetBool("attackright", false);
        myanim.SetBool("attackleft", false);
    }

    

    void HandleDirection()
    {
        //If reached left edge, set to move right
        if (this.transform.position.x <= -7.4)
        {
            moveright = true;
            moveleft = false;
            myanim.SetBool("walkright", true);
            myanim.SetBool("walkleft", false);

        }

        //If reached right edge, set move left
        if (this.transform.position.x >= 7.4)
        {
            moveright = false;
            moveleft = true;
            myanim.SetBool("walkleft", true);
            myanim.SetBool("walkright", false);

        }

        if (moveleft == true && moveright == false)
        {
            direction = Vector2.left;
        }

        if (moveleft == false && moveright == true)
        {
            direction = Vector2.right;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Knife")
        {
            //Decrease health by 5
            health = health - 5;
            //If hit with knife, start attacking
            attackNow = true;
        }

        //If playing attack animation and colliding with player, then do damage
        if (col.tag == "PC" && this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            
            //Access take damage function in player and do 1 damage
            col.gameObject.GetComponent<PlayerV2>().TakeDamage(1);

        }

    }

    



}
