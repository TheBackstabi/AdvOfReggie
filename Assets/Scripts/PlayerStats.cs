using UnityEngine;
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
    public GameObject Camera1;
    public GameObject GroundCheck;
    public GameObject Ground;
    public GameObject Weapon;
    private bool canJump;


	void Start () 
    {
       
        collision = player.GetComponent<BoxCollider2D>();
        Avatar = player.GetComponent<SpriteRenderer>();
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
        if (Input.GetKey(KeyCode.S))
        {
            Avatar.sprite = Crouching;
        }
        else if (Input.GetKey(KeyCode.A)) //&& GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()))
        {
            //Avatar.sprite = Walking;
            //player.transform.localScale = new Vector3(-1.0f *player.transform.localScale.x , player.transform.localScale.y, player.transform.localScale.z);
           // PlayerLocation.Translate(-0.1f, 0.0f, 0.0f);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.8f, player.GetComponent<Rigidbody2D>().velocity.y);

        }
        else if (Input.GetKey(KeyCode.D)) //&& GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()))
        {
            //Avatar.sprite = Walking;
            //PlayerLocation.Translate(0.1f, 0.0f, 0.0f);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(1.8f, player.GetComponent<Rigidbody2D>().velocity.y);

        }
        else if (Input.GetKey(KeyCode.Tab))
        {
            CancelInvoke();
            if (WeaponSelected)
                Weapon.GetComponent<SpriteRenderer>().sprite = RangedWeapon;
            else
                Weapon.GetComponent<SpriteRenderer>().sprite = MeleeWeapon;

            WeaponSelected = !WeaponSelected;
            availstamina -= 10;
        }
        else
        {
            Avatar.sprite = Idle;
            Invoke("StaminaRegen", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump)//&& GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()) && GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            //Avatar.sprite = Jumping;
            canJump = false;
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

    void OnCollision2DEnter(Collider2D other)
    {
        if (other.gameObject.tag == "Platform")
            canJump = true;
        if(other.gameObject.tag == "Jumpable")
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
        if (other.gameObject.tag == "Hazardous")
            hitcount--;

    }
}
