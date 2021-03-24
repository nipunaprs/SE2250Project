using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //variables 
    public float speed; 
    private Transform player; 
    private Vector2 target; 
    void Start()
    {
        //setting player object to player 
        player = GameObject.FindGameObjectWithTag("PC").transform; 
        //setting target movement to player position 
        target = new Vector2(player.position.x, player.position.y); 
    }

    void Update()
    {
        //moves projectiles towards player position 
        transform.position = Vector2.MoveTowards(transform.position, target, speed*Time.deltaTime);

        //if projectile reaches players position, projectile is destroyed 
        if(transform.position.x == target.x && transform.position.y == target.y) {
            DestroyProjectile(); 

        } 
    }
    // projectile is destroyed on collision with player
    void OnTriggerEnter2D (Collider2D other) {
        if(other.CompareTag("PC")) {
            DestroyProjectile(); 
        }
    }

    // destroying projectile 
    void DestroyProjectile() {
        Destroy(gameObject); 
    }
}
