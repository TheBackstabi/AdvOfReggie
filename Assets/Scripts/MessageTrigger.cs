using UnityEngine;
using System.Collections;

public class MessageTrigger : MonoBehaviour 
{
	public GameObject Message;

	void OnTriggerStay2D( Collider2D coll )
	{
		if(coll.tag == "Player")
		{
			Message.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.tag == "Player")
		{
			Message.SetActive(false);
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
