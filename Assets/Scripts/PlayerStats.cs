using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	// Use this for initialization


    public int hitcount = 3;
    public float availstamina = 100;
    public bool isCrouched = false;
    public bool Facingleft = false;
    private GameObject GroundCheck;
    private Vector2 NormBounds;
    private Vector2 CrouchBounds;
    private bool WeaponSelected;
    
    [SerializeField]
    Sprite Idle;

    [SerializeField]
    GameObject MeleeWeapon;

    //[SerializeField]
    //GameObject RangedWeapon;

    [SerializeField]
    GameObject Weapon;

    [SerializeField]
    bool canJump = true;


    [SerializeField]
    bool jumped;

    [SerializeField]
    LayerMask platform;




	void Start () 
    {
        NormBounds = GetComponent<BoxCollider2D>().size;
        CrouchBounds = new Vector2(NormBounds.x, NormBounds.y -2.0f);
        GroundCheck = GameObject.Find("GroundCheck");
		Weapon = MeleeWeapon;
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) /*canJump &&*/)
        {
            if (Physics2D.OverlapCircle(GroundCheck.transform.position, 0.1f, platform))
                jumped = true;
            JumpingUp();
        }

        if (MeleeWeapon.GetComponent<BoxCollider2D>().enabled)
        {
            MeleeWeapon.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (hitcount <= 0)
        {
            GetComponent<Animator>().SetBool("death", true);
            Invoke("Death", Time.deltaTime * 34);
        }
        
        
	}

    void FixedUpdate()
    {
        

        StaminaRegen();
        if (Input.GetKey(KeyCode.S))
            CrouchingDown();
        else if (Input.GetKey(KeyCode.A) && !isCrouched)
            WalkingLeft();
        else if (Input.GetKey(KeyCode.D) && !isCrouched)
            WalkingRight();
        else if (Input.GetKeyDown(KeyCode.Tab) && availstamina >= 10)
            SwitchingWeapon();
        else
            IdleState();

        if(Input.GetMouseButtonDown(0) && availstamina > 10)
            LeftMouseAttack();
        if (Input.GetMouseButtonDown(1) && availstamina > 15)
            RightMouseAttack();

       
    }

    void IdleState()
    {
        GetComponent<Animator>().SetBool("walk", false);
            GetComponent<Animator>().SetBool("crouch", false);
            GetComponent<Animator>().SetBool("attack", false);
            if (GetComponent<Rigidbody2D>().velocity.y  <= 0)
                GetComponent<SpriteRenderer>().sprite = Idle;
            isCrouched = false;
    }

    void StaminaRegen()
    {
        if (availstamina < 100)
            availstamina += Time.deltaTime * 8.5f;
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

    void OnHit()
    {
        GetComponent<Animator>().ResetTrigger("hurt");
    }

    void JumpingUp()
    {
        if (jumped)
        {
            jumped = false;
            GetComponent<Animator>().SetBool("jump", true);
            if (availstamina >= 15)
            {
                //canJump = false;
                CancelInvoke();
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 7500));
                availstamina -= 15;
            }
        }

        if (Physics2D.OverlapCircle(GroundCheck.transform.position, 0.1f, platform) && GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            GetComponent<Animator>().SetBool("jump", false);
        }

        //Double Jump code
        //if (GetComponent<Rigidbody2D>().velocity.y == 0 && GroundCheck.GetComponent<BoxCollider2D>().IsTouching(Ground.GetComponent<BoxCollider2D>()) || player.GetComponent<Collider2D>().IsTouchingLayers(15))
        //{
        //    canJump = true;
        //}
    }

    bool BoundsDifference(Vector2 compared)
    {
        if (NormBounds.x == compared.x && NormBounds.y == compared.y)
        {
            Debug.Log("Bounds returned true");
            return true;
        }
        else
        {
            Debug.Log("Bounds returned false");
            return false;
        }

    }

    void CrouchingDown()
    {
        GetComponent<Animator>().SetBool("crouch", true);
        GetComponent<BoxCollider2D>().size.Set(1.2f, 5.0f);
        if (BoundsDifference(GetComponent<BoxCollider2D>().size))
            Debug.Log("Successful Change in Bounds");

        //Avatar.sprite = Crouching;
        if (!isCrouched)
        {
            //Weapon.transform.position = new Vector3(Weapon.transform.position.x, Weapon.transform.position.y - 0.1f, Weapon.transform.position.z);
            isCrouched = true;
        }
    }

    void WalkingLeft()
    {
        GetComponent<Animator>().SetBool("walk", true);
        if (!Facingleft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
            Weapon.transform.localScale = new Vector3(Weapon.transform.localScale.x * -1.0f, Weapon.transform.localScale.y, Weapon.transform.localScale.z);
        }
        Facingleft = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(-4.0f, GetComponent<Rigidbody2D>().velocity.y);
    }

    void WalkingRight()
    {
        GetComponent<Animator>().SetBool("walk", true);
        if (Facingleft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
            Weapon.transform.localScale = new Vector3(Weapon.transform.localScale.x * -1.0f, Weapon.transform.localScale.y, Weapon.transform.localScale.z);

        }
        Facingleft = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(4.0f, GetComponent<Rigidbody2D>().velocity.y);
    }

    void SwitchingWeapon()
    {
            if (WeaponSelected)
            {
                Weapon = MeleeWeapon;
            }
            else
            {
               // Weapon = RangedWeapon;
            }

            WeaponSelected = !WeaponSelected;
            availstamina -= 10;
    }

    void LeftMouseAttack()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        availstamina -= 10;
        Weapon.transform.localRotation.Set(Weapon.transform.localRotation.x, Weapon.transform.localRotation.y + 30.5f, Weapon.transform.localRotation.z, Weapon.transform.localRotation.w);
        GetComponent<Animator>().SetBool("attack", true);
        if (MeleeWeapon.activeSelf)
        {
            MeleeWeapon.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void RightMouseAttack()
    {
        GetComponent<Animator>().SetBool("attack", true);
        if (MeleeWeapon.activeSelf)
        {
            MeleeWeapon.GetComponent<BoxCollider2D>().enabled = true;
        }

        availstamina -= 15;
    }
}
