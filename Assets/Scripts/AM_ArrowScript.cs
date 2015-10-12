using UnityEngine;
using System.Collections;

public class AM_ArrowScript : MonoBehaviour {
    public float fireStrength = 100.0f;
    public Transform target = null;

    private int currentDamage = 0;
	// Use this for initialization
	void Start () {
        if (tag == "PlayerWeapon")
        {
            // Player stuff
        }
        else
        {
            if (target != null)
            {
                if (target.position.x > transform.position.x + 2)
                    transform.Rotate(0.0f, 0.0f, -90.0f);
                else if (target.position.x < transform.position.x - 2)
                    transform.Rotate(0.0f, 0.0f, 90.0f);
                else if (target.position.y < transform.position.y)
                    transform.Rotate(0.0f, 0.0f, 180.0f);
                GetComponent<Rigidbody2D>().AddForce(fireStrength * (target.position - transform.position));
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Nothing to put here
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "EnemyArrow" && other.gameObject.tag == "Player")
            GetComponent<AudioSource>().Play();
        else if (gameObject.tag == "PlayerWeapon" && other.gameObject.tag == "Enemy")
            GetComponent<AudioSource>().Play();
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
