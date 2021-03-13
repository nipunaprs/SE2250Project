using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementNew : MonoBehaviour
{

    //VARIABLES

    public float speed = 5;
    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);


        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;


        if (Input.GetKey("up"))
        {
            anim.SetBool("isrunningup", true);
        }
        else
        {
            anim.SetBool("isrunningup", false);
        }

        if (Input.GetKey("down"))
        {
            anim.SetBool("isrunningforward", true);
        }
        else
        {
            anim.SetBool("isrunningforward", false);
        }

        if (Input.GetKey("right"))
        {
            anim.SetBool("isrunningright", true);
        }
        else
        {
            anim.SetBool("isrunningright", false);
        }

        if (Input.GetKey("left"))
        {
            anim.SetBool("isrunningleft", true);
        }
        else
        {
            anim.SetBool("isrunningleft", false);
        }






    }
}
