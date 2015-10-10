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
		if(coll.collider.tag == "PlayerWeapon")
		{
			GetComponent<Rigidbody2D>().gravityScale = 1;
		}
	}
}
