using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bat : Enemy
{
    //private Transform player;

    //Always start moving left
    private bool moveleft = true;
    private bool moveright = false;

    public float speed;

    private float timestore = 5f;
    private float time;

    private Vector2 direction;
    public GameObject Bomb;
    private Rigidbody2D myrigidbody;
    private Animator myanim;

    // Start is called before the first frame update
    void Start()
    {
        //creating player reference to tag
        //player = GameObject.FindGameObjectWithTag("PC").transform;

        myrigidbody = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();
        health = 5;
        time = timestore;
    }

    private void FixedUpdate()
    {
        HandleDirection();
        myrigidbody.velocity = direction * speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) Destroy(gameObject);

        if (time > 0)
        {
            time -= Time.deltaTime;
            
        }
        else
        {
            Instantiate(Bomb, transform.position, Quaternion.identity);
            time = timestore;
        }
    }

    void HandleDirection()
    {
        //If reached left edge, set to move right
        if(this.transform.position.x <= -7.4)
        {
            moveright = true;
            moveleft = false;
            myanim.SetBool("flyright", false);
            myanim.SetBool("flyleft", true);
            
        }

        //If reached right edge, set move left
        if (this.transform.position.x >= 7.4)
        {
            moveright = false;
            moveleft = true;
            myanim.SetBool("flyleft", false);
            myanim.SetBool("flyright", true);
            
        }

        if (moveleft == true && moveright == false)
        {
            direction = Vector2.left;
        }

        if(moveleft == false && moveright == true)
        {
            direction = Vector2.right;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Knife")
        {
            
            health = health - 5;
        }
    }
    






    }
