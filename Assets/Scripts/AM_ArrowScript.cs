using UnityEngine;
using System.Collections;

public class AM_ArrowScript : MonoBehaviour {
    public float fireStrength = 100.0f;
    public Transform target = null;

    private int currentDamage = 0;

	// Use this for initialization
	void Start () {
        if (gameObject.tag == "PlayerWeapon")
        {
            // Player stuff
        }
        else
        {
            if (target != null)
            {
                //if (target.position.x > transform.position.x + 2)
                //    transform.Rotate(0.0f, 0.0f, -90.0f);
                //else if (target.position.x < transform.position.x - 2)
                //    transform.Rotate(0.0f, 0.0f, 90.0f);
                //else if (target.position.y < transform.position.y)
                //    transform.Rotate(0.0f, 0.0f, 180.0f);

                if (target.position.x > transform.position.x)
                    transform.Rotate(new Vector3(0f, 0f, 1f), -Vector3.Angle(transform.position, -target.position));
                else
                    transform.Rotate(new Vector3(0f, 0f, 1f), Vector3.Angle(target.position, transform.position));
                GetComponent<Rigidbody2D>().AddForce(fireStrength * Vector3.Normalize((target.position - transform.position)));
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<SpriteRenderer>().enabled == false && GetComponent<AudioSource>().isPlaying == false)
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "EnemyArrow" && other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            other.GetComponent<PlayerStats>().hitcount--;
        }
        else if (gameObject.tag == "PlayerWeapon" && other.gameObject.tag == "Enemy")
        {
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            other.GetComponent<AM_NPCScript>().TakeDamage(currentDamage);
        }
        else if (gameObject.tag == "EnemyArrow" && other.gameObject.tag == "PlayerWeapon")
        {
            // New audio source here
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SetDamageVal(int _damage)
    {
        currentDamage = _damage;
    }

    public int GetDamageVal()
    {
        return currentDamage;
    }
}
