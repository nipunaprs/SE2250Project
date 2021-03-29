using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        

        if (attackNow == false)
        {
            
            HandleDirection();
            myrigidbody.velocity = direction * speed;
            CheckDistance();
        }
        else
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

    void HandleAttack()
    {
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

    void MoveTowards()
    {
        myrigidbody.velocity = Vector2.zero;
        

        if (Vector2.Distance(transform.position, player.position) < stoppingDistance) //&& Vector2.Distance(transform.position, player.position) > retreatDistance)
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

            //health = health - 5;
        }
    }



}
