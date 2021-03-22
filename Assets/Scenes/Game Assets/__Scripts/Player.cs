using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 speed = new Vector2(50,50);
    public int maxHealth = 20; //Set player max value
    public int currentHealth = 20; //tracks current health

    public HealthBar healthBar; //Gets healthbar


    void Start()
    {
        currentHealth = maxHealth;//set current health to max at start of game

        //Set the slider health to max
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update() {

    }

    //example take damage
    void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        healthBar.SetHealth(currentHealth);
    }
    void onTriggerEnter2D(Collider other) {
        speed = speed*-1; 
        TakeDamage(2);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
     {
         print("Hit");

         TakeDamage(2);
         speed = speed*-1; 
         
     }


     
    
}
