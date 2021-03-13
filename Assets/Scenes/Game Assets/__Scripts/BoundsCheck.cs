using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radiusx = 1f;
    public float radiusy = 1f;

    [Header ("Set Dynamically")]
    public float camWidth;
    public float camHeight;
    // Start is called before the first frame update
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        if(pos.x > camWidth - radiusx){
            pos.x = camWidth - radiusx;
        }
        if(pos.x < -camWidth + radiusx){
            pos.x = -camWidth + radiusx;
        }
        if(pos.y > camHeight - radiusy){
            pos.y = camHeight - radiusy;
        }
        if(pos.y < -camHeight + radiusy){
            pos.y = -camHeight + radiusy;
        }

        transform.position = pos;
    }

    void OnDrawGizmos(){
        if(!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth*2, camHeight*2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }

}
