﻿using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	// Use this for initialization

    public int hitcount = 3;
    public int availstamina = 100;
    public GameObject player;
    private BoxCollider2D collision;
    private SpriteRenderer Avatar;
    public Sprite Idle;
    public Sprite Crouching;
    public Sprite Walking;
    public Sprite Jumping;
    public Sprite MeleeWeapon;
    public Sprite RangedWeapon;
    private bool WeaponSelected = false;
    private Transform PlayerLocation;
    public GameObject Camera;
    public GameObject GroundCheck;
    public GameObject Ground;
    public GameObject Weapon;


	void Start () 
    {
       
        collision = player.GetComponent<BoxCollider2D>();
        Avatar = player.GetComponent<SpriteRenderer>();
        PlayerLocation = player.transform;
        //Weapon.GetComponent<SpriteRenderer>().sprite = MeleeWeapon;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (collision.IsTouchingLayers(14))
        {
            hitcount--;
        }

        if(transform.position.x <= Camera.transform.position.x - 2.9)
        {
            transform.position = new Vector3(Camera.transform.position.x - 2.9f, transform.position.y, 0.0f);
        }

        
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Avatar.sprite = Crouching;
        }
        else if (Input.GetKey(KeyCode.A)) //&& GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()))
        {
            //Avatar.sprite = Walking;
            //player.transform.localScale = new Vector3(-1.0f *player.transform.localScale.x , player.transform.localScale.y, player.transform.localScale.z);
           // PlayerLocation.Translate(-0.1f, 0.0f, 0.0f);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-4.2f, player.GetComponent<Rigidbody2D>().velocity.y);

        }
        else if (Input.GetKey(KeyCode.D)) //&& GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()))
        {
            //Avatar.sprite = Walking;
            //PlayerLocation.Translate(0.1f, 0.0f, 0.0f);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(4.2f, player.GetComponent<Rigidbody2D>().velocity.y);

        }
        else if (Input.GetKey(KeyCode.Tab))
        {
            CancelInvoke();
            if (WeaponSelected)
                player.GetComponentInChildren<SpriteRenderer>().sprite = RangedWeapon;
            else
                player.GetComponentInChildren<SpriteRenderer>().sprite = MeleeWeapon;

            WeaponSelected = !WeaponSelected;
            availstamina -= 10;
        }
        else
        {
            Avatar.sprite = Idle;
            Invoke("StaminaRegen", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.W) && GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()) && GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            //Avatar.sprite = Jumping;
            CancelInvoke();
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            availstamina -= 15;
            
        }
    }

    void StaminaRegen()
    {
        if (availstamina < 100)
            availstamina++;
        else
            availstamina = 100;
    }
}
