using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1f; 
    public bool keepOnScreen = true; 


    [Header("Set Dynamically")]
    public bool isOnscreen = true; 
    public float camWidth; 
    public float camHeight; 

    [HideInInspector]

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
