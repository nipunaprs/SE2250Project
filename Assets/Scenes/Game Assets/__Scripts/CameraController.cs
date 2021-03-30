using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
public GameObject player; 
   private Vector3 offset;

    

    // Start is called before the first frame update
    void Start()
    {
        offset=transform.position - player.transform.position;

        
    }

    void Update()
    {
        //Restart the game when R is pressed and when the death screen is playing
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
