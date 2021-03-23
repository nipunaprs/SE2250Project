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

    private bool attackRight;
    private bool attackDown;
    private bool attackUp;
    private bool attackLeft;


    private bool throwKnife;
    public GameObject knifePrefab;

    public Camera mainCam;
    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();

    }

    private void Update()
    {
        HandleInput();
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
        if (Input.GetKey("3")) {
            HandleTeleport();
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

        //Getting mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCam.nearClipPlane;

        //Fixing the location of the mouse position
        mousePos = mainCam.ScreenToWorldPoint(mousePosition);

        //Fixing the mouse position by removing the camera additions to the value
        float x = mousePos.x - mainCam.transform.position.x;
        float y = mousePos.y - mainCam.transform.position.y;

        //Set the player location
        GameObject.FindGameObjectWithTag("PC").GetComponent<Transform>().position = new Vector2(x, y);
    }





}
