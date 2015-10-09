using UnityEngine;
using System.Collections;

public class AM_NPCScript : MonoBehaviour {
    public GameObject Reggie;
    public GameObject Weapon;
    public float moveSpeed = 5.0f; // Movement speed
    public int damageVal = 1; // Damage dealt to player
    public float attackSpeed = 2.0f; // Rate of attack
    public int spawnHealth = 2; // Initial health value
    public float attackRange = 3.0f; // From how far can they hit
    public float aggroRange = 10.0f; // When will they move towards the player
    public bool IsShielded = false; // Enable for ShieldBro

    private float currHealth;
    private float moveDir;
    private float timeSinceLastAttack;
	// Use this for initialization
	void Start () {
        currHealth = spawnHealth;
   	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Vector3.Distance(Reggie.transform.position, transform.position) > attackRange && Vector3.Distance(Reggie.transform.position, transform.position) <= aggroRange)
        {
            // We're not in attack range but within aggro range.

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
        else
        {
            // We're in range, attack.

            if (timeSinceLastAttack < attackSpeed)
                timeSinceLastAttack += Time.deltaTime;
            else
            {
                timeSinceLastAttack = 0;

                // Remove once animation keyframes are done.
                Attack();
            }
        }
	}

    void Attack()
    {
        Reggie.GetComponent<PlayerStats>().hitcount -= (int)damageVal;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        
    }
}
