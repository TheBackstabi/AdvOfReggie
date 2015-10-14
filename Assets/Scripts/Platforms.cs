using UnityEngine;
using System.Collections;

public class Platforms : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.collider.tag == "Hazardous")
		{
			this.gameObject.AddComponent<Rigidbody2D>();
			GetComponent<Rigidbody2D>().mass = 8.0f;
			GetComponent<Rigidbody2D>().gravityScale = 7.0f;
		}
	}

	void OnCollisionStay2D(Collision2D collide)
	{
		if(collide.collider.name == "Ground")
		{
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<EdgeCollider2D>().enabled = false;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
