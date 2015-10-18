using UnityEngine;
using System.Collections;

public class AM_IceBossScript : MonoBehaviour {
    public float fireRate = 5.0f;
    public int arrowDamageVal = 1;
    public GameObject Reticule;
    public GameObject blockImage;
    public GameObject stunnedImage;
    public float stunDuration = 5.0f;

    private float timeSinceLastArrow = 0; // Counter to place arrow target and fire arrow
    private float stunTimer = 0;
    private AM_NPCScript baseScript; // Base NPC script, set on Start()
    private bool arrowSet = false, isStunned = false; // arrowSet: Is the arrow reticule up. isStunned: Am I able to act
    private Sprite defaultSprite;

	// Use this for initialization
	void Start () {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        baseScript = GetComponent<AM_NPCScript>();
        baseScript.ToggleImmunity(true);
        blockImage.GetComponent<SpriteRenderer>().enabled = true;
        Reticule.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (baseScript.GetHealth() <= 0) // Remove reticule on death
        {
            Reticule.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (baseScript.isActive && !baseScript.IsStunned()) // If I'm not iceblocked, I can do stuff.
        {

            timeSinceLastArrow += Time.deltaTime;
            if (timeSinceLastArrow >= fireRate && arrowSet) // Target has been placed, fire...
            {
                arrowSet = false;
                GameObject newArrow = Instantiate(baseScript.Arrow, transform.position, transform.rotation) as GameObject;
                newArrow.GetComponent<AM_ArrowScript>().SetDamageVal(arrowDamageVal);
                newArrow.tag = "EnemyArrow";
                newArrow.GetComponent<AM_ArrowScript>().target = Reticule.transform;
                timeSinceLastArrow = 0;
                Reticule.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (timeSinceLastArrow >= fireRate - 3 && !arrowSet) // Otherwise, place the target
            {
                arrowSet = true;
                Reticule.GetComponent<SpriteRenderer>().enabled = true;
                Vector3 pos = baseScript.Reggie.transform.position;
                pos.y += 1;
                Reticule.transform.position = pos;
            }
        }
        else if (baseScript.isActive) // If I am stunned
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunDuration) // Time to wake up
            {
                stunTimer = 0;
                baseScript.ToggleImmunity(true);
                baseScript.ToggleStun(false);
                stunnedImage.GetComponent<SpriteRenderer>().enabled = false;
                blockImage.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other) // Handle getting hit
    {
        if (!baseScript.GetImmunity())
        {
            if (other.gameObject.tag == "PlayerWeapon") // Melee
            {
                baseScript.TakeDamage((int)other.GetComponent<WeaponStats>().damage);
                GetComponents<AudioSource>()[0].Play();
            }
            else if (other.tag == "PlayerArrow") // Ranged
            {
                baseScript.TakeDamage(other.GetComponent<AM_ArrowScript>().GetDamageVal());
                GetComponents<AudioSource>()[0].Play();
            }
        }
        else if (other.gameObject.tag == "PlayerWeapon") { }
            // Shield audio?
    }

    public void HitByCrystal()
    {
        baseScript.ToggleImmunity(false);
        baseScript.ToggleStun(true);
        stunnedImage.GetComponent<SpriteRenderer>().enabled = true;
        blockImage.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
