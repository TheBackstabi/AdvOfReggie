using UnityEngine;
using System.Collections;

public class AM_MobSpawnerScript : MonoBehaviour {
    public GameObject enemyType; // What mob to spawn
    public float delay; // How long before spawning begins
    public float interval = 5.0f; // How frequently to spawn
    public int numToSpawn = 1; // Max number of enemies to spawn
    public bool isEnabled = false;
    [Tooltip("Enable to spawn mobs randomly Left/Right of the spawner")]
    public bool isRandom = false;
    public float randomRange = 1.5f;

    private float sinceLastSpawn;
	// Use this for initialization
	void Start () {
        sinceLastSpawn = 0;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            if (numToSpawn > 0)
            {
                if (delay < 0)
                {
                    sinceLastSpawn += Time.deltaTime;
                    if (sinceLastSpawn >= interval)
                    {
                        numToSpawn--;
                        sinceLastSpawn = 0;
                        GameObject newEnemy = Instantiate(enemyType, transform.position, transform.rotation) as GameObject;
                        if (isRandom)
                        {
                            Vector3 newPos = newEnemy.transform.position;
                            newPos.x += Random.Range(-randomRange, randomRange);
                            newEnemy.transform.position = newPos;
                        }
                        newEnemy.GetComponent<AM_NPCScript>().moveSpeed += Random.Range(-.3f, .3f);
                        newEnemy.GetComponent<AM_NPCScript>().attackRange += Random.Range(-.15f, .15f);
                        newEnemy.GetComponent<AM_NPCScript>().isActive = true;
                        newEnemy.GetComponent<AM_NPCScript>().ResetFacing();
                        newEnemy.GetComponent<Rigidbody2D>().gravityScale = 1;
                    }
                }
                else
                {
                    delay -= Time.deltaTime;
                    if (delay <= 0) // Spawn as soon as delay hits 0
                    {
                        numToSpawn--;
                        GameObject newEnemy = Instantiate(enemyType, transform.position, transform.rotation) as GameObject;
                        if (isRandom)
                        {
                            Vector3 newPos = newEnemy.transform.position;
                            newPos.x += Random.Range(-randomRange, randomRange);
                            newEnemy.transform.position = newPos;
                        }
                        newEnemy.GetComponent<AM_NPCScript>().moveSpeed += Random.Range(-.3f, .3f);
                        newEnemy.GetComponent<AM_NPCScript>().attackRange += Random.Range(-.15f, .15f);
                        newEnemy.GetComponent<AM_NPCScript>().isActive = true;
                        newEnemy.GetComponent<AM_NPCScript>().ResetFacing();
                        newEnemy.GetComponent<Rigidbody2D>().gravityScale = 1;
                    }
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        isEnabled = false;
    }

    void OnBecameVisible()
    {
        isEnabled = true;
    }
}
