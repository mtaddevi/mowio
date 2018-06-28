using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    // LateUpdate()
   // {
//transform.position = player.transform.position + offset;

   // }

   // private void FixedUpdate()
    //{
       // transform.position = player.transform.position + offset;
        
   // }

}
