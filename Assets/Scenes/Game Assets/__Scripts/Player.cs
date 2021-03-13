﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
public Vector2 speed = new Vector2(50,50);
    public Animator anim;
    public KeyCode RightKey;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update() {

    float inputX = Input.GetAxis ("Horizontal"); 
    float inputY = Input.GetAxis("Vertical"); 

    Vector3 movement = new Vector3 (speed.x*inputX, speed.y*inputY, 0);

    movement *= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            anim.Play("");


    transform.Translate(movement);
    
    }
}
