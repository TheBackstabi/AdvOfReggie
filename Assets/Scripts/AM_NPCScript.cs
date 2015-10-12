using UnityEngine;
using System.Collections;

public class AM_NPCScript : MonoBehaviour {
    public GameObject Reggie;
    public GameObject Arrow;
    public float moveSpeed = 5.0f; // Movement speed
    public int damageVal = 1; // Damage dealt to player
    public int spawnHealth = 2; // Initial health value
    public float attackRange = 3.0f; // From how far can they hit
    public bool IsShielded = false; // Enable for ShieldBro; no attacking, no moving
    public bool isRanged = false; // Enable for ranged mobs; no moving
    public bool isActive = true;

    private float currHealth;
    private float moveDir, prevDir;
    private AudioSource[] audioSources;
	// Use this for initialization
	void Start () {
        currHealth = spawnHealth;
        prevDir = 0;
        audioSources = GetComponents<AudioSource>();
   	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isActive) // Only do stuff when active
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            if (currHealth <= 0) // If I'm dead
            {
                // Replace with isDead animator bool = true

                Dying();
                Dead();
            }

            if (Reggie.transform.position.x > transform.position.x) // Adjust direction based on player loc
            {
                moveDir = 1;
            }
            else
            {
                moveDir = -1;
            }
            if (prevDir != moveDir && prevDir != 0)
            {
                Vector3 x = transform.localScale;
                x.x = -x.x;
                transform.localScale = x;
            }
            prevDir = moveDir;

            if (!IsShielded) // ShieldBros don't attack or move
            {
                if (Vector3.Distance(Reggie.transform.position, transform.position) > attackRange)
                {
                    // We're not in attack range
                    
                    GetComponent<Animator>().SetBool("IsInRange", false);

                    if (!isRanged) // Ranged don't move left/right
                    {
                        GetComponent<Animator>().SetBool("IsMoving", true);
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
                else if (Vector3.Distance(Reggie.transform.position, transform.position) <= attackRange)
                {
                    // We're in range, attack.
                    if (!isRanged)
                        GetComponent<Animator>().SetBool("IsMoving", false);
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GetComponent<Animator>().SetBool("IsInRange", true);
                }
            }
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
	}

    void MeleeAttack() // Used by animator
    {
        Reggie.GetComponent<PlayerStats>().hitcount -= damageVal;
        audioSources[2].Play();
    }

    void PrepRangedSound() // Used by animator; fire arrow sfx is borked
    {
        audioSources[2].Play();
    }

    void FireRanged() // Used by animator
    {
        GameObject newArrow = Instantiate(Arrow, transform.position, transform.rotation) as GameObject;
        newArrow.GetComponent<AM_ArrowScript>().target = Reggie.transform;
        newArrow.tag = "EnemyWeapon";
    }

    void Dying() // Used by animator
    {
        isActive = false;
        audioSources[1].Play();
    }

    void Dead() // Used by animator
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PlayerWeapon")
        {
            // Uncomment once PlayerWeaponScript is done.
            //currHealth -= coll.gameObject.GetComponent<PlayerWeaponScript>().currentDamage;
            currHealth -= 1;

            audioSources[0].Play();
        }
    }

    void OnBecameInvisible()
    {
        isActive = false;
    }

    void OnBecameVisibile()
    {
        isActive = true;
    }
}
