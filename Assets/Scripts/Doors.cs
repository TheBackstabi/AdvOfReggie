using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour 
{
	private float opentime;
	// Use this for initialization
	void Start () 
	{
		opentime = 0.0f;
	}

	void OnTriggerEnter2D( Collider2D specEnemy )
	{
		if(specEnemy.tag == "Special Enemy")
			GetComponent<Animator>().SetBool("open", true);
	}

	void OnTriggerExit2D( Collider2D specEnemy )
	{
		if(specEnemy.tag == "Special Enemy")
		{
			GetComponent<Animator>().SetBool("open", false);
			GetComponent<Animator>().SetBool("close", true);
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void FixedUpdate()
	{
		if(GetComponent<Animator>().GetBool("open") == false && GetComponent<Animator>().GetBool("close") == true)
		{
			GetComponent<Animator>().SetBool("open", false);
			GetComponent<Animator>().SetBool("close", false);
		}
	}
}
