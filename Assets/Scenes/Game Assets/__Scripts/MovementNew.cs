using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementNew : MonoBehaviour
{

    //VARIABLES

    public float speed = 5;
    Animator anim;
    Rigidbody2D rg;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
    }

    void MovePlayer()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");


        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
    }

    //Run into border, stop
    private void OnCollisionEnter2D(Collision2D collision)
    {
      

    }

    // Update is called once per frame
    void Update()
    {

        

        //If pressing down and left
        if (Input.GetKey("down") && Input.GetKey("left"))
        {
            anim.SetBool("isrunningleft", true);
            anim.SetBool("isrunningforward", false);
            MovePlayer();
        }
        else if (Input.GetKey("down") && Input.GetKey("right"))
        {
            anim.SetBool("isrunningright", true);
            anim.SetBool("isrunningforward", false);
            MovePlayer();
        }
        else if (Input.GetKey("up") && Input.GetKey("down"))         //If pressing both
        {
            anim.SetBool("isrunningup", false);
            anim.SetBool("isrunningforward", false);
        }
        else if (Input.GetKey("up") && Input.GetKey("right"))         //If pressing both
        {
            anim.SetBool("isrunningup", false);
            anim.SetBool("isrunningright", true);
            MovePlayer();
        }
        else if (Input.GetKey("up") && Input.GetKey("left"))         //If pressing both
        {
            anim.SetBool("isrunningup", false);
            anim.SetBool("isrunningleft", true);
            MovePlayer();
        }
        else if (Input.GetKey("right") && Input.GetKey("left"))         //If pressing both
        {
            anim.SetBool("isrunningright", false);
            anim.SetBool("isrunningleft", false);
        }
        else
        {

        
            if (Input.GetKey("up"))
            {
                anim.SetBool("isrunningup", true);
                MovePlayer();
            }
            else
            {
                anim.SetBool("isrunningup", false);
            }

            if (Input.GetKey("down"))
            {
                anim.SetBool("isrunningforward", true);
                MovePlayer();
            }
            else
            {
                anim.SetBool("isrunningforward", false);
            }

            if (Input.GetKey("right"))
            {
                anim.SetBool("isrunningright", true);
                MovePlayer();
            }
            else
            {
                anim.SetBool("isrunningright", false);
            }

            if (Input.GetKey("left"))
            {
                anim.SetBool("isrunningleft", true);
                MovePlayer();
            }
            else
            {
                anim.SetBool("isrunningleft", false);
            }

        }







    }
}
