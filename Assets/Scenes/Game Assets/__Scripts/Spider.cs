using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    // Start is called before the first frame update


    private Transform player; 
    public float stoppingDistance=3; 
    public float retreatDistance=2; 
   
    public float speed; 

    private float timestore= 3f; 
    private float time; 
 

    public GameObject projectile; 
    
    
    void Start() {
      
      //creating player reference to tag
      player = GameObject.FindGameObjectWithTag("PC").transform;

       


   }
    void Update() {
      
      // if the distance between the player and enemy is greater than the stopping distance...
      if(Vector2.Distance(transform.position, player.position)>stoppingDistance) {
        //move enemy towards the player
         transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
        //if player distance is smaller than the stoppinf distance and greater than the retreating distance ...
      }else if (Vector2.Distance(transform.position, player.position)< stoppingDistance && Vector2.Distance(transform.position, player.position)> retreatDistance ) {
          //enemy stops moving 
            transform.position = this.transform.position; 
     // id the player distance is smaller than the retreat distance (player too close to enemy) 
      }else if (Vector2.Distance(transform.position, player.position)< retreatDistance) {
          //the enemy moves away from player
          transform.position = Vector2.MoveTowards(transform.position, player.position, -speed*Time.deltaTime);

      }

    //instantiating projectiles 
    if(time>0) {
        time-=Time.deltaTime; 
    } else {
        //instantiating projectiles from enemy towards player
         Instantiate(projectile, transform.position, Quaternion.identity); 
         time=timestore; 
    }

   }


}
