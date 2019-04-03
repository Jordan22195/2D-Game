using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    /// <summary>
    /// /this is tied to the camera
    /// </summary>
    public Vector3 mousePosition;
    public float moveSpeed = 0.01f;
    public GameObject player;
    public GameObject Fireball;
    public GameObject Border;

	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
        if (!SceneGlobals.pause)
        {
            Walk();
        }
	}
    void LateUpdate()
    {
        
        
    }

    void Walk()
    {

        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }


        transform.position = pos;
        Vector3 newPos = transform.position;
        newPos.z = player.transform.position.z;
        player.transform.position = newPos;

        //if (Input.GetMouseButton(0))
        //{
        //    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    mousePosition.z = transform.position.z;
        //    transform.position = Vector3.MoveTowards(transform.position, mousePosition, moveSpeed);
        //}

    }
}
