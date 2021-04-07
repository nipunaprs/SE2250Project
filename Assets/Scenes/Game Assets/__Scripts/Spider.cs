using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spider : Enemy
{
    // Start is called before the first frame update


    private Transform player; 
    public float stoppingDistance=2; 
    public float retreatDistance=1;

    public float distanceBtw = 5f;  
   
    public float speed; 

    private float timestore= 3f; 
    private float time; 

    public float min = 1f; 

    public float max = 2f; 
 

    public GameObject projectile; 

    Vector3 startPosition; 

public bool attackNow; 

public bool isAttacking; 
    
    void Start() {
      
      //creating player reference to tag
      player = GameObject.FindGameObjectWithTag("PC").transform;

      min = transform.position.x; 
      max = transform.position.x + 3; 
      
      attackNow= false; 
      this.xp = 10;
      this.xpbar.SetMaxXP(30);
      this.xpbar.SetXP(0);

      
   }
    /*void Update() {
      
      // if the distance between the player and enemy is greater than the stopping distance...
      if(Vector2.Distance(transform.position, player.position)> stoppingDistance  ) {
        //move enemy towards the player
         transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);

         attack = true; 
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

   }*/

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Knife")
        {

            TakeDamage(5);
            //If hit with knife, start attacking
            attackNow = true;
        }
    }

        void Update () {
    if(Vector2.Distance(transform.position, player.position)<distanceBtw) {
      //transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
       attackNow = true; 


    }  if (Vector2.Distance(transform.position, player.position)> distanceBtw && attackNow==false) {
       
    transform.position = new Vector3(Mathf.PingPong(Time.time*2,max-min)+min, transform.position.y, transform.position.z); 

    }  if (Vector2.Distance(transform.position, player.position)< stoppingDistance && Vector2.Distance(transform.position, player.position)> retreatDistance ) {

      transform.position = this.transform.position;
      isAttacking = true; 

    }
      if (Vector2.Distance(transform.position, player.position)< retreatDistance) {

      transform.position = Vector2.MoveTowards(transform.position, player.position, -speed*Time.deltaTime);

   }
   if (attackNow==true ) {
    transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
   }
    

   if(time>0) {
        time-=Time.deltaTime; 
    }  else {
      if ( attackNow==true && isAttacking==true) {
          //instantiating projectiles from enemy towards player
         Instantiate(projectile, transform.position, Quaternion.identity); 
         time=timestore;  
          
      }
        
    }




   


}
}
