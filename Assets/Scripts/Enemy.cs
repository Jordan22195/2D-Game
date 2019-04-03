using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float patrolRadius = 3f;
    private Vector3 patrolDesitination;
    private Vector3 spawnPos;
    private float moveSpeed = .05f;
    private float rangeFromPlayer;
    public int maxHealth = 10;
    private int currentHealth;
    public int attack;
    private int xpValue = 10;
    public bool canMove;
    private bool moving = false;
    private GameObject greenBar;
	// Use this for initialization
	void Start () {
        spawnPos = transform.position;
        currentHealth = maxHealth;
        Component[] comps = this.GetComponentsInChildren<SpriteRenderer>();
        foreach( Component c in comps)
        {
            if (c.name == "Green") greenBar = c.gameObject;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!SceneGlobals.pause)
        {
            if (canMove) Patrol();
            CheckDeath();
        }
	}

    void Patrol()
    {
        if(!moving)
        {
            patrolDesitination.x = spawnPos.x + Random.Range(0f, patrolRadius);
            patrolDesitination.y = spawnPos.y + Random.Range(0f, patrolRadius);
            moving = true;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolDesitination, moveSpeed);
            if (transform.position == patrolDesitination) moving = false;
        }
    }

    void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("monster died");
            Player.currentXp += xpValue;
            Destroy(this.gameObject);
        }
    }

    public void UpdateRangeFromPlayer(float r)
    {
        this.rangeFromPlayer = r;
        //Debug.Log("range from player: " + r.ToString());
    }


    void OnCollisionEnter2D(Collision2D col)
    {
       
        if (col.gameObject.tag == "Fireball")
        {
            Destroy(col.gameObject);
            this.currentHealth -= Fireball.damage;
            Debug.Log("Monster health: " + this.currentHealth.ToString());
            updateHealthBar();
        }
    }

    private void OnDestroy()
    {
        SpawnController.enemies.Remove(this.gameObject);
        SpawnController.spawnCount--;
    }

    private void updateHealthBar()
    {
        float p = (float)currentHealth / (float)maxHealth;
        greenBar.transform.localScale = new Vector3(p, 1, 1);
        greenBar.transform.localPosition = new Vector3(p-1, 0, 0);
    }


}
