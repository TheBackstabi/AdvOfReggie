using UnityEngine;
using System.Collections;

public class IceCrystalScript : MonoBehaviour {
    private GameObject Reggie;
    private GameObject IceBoss;
    private bool isEnabled = true;
    private bool canHitPlayer = true, hitByPlayer = false;
    private bool touchedGround = false;
    private float timeOnGround = 0;

	// Use this for initialization
	void Start () {
	
	}

    public void SetUp(GameObject _R, GameObject _B)
    {
        Reggie = _R;
        IceBoss = _B;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isEnabled && !GetComponent<AudioSource>().isPlaying)
            Destroy(gameObject);
        else if (touchedGround)
        {
            timeOnGround += Time.deltaTime;
            if (timeOnGround >= 8.0f)
                Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == Reggie && canHitPlayer)
        {
            Reggie.GetComponent<PlayerStats>().hitcount--;
            GetComponents<AudioSource>()[0].Play();
            GetComponent<SpriteRenderer>().enabled = false;
            isEnabled = false;
        }
        else if (other.gameObject.tag == "PlayerWeapon")
        {
            GetComponent<Rigidbody2D>().AddForce(1000 * Vector3.Normalize((IceBoss.transform.position - transform.position)));
            hitByPlayer = true;
        }
        else if (other.gameObject.tag == "Platform")
        {
            GetComponents<AudioSource>()[0].Play();
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            canHitPlayer = false;
            touchedGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && hitByPlayer)
        {
            if (other.gameObject.GetComponent<AM_IceBossScript>() != null)
                other.gameObject.GetComponent<AM_IceBossScript>().HitByCrystal();

            other.GetComponent<AM_NPCScript>().TakeDamage(5);
            GetComponent<SpriteRenderer>().enabled = false;
            isEnabled = false;
            GetComponents<AudioSource>()[0].Play();
        }
        else if (other.gameObject.tag == "PlayerWeapon")
        {
            GetComponent<Rigidbody2D>().AddForce(500 * Vector3.Normalize((IceBoss.transform.position - transform.position)));
            hitByPlayer = true;
        }
        else if (other.gameObject == Reggie)
        {
            GetComponents<AudioSource>()[1].Play();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && hitByPlayer && isEnabled)
        {
            if (other.gameObject.GetComponent<AM_IceBossScript>() != null)
                other.gameObject.GetComponent<AM_IceBossScript>().HitByCrystal();

            other.GetComponent<AM_NPCScript>().TakeDamage(5);
            GetComponent<SpriteRenderer>().enabled = false;
            isEnabled = false;
            GetComponents<AudioSource>()[0].Play();
        }
    }
}
