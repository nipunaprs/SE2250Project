using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerV2 : MonoBehaviour
{

    private Rigidbody2D myrigidbody;
    private Animator myanim;

    public float movementSpeed;

    private bool facingUp = false;
    private bool facingRight = false;
    private bool facingLeft = false;
    private bool facingDown = true;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();

    }

    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        HandleMovement(horizontal,vertical);
        Flip(horizontal, vertical);
        HandleWalk();
    }

    //Method to handle movement of player using velocity
    private void HandleMovement(float horizontal,float vertical)
    {
        myrigidbody.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);

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

    



}
