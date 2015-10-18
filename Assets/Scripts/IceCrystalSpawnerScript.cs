using UnityEngine;
using System.Collections;

public class IceCrystalSpawnerScript : MonoBehaviour
{
    public GameObject Reggie;
    public GameObject IceBoss;
    public GameObject enemyType; // What mob to spawn
    public float interval = 5.0f; // How frequently to spawn
    public bool isEnabled = true;

    private float sinceLastSpawn;
    // Use this for initialization
    void Start()
    {
        sinceLastSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            sinceLastSpawn += Time.deltaTime;
            if (sinceLastSpawn >= interval)
            {
                Vector3 pos = transform.position;
                pos.x = Reggie.transform.position.x;
                transform.position = pos;
                sinceLastSpawn = 0;
                GameObject newEnemy = Instantiate(enemyType, transform.position, transform.rotation) as GameObject;
                newEnemy.GetComponent<IceCrystalScript>().SetUp(Reggie, IceBoss);
                newEnemy.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
        else if (GetComponent<AudioSource>().isPlaying == false)
            Destroy(gameObject);
    }
}
