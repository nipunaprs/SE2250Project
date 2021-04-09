using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

   // public GameObject Heart;
    Vector3 startPosition; 

public bool attackNow; 

public bool isAttacking; 
    
    void Start() {
      
      //creating player reference to tag
      player = GameObject.FindGameObjectWithTag("PC").transform;
      //minimun x direction position movement for spider 
      min = transform.position.x; 
      // max x direction positiion for spider
      max = transform.position.x + 3; 
      //setting boolean attck to false at start of game 
      attackNow= false; 

      
   }
    

   void Update () {
     
     //setting attack value to true when player approaches spider 
    if(Vector2.Distance(transform.position, player.position)<distanceBtw) {
     
       attackNow = true; 

  // if the player is far away enough, the spider does not attack 
    }  if (Vector2.Distance(transform.position, player.position)> distanceBtw && attackNow==false) {
    // spider moves in horizantal direction back and forth untill player is close enough to spider   
    transform.position = new Vector3(Mathf.PingPong(Time.time*2,max-min)+min, transform.position.y, transform.position.z); 
  //if the player is close enough to the spider, the spider follows the player and attacks
    }  if (Vector2.Distance(transform.position, player.position)< stoppingDistance && Vector2.Distance(transform.position, player.position)> retreatDistance ) {

      transform.position = this.transform.position;
      isAttacking = true; 

    }
    // if player is too close to spider, spider retreats
      if (Vector2.Distance(transform.position, player.position)< retreatDistance) {

      transform.position = Vector2.MoveTowards(transform.position, player.position, -speed*Time.deltaTime);

   }
   // while the spider is attacking, it moves towards the player 
   if (attackNow==true ) {
    transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
   }
    
  // while attacknow and is attacking is true, the spider instantiates projectiles doing damage
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
