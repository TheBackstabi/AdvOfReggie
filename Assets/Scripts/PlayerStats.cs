using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	// Use this for initialization

    public int hitcount = 3;
    public float availstamina = 100;
    public GameObject player;
    private BoxCollider2D collision;
    private SpriteRenderer Avatar;
    public Sprite Idle;
    public Sprite Crouching;
    public Sprite Walking;
    public Sprite Jumping;
    public Sprite Attacking;
    public GameObject MeleeWeapon;
    public GameObject RangedWeapon;
    private bool WeaponSelected = false;
    public GameObject Camera1;
    public GameObject GroundCheck;
    public GameObject Ground;
    public GameObject Weapon;
    public bool canJump = true;
    public bool isCrouched = false;
    public bool Facingleft = false;
    public int deathtimer = 10;
    public GameObject ArrowType;


	void Start () 
    {
        MeleeWeapon = GameObject.Find("Katana");
        RangedWeapon = GameObject.Find("Bow");
        RangedWeapon.SetActive(false);
        collision = player.GetComponent<BoxCollider2D>();
        Avatar = player.GetComponent<SpriteRenderer>();
		Weapon = MeleeWeapon;
        //Weapon.GetComponent<SpriteRenderer>().sprite = MeleeWeapon;\
        //Camera.main.orthograpicSize = 4.2f;
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (collision.IsTouchingLayers(14))
        {
            hitcount--;
        }

        if(transform.position.x <= Camera1.transform.position.x - 7.2f)
        {
            transform.position = new Vector3(Camera1.transform.position.x - 7.2f, transform.position.y, 0.0f);
        }

        
        
	}

    void FixedUpdate()
    {
        if (MeleeWeapon.GetComponent<BoxCollider2D>().enabled)
        {
            MeleeWeapon.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //GetComponent<Animator>().SetBool("crouch", true);
            Avatar.sprite = Crouching;
            if (!isCrouched)
            { 
                //Weapon.transform.position = new Vector3(Weapon.transform.position.x, Weapon.transform.position.y - 0.1f, Weapon.transform.position.z);
                isCrouched = true;
            }

        }
        else if (Input.GetKey(KeyCode.A) && !isCrouched) //&& GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()))
        {
            //GetComponent<Animator>().SetBool("Walking", true);
            //Avatar.sprite = Walking;
            //player.transform.localScale = new Vector3(-1.0f *player.transform.localScale.x , player.transform.localScale.y, player.transform.localScale.z);
           // PlayerLocation.Translate(-0.1f, 0.0f, 0.0f);
			if (!Facingleft)
			{
				player.transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
				Weapon.transform.localScale = new Vector3(Weapon.transform.localScale.x * -1.0f, Weapon.transform.localScale.y, Weapon.transform.localScale.z);
				// GameObject.Find("Katana").GetComponent<SpriteRenderer>().localToWorldMatrix.MultiplyPoint(new Vector3(-1, 1, 1));
				// Weapon.transform.Translate(new Vector3(-50, 0, 0));
			}
            Facingleft = true;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.8f, player.GetComponent<Rigidbody2D>().velocity.y);

        }
        else if (Input.GetKey(KeyCode.D) && !isCrouched) //&& GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()))
        {
            //GetComponent<Animator>().SetBool("Walking", true);
            //PlayerLocation.Translate(0.1f, 0.0f, 0.0f);
            //Avatar.sprite = Walking;
			if (Facingleft)
			{
				player.transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
				Weapon.transform.localScale = new Vector3(Weapon.transform.localScale.x * -1.0f, Weapon.transform.localScale.y, Weapon.transform.localScale.z);
				
			}
            Facingleft = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(1.8f, player.GetComponent<Rigidbody2D>().velocity.y);
        }
        //else if (Input.GetKeyDown(KeyCode.Tab) && availstamina >= 10)
        //{
        //    CancelInvoke();
        //    if (!WeaponSelected)
        //    {
        //        RangedWeapon.SetActive(true);
        //        RangedWeapon.transform.position = MeleeWeapon.transform.position;
        //        RangedWeapon.transform.localScale = new Vector3(RangedWeapon.transform.localScale.x * -1.0f, RangedWeapon.transform.localScale.y, RangedWeapon.transform.localScale.z);
        //        MeleeWeapon.SetActive(false);
        //        Weapon = RangedWeapon;
        //    }
        //    else
        //    {
        //        Weapon = MeleeWeapon;
        //        RangedWeapon.SetActive(false);
        //        MeleeWeapon.SetActive(true);


        //    }
        //    WeaponSelected = !WeaponSelected;
        //    availstamina -= 10;
        //}
        else
        {
            GetComponent<Animator>().SetBool("Walking", false);
            GetComponent<Animator>().SetBool("Crouch", false);
            if (GetComponent<Rigidbody2D>().velocity.y != 0)
                Avatar.sprite = Jumping;
            else
                Avatar.sprite = Idle;
            isCrouched = false;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) /*canJump &&*/  /*&& GroundCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(15) */
        {
            if(availstamina >= 15)
            {
                if (GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    Avatar.sprite = Jumping;
                    //canJump = false;
                    CancelInvoke();
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
                    availstamina -= 15;
                }
            }
            
        }

        //if (GetComponent<Rigidbody2D>().velocity.y == 0 && GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()) || player.GetComponent<Collider2D>().IsTouchingLayers(15))
        //{
        //    canJump = true;
        //}

        Invoke("StaminaRegen", 0);

        if(Input.GetMouseButtonDown(0))
        {            Avatar.sprite = Attacking;
            availstamina -= 5;
            Weapon.transform.localRotation.Set(Weapon.transform.localRotation.x, Weapon.transform.localRotation.y + 30.5f, Weapon.transform.localRotation.z, Weapon.transform.localRotation.w);
               // GetComponent<Animator>().SetBool("Attacking", true);
            if(MeleeWeapon.activeSelf)
            {
                MeleeWeapon.GetComponent<BoxCollider2D>().enabled = true;                
            }
                
        }

        if (Input.GetMouseButtonDown(1))
        {
            //GetComponent<Animator>().SetBool("Attacking", true);
            Avatar.sprite = Attacking;
            if (MeleeWeapon.activeSelf)
            {
                MeleeWeapon.GetComponent<BoxCollider2D>().enabled = true;
            }
           // else
                //Instantiate(ArrowType);

            availstamina -= 5;
        }

        if(hitcount == 0)
        {
            GetComponent<Animator>().SetBool("Death", true);
            //Invoke("Death", 3);
			Death();
              

        }
        
    }

    void StaminaRegen()
    {
        if (availstamina < 100)
            availstamina += Time.deltaTime * 7.5f;
        else
            availstamina = 100;
    }

    void OnCollision2DEnter(Collider2D other)
    {
        //if(other.gameObject.tag == "Jumpable")
        //    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 600));
        if (other.gameObject.tag == "Hazardous")
            hitcount--;

    }

    void Death()
    {
        // Click to restart level

        // Somehow reload level blahblahblah
        Application.LoadLevel(Application.loadedLevelName);

    }
}
