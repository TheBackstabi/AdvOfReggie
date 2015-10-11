using UnityEngine;
using System.Collections;

public class AM_MobSpawnerScript : MonoBehaviour {
    public GameObject enemyType; // What mob to spawn
    public float delay; // How long before spawning begins
    public float interval = 5.0f; // How frequently to spawn
    public int numToSpawn = 1; // Max number of enemies to spawn

    private float sinceLastSpawn;
	// Use this for initialization
	void Start () {
        sinceLastSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (numToSpawn > 0)
        {
            if (delay <= 0)
            {
                sinceLastSpawn += Time.deltaTime;
                if (sinceLastSpawn >= interval)
                {
                    numToSpawn--;
                    sinceLastSpawn = 0;
                    GameObject newEnemy = Instantiate(enemyType, transform.position, transform.rotation) as GameObject;
                }
            }
            else
            {
                delay -= Time.deltaTime;
                if (delay <= 0) // Spawn as soon as delay hits 0
                {
                    numToSpawn--;
                    GameObject newEnemy = Instantiate(enemyType, transform.position, transform.rotation) as GameObject;
                }
            }
        }
	}
}
