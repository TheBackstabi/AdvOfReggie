using UnityEngine;
using System.Collections;

public class AM_NPCScript : MonoBehaviour {
    public GameObject Reggie;
    [Tooltip("Any projectile the mob will use")]
    public GameObject Arrow;
    public float moveSpeed = 5.0f; // Movement speed
    [Tooltip("Damage dealt to player on attack")]
    public int damageVal = 1; // Damage dealt to player
    [Tooltip("Health to spawn with")]
    public int spawnHealth = 2; // Initial health value
    public float attackRange = 3.0f; // From how far can they hit
    [Tooltip("Can't move nor attack")]
    public bool IsShielded = false; // Enable for ShieldBro; no attacking, no moving
    [Tooltip("Can't move, disables melee")]
    public bool isRanged = false; // Enable for ranged mobs; no moving
    [Tooltip("Is the mob on screen and able to do stuff")]
    public bool isActive = true;

    private int currHealth;
    private float moveDir, prevDir;
    private AudioSource[] audioSources;
    private Collider2D lastPlatform = null;
    private bool isAttacking = false, isAlive = true;

	// Use this for initialization
	void Start () {
        currHealth = spawnHealth;
        prevDir = 0;
        audioSources = GetComponents<AudioSource>();
        ResetFacing();
   	}

    void Update()
    {
        if (!isAlive && audioSources[1].isPlaying == false)
            Destroy(gameObject);
        else if (currHealth <= 0 && isActive) // If I'm dead
        {
            if (!isRanged)
                GetComponent<Animator>().SetBool("IsMoving", false);
            GetComponent<Animator>().SetBool("IsInRange", false);
            isActive = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Animator>().SetBool("Dead", true);
        }
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isActive) // Only do stuff when active
        {
            // Trigger gravity if I'm in the air
            if (lastPlatform == null || !GetComponent<BoxCollider2D>().IsTouching(lastPlatform))
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }

            // If I'm on the groundand not attacking, I can move
            if (GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                if (Reggie.transform.position.x >= transform.position.x) // Adjust direction based on player loc
                {
                    moveDir = 1;
                }
                else if (Reggie.transform.position.x <= transform.position.x)
                {
                    moveDir = -1;
                }
                if (prevDir != moveDir && prevDir != 0)
                {
                    Vector3 x = transform.localScale;
                    x.x = -x.x;
                    transform.localScale = x;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
                }
                prevDir = moveDir;
            }
            
            if (!IsShielded) // ShieldBros don't attack or move
            {
                if (Vector3.Distance(Reggie.transform.position, transform.position) > attackRange && !isAttacking)
                {
                    // We're not in attack range
                    
                    GetComponent<Animator>().SetBool("IsInRange", false);

                    if (!isRanged) // Ranged don't move left/right
                    {
                        GetComponent<Animator>().SetBool("IsMoving", true);
                        isAttacking = true;
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
                    {
                        GetComponent<Animator>().SetBool("IsMoving", false);
                    }
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GetComponent<Animator>().SetBool("IsInRange", true);
                }
            }

        }
	}

    void MeleeAttack() // Used by animator
    {
        if (Vector3.Distance(Reggie.transform.position, transform.position) <= attackRange)
        {
            if (Reggie.transform.position.x <= transform.position.x && moveDir == -1)
            {
                if (Reggie.GetComponent<PlayerStats>().isCrouched)
                    Reggie.GetComponent<PlayerStats>().hitcount -= (damageVal / 2);
                else
                    Reggie.GetComponent<PlayerStats>().hitcount -= damageVal;
                audioSources[2].Play();
            }
            else if (Reggie.transform.position.x >= transform.position.x && moveDir == 1)
            {
                if (Reggie.GetComponent<PlayerStats>().isCrouched)
                    Reggie.GetComponent<PlayerStats>().hitcount -= (damageVal / 2);
                else
                    Reggie.GetComponent<PlayerStats>().hitcount -= damageVal;
                Reggie.GetComponent<PlayerStats>().hitcount -= damageVal;
                audioSources[2].Play();
            }
        }
        else
        {
            audioSources[3].Play();
        }
        isAttacking = false;
    }

    void PrepRangedSound() // Used by animator; fire arrow sfx is borked
    {
        audioSources[2].Play();
    }

    void FireRanged() // Used by animator
    {
        GameObject newArrow = Instantiate(Arrow, transform.position, transform.rotation) as GameObject;
        newArrow.GetComponent<AM_ArrowScript>().target = Reggie.transform;
        newArrow.GetComponent<AM_ArrowScript>().SetDamageVal(damageVal);
        newArrow.tag = "EnemyArrow";
    }

    void Dying() // Used by animator
    {
        isActive = false;
        audioSources[1].Play();
        GetComponent<Animator>().SetBool("Dead", false);
    }

    void Dead() // Used by animator
    {
        isAlive = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PlayerWeapon")
        {
            currHealth -= (int)coll.gameObject.GetComponent<WeaponStats>().damage;
		
    		audioSources[0].Play();
        }
        if (coll.gameObject.tag == "PlayerArrow")
        {
            currHealth -= coll.gameObject.GetComponent<AM_ArrowScript>().GetDamageVal();
            audioSources[0].Play();
        }
        else if (coll.gameObject.tag == "Platform")
        {
            Vector2 vel = GetComponent<Rigidbody2D>().velocity;
            vel.y = 0;
            GetComponent<Rigidbody2D>().velocity = vel;
            lastPlatform = coll;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            Vector2 vel = GetComponent<Rigidbody2D>().velocity;
            vel.y = 0;
            GetComponent<Rigidbody2D>().velocity = vel;
            lastPlatform = other;
            GetComponent<Rigidbody2D>().gravityScale = 0;
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

    public void ResetFacing()
    {
        if (Reggie.transform.position.x > transform.position.x)
        {
            moveDir = 1;
			prevDir = -1;
        }
        else
        {
            moveDir = -1;
        }
    }

    void ProcessOfDying() // Couldn't think of a better name
    {
        transform.Translate(0.0f, -1.0f, 0.0f);
    }

    public void TakeDamage(int _damage)
    {
        currHealth -= _damage;
    }

    public int GetHealth()
    {
        return currHealth;
    }
}
