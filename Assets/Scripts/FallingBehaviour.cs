using UnityEngine;
using System.Collections;

public class FallingBehaviour : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.collider.tag == "PlayerWeapon" || coll.collider.tag == "Player")
		{
			GetComponent<Rigidbody2D>().mass = 8.0f;
			GetComponent<Rigidbody2D>().gravityScale = 8.0f;
			//GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -600.0f));
		}
		if(coll.collider.tag == "Platform")
		{
			if(coll.collider.name == "Ground")
			{
				GetComponent<Rigidbody2D>().gravityScale = 0.0f;
				GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}
}
