using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {


    List<GameObject> objectPool = new List<GameObject>();
    List<GameObject> usedObjects = new List<GameObject>();
    public GameObject player;
    public GameObject q1Grass;
    public GameObject q2Grass;
    public GameObject q3Grass;
    public GameObject q4Grass;
    public GameObject temp;
    public GameObject spawnArea;

    public GameObject enemy;

    public static List<GameObject> enemies = new List<GameObject>();
    public static int spawnCount = 0;
    public int maxSpawnCount = 50;
    public int spawnRadius;
  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!SceneGlobals.pause)
        {
            updateGround();
            if (spawnCount < maxSpawnCount) spawnEnemy();
            updateRange();
        }
  
	}

    void spawnEnemy()
    {
        Vector3 spawnLocation;
        float r =  Random.Range(0.0f, spawnRadius * spawnRadius);
        float theta = Random.Range(0.0f, 1.0f) * 2 * Mathf.PI;
        spawnLocation.x = Mathf.Sqrt(r) * Mathf.Cos(theta) + this.transform.position.x;
        spawnLocation.y = Mathf.Sqrt(r) * Mathf.Sin(theta) + this.transform.position.y;
        spawnLocation.z = 1;
        GameObject slimeClone = Instantiate(enemy, spawnLocation, transform.rotation);
        spawnCount++;
        enemies.Add(slimeClone);
        Debug.Log("slime spawn");
    }

    void updateGround()
    {
        //move up (q3 + q4)
        if (player.transform.position.y > q1Grass.transform.position.y)
        {
            Vector3 newq1Location = q1Grass.transform.position;
            Vector3 newq2Location = q2Grass.transform.position;
            newq1Location.y += 12;
            newq2Location.y += 12;
            q3Grass.transform.position = newq1Location;
            q4Grass.transform.position = newq2Location;
            quadrantSwap(ref q1Grass,ref q3Grass);
            quadrantSwap(ref q2Grass, ref q4Grass);
        }
        //move down (q1 + q2)
        if (player.transform.position.y < q3Grass.transform.position.y)
        {
            Vector3 newq3Location = q3Grass.transform.position;
            Vector3 newq4Location = q4Grass.transform.position;
            newq3Location.y -= 12;
            newq4Location.y -= 12;
            q1Grass.transform.position = newq3Location;
            q2Grass.transform.position = newq4Location;
            quadrantSwap(ref q1Grass, ref q3Grass);
            quadrantSwap(ref q2Grass, ref q4Grass);
        }
        //move left (q2 + q4)
        if (player.transform.position.x < q3Grass.transform.position.x)
        {
            Vector3 newq1Location = q1Grass.transform.position;
            Vector3 newq3Location = q3Grass.transform.position;
            newq1Location.x -= 26;
            newq3Location.x -= 26;
            q2Grass.transform.position = newq1Location;
            q4Grass.transform.position = newq3Location;
            quadrantSwap(ref q1Grass, ref q2Grass);
            quadrantSwap(ref q3Grass, ref q4Grass);
        }
        //move right (q1 + q3)
        if (player.transform.position.x > q4Grass.transform.position.x)
        {
            Vector3 newq2Location = q2Grass.transform.position;
            Vector3 newq4Location = q4Grass.transform.position;
            newq2Location.x += 26;
            newq4Location.x += 26;
            q1Grass.transform.position = newq2Location;
            q3Grass.transform.position = newq4Location;
            quadrantSwap(ref q1Grass, ref q2Grass);
            quadrantSwap(ref q3Grass, ref q4Grass);
        }
    }

    void quadrantSwap (ref GameObject a, ref GameObject b)
    {
        temp = a;
        a = b;
        b = temp;
    }

    void updateRange()
    {
        float d;
        foreach( GameObject o in enemies)
        {
            d = Vector3.Distance(o.transform.position, this.transform.position);
            if (d > 1.1 * spawnRadius) Destroy(o);
            else
            {
                Enemy e = o.GetComponent<Enemy>();
                e.UpdateRangeFromPlayer(d);
            }
        }
    }
}
