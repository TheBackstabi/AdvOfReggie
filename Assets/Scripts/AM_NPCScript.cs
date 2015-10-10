using UnityEngine;
using System.Collections;

public class AM_NPCScript : MonoBehaviour {
    public GameObject Reggie;
    public float moveSpeed = 5.0f; // Movement speed
    public int damageVal = 1; // Damage dealt to player
    public int spawnHealth = 2; // Initial health value
    public float attackRange = 3.0f; // From how far can they hit
    public float aggroRange = 15.0f; // When will they move towards the player
    public bool IsShielded = false; // Enable for ShieldBro; no attacking, no moving
    public bool isRanged = false; // Enable for ranged mobs; no moving

    private float currHealth;
    private float moveDir;
    private float timeSinceLastAttack;
	// Use this for initialization
	void Start () {
        currHealth = spawnHealth;
   	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gameObject.activeInHierarchy) // Only do stuff when active
        {
            if (!IsShielded) // ShieldBros don't attack or move
            {
                if (Vector3.Distance(Reggie.transform.position, transform.position) > attackRange && Vector3.Distance(Reggie.transform.position, transform.position) <= aggroRange)
                {
                    // We're not in attack range but within aggro range.

                    if (!isRanged) // Ranged mobs dont' move
                    {
                        GetComponent<Animator>().SetBool("IsInRange", false);
                        if (Reggie.transform.position.x > transform.position.x)
                            moveDir = 1;
                        else
                            moveDir = -1;

                        Vector3 currVel = GetComponent<Rigidbody2D>().velocity;
                        if (currVel.y == 0)
                        {
                            // We're on the ground, move into range

                            if (currVel.x < moveSpeed)
                                currVel.x = moveSpeed * moveDir;
                            else
                                currVel.x = currVel.x + moveDir * Time.deltaTime * moveSpeed;
                            GetComponent<Rigidbody2D>().velocity = currVel;
                        }
                    }
                }
                else
                {
                    // We're in range, attack.

                    GetComponent<Animator>().SetBool("IsInRange", true);
                }
            }
        }
	}

    void MeleeAttack()
    {
        Reggie.GetComponent<PlayerStats>().hitcount -= damageVal;
    }

    void FireRanged()
    {
        // Code
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PlayerWeapon")
        {
            // Uncomment once PlayerWeaponScript is done.
            //currHealth -= coll.gameObject.GetComponent<PlayerWeaponScript>().currentDamage;
        }
    }
}
