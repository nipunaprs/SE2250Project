using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject spider;
    public int health = 16;

    void OnCollisionEnter2D(Collision2D collision)
     {
       
        Transform rootT = collision.gameObject.transform.root;

        //We then get reference to the colliding object
        GameObject go = rootT.gameObject; 
        // S = go.GetComponent<MovementNew>();
        Animator anim = go.GetComponent<Animator>();
        
        if ( anim.GetCurrentAnimatorStateInfo(0).IsName("attackright") &&(go.tag=="PC"))
        {
            
           TakeDamage(3);
        }
        if ( anim.GetCurrentAnimatorStateInfo(0).IsName("attackup") &&(go.tag=="PC"))
        {
            
           TakeDamage(3);
        }
        if ( anim.GetCurrentAnimatorStateInfo(0).IsName("attackleft") &&(go.tag=="PC"))
        {
            
           TakeDamage(3);
        }
        if ( anim.GetCurrentAnimatorStateInfo(0).IsName("attackdown") &&(go.tag=="PC"))
        {
            
           TakeDamage(3);
        }
         
     }

     void FixedUpdate() {
         if (health <= 0) {
             print("dead");
             Destroy(spider);
         }
        
     }

     void TakeDamage(int damage) {

         
        health = health - damage;
         
     }

     
}
