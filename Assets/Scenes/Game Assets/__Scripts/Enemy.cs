using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int health;

    void OnCollisionEnter2D(Collision2D collision)
     {
         print("Hit");
         
     }
}
