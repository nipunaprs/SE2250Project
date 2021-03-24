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

    private float timeBtwShots; 

    public float startTimeBtwShots; 

    public GameObject projectile; 
    
    
    void Start() {
      
      player = GameObject.FindGameObjectWithTag("PC").transform;

      timeBtwShots = startTimeBtwShots; 


   }
    void Update() {
      
      if(Vector2.Distance(transform.position, player.position)>stoppingDistance) {
        
         transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);

      }else if (Vector2.Distance(transform.position, player.position)< stoppingDistance && Vector2.Distance(transform.position, player.position)> retreatDistance ) {

            transform.position = this.transform.position; 
     
      }else if (Vector2.Distance(transform.position, player.position)< retreatDistance) {

          transform.position = Vector2.MoveTowards(transform.position, player.position, -speed*Time.deltaTime);

      }

    if(time>0) {
        time-=Time.deltaTime; 
    } else {
         Instantiate(projectile, transform.position, Quaternion.identity); 
         time=timestore; 
    }

   }


}
