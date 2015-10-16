using UnityEngine;
using System.Collections;

public class BounceBehaviour : MonoBehaviour 
{
	public GameObject Player;
	
	void OnTriggerEnter2D( Collider2D coll )
	{		
		if(coll.tag == "Player" && Player.GetComponent<Rigidbody2D>().velocity.y != 0)
		{
			Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Player.GetComponent<Rigidbody2D>().velocity.x*2, 13400.0f));
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
