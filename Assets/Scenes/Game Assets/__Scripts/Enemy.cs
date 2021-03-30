using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject spider;
    public int health = 15;
    public int xp;
    public Slider slider;

    public XPBar xpbar;
    public Transform heart;
    

    

    void OnCollisionEnter2D(Collision2D collision)
     {
       
        Transform rootT = collision.gameObject.transform.root;

        //We then get reference to the colliding object
        GameObject go = rootT.gameObject; 
        // S = go.GetComponent<MovementNew>();
        Animator anim = go.GetComponent<Animator>();
        
        if ( anim.GetCurrentAnimatorStateInfo(0).IsName("attackright") &&(go.tag=="PC"))
        {
            
           TakeDamage(1);
        }
        if ( anim.GetCurrentAnimatorStateInfo(0).IsName("attackup") &&(go.tag=="PC"))
        {
            
           TakeDamage(1);
        }
        if ( anim.GetCurrentAnimatorStateInfo(0).IsName("attackleft") &&(go.tag=="PC"))
        {
            
           TakeDamage(1);
        }
        if ( anim.GetCurrentAnimatorStateInfo(0).IsName("attackdown") &&(go.tag=="PC"))
        {
            
           TakeDamage(1);
        }

    
         
     }

     void OnTriggerEnter2D(Collider2D col)
    {
        Transform rootT = col.gameObject.transform.root;

        //We then get reference to the colliding object
        GameObject go = rootT.gameObject; 

        if (go.tag == "Knife") {
            TakeDamage(5);
        }
    }

     void FixedUpdate() {
         if (health <= 0) {
             print("dead");
            
             Destroy(this.gameObject);
             xpbar.SetXP((int)slider.value + xp);

            if (xpbar.IsMax()) {
               GameObject prefab = GameObject.FindGameObjectWithTag("Heart");
               Instantiate(prefab, new Vector2(spider.transform.position.x,spider.transform.position.y), Quaternion.identity);
               xpbar.ResetXP(50);
               
            }
             
         }
        
     }

     void TakeDamage(int damage) {

         
        health = health - damage;
         
     }

     
}
