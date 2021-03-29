using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject hud;
    public GameObject hero;

    [Header ("Set Dynamically")]
    public GameObject h;
    // Start is called before the first frame update
    void Awake()
    {
        h = Instantiate<GameObject>(hud);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.zero;
        pos.x = (hero.transform.position.x - 200);
        pos.y = (hero.transform.position.y + 200);
        h.transform.position = pos;
    }
}
