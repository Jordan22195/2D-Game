using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : MonoBehaviour {

    public GameObject me;
    public Vector3 mousePosition;
    public float moveSpeed = 0.01f;
    private Vector3 targetPos;
    private Vector3 targetRotatePos;
    float lifeLength;


    public static int damage
    {
        get
        {
            return Player.intell;
        }
    }

    // Use this for initialization
    void Awake () {

        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        targetPos.z = transform.position.z;
        targetRotatePos = targetPos;
        targetRotatePos.x = targetRotatePos.x - transform.position.x;
        targetRotatePos.y = targetRotatePos.y - transform.position.y;
        float distance = Vector3.Distance(targetPos, transform.position);
        float scale = 100;

        float angle = Mathf.Atan2(targetRotatePos.y, targetRotatePos.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        targetPos.x += scale * targetRotatePos.x;
        targetPos.y += scale * targetRotatePos.y;
    }
	
	// Update is called once per frame
	void Update () {


        if (!SceneGlobals.pause)
        {
            lifeLength += Time.deltaTime;
            if (lifeLength > 1) Destroy(this.gameObject);
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }



    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
