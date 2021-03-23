using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{

    public float speed;
    private Rigidbody2D myrigidbody;

    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        
    }


    void FixedUpdate()
    {
        myrigidbody.velocity = direction * speed;
    }


    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
