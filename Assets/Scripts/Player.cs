using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public GameObject Fireball;
    private bool allowFire;
    private float nextFire;
    public static int fireRate = 2;


    public static int maxHealth = 10;
    public static int currentHealth;
    public static int currentXp;
    public static int totalXpForLvl;
    public static int playerLevel;
    public static int intell;
    public static int AbilityPoints;
    public static int spentAP;

  

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        currentXp = 0;
        totalXpForLvl = 50;
        playerLevel = 1;
        intell = 2;
	}

    // Update is called once per frame
    private void Update () {

        if (!SceneGlobals.pause)
        {
            Shoot();
            checkLevelUp();
        }
	}

    void Shoot()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1) && Time.time > nextFire)
        { 
            GameObject projectileCone = Instantiate(Fireball, transform.position, transform.rotation);
            nextFire = Time.time + (float)(1/(float)fireRate);
        }
    }

    void checkLevelUp()
    {
        if (currentXp >= totalXpForLvl)
        {
            playerLevel++;
            currentXp = currentXp - totalXpForLvl;
            totalXpForLvl *= 2;
            AbilityPoints += 5;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            currentHealth -= col.gameObject.GetComponent<Enemy>().attack;
        }
    }
}
