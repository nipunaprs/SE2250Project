using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    //Speed variables and rigidbody
    public float speed;
    private Rigidbody2D myrigidbody;

    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        
    }

    //Change the velocity depending on the set direction and speed
    void FixedUpdate()
    {
        myrigidbody.velocity = direction * speed;
    }

    //Initilize is public so it can be accessed from other objects
    //Player direction sets the direction for knife and is called in player
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    
    //Once knife is out of screen, destroy it
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
