using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //Variables
    private float timestore = 2f;
    private float time;
    private Animator myanim;
    private bool alreadyblew = false;

    // Start is called before the first frame update
    void Start()
    {
        myanim = GetComponent<Animator>();
        time = timestore;
    }

    // Update is called once per frame
    void Update()
    {
        //If time is 0, blow up the bomb
        if (time > 0)
        {
            time -= Time.deltaTime;
            myanim.SetBool("blowup", false);
        }
        else
        {
            //Play blow up after time is 0
            myanim.SetBool("blowup", true);
            alreadyblew = true;
            time = timestore;
        }

        

    }
    //After animation has played, destroy the object
    private void LateUpdate()
    {
        //After animation is done playing and alreadyblew is true, destroy the bomb
        if (this.myanim.GetCurrentAnimatorStateInfo(0).IsTag("Normal") && alreadyblew)
        {
            Destroy(gameObject);
        }
    }


}
