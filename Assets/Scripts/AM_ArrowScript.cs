﻿using UnityEngine;
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
            GetComponent<Rigidbody2D>().AddForce(fireStrength * Vector3.Normalize(Input.mousePosition));
        }
        else
        {
            

            if (target != null)
            {
                if (target.position.x > transform.position.x)
                    transform.Rotate(0.0f, 0.0f, -90.0f);
                else
                    transform.Rotate(0.0f, 0.0f, 90.0f);

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
            other.GetComponent<Animator>().SetTrigger("hurt");
        }
        else if (gameObject.tag == "PlayerWeapon" && other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<AM_NPCScript>().isBoss)
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
