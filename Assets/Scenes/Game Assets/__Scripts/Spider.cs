using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    // Start is called before the first frame update


    private Transform target; 
    public float stoppingDistance=3; 
    public float retreatDistance=2; 
    public float speed;
    
    void Start() {
      target = GameObject.FindGameObjectWithTag("PC").transform; 
   }
    void Update() {
      
      if(Vector2.Distance(transform.position, target.position)>stoppingDistance) {
        
         transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);

      }else if (Vector2.Distance(transform.position, target.position)< stoppingDistance && Vector2.Distance(transform.position, target.position)> retreatDistance ) {

            transform.position = this.transform.position; 
     
      }else if (Vector2.Distance(transform.position, target.position)< retreatDistance) {

          transform.position = Vector2.MoveTowards(transform.position, target.position, -speed*Time.deltaTime);

      }
   }


}
