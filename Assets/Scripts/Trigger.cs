using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

    public GameObject InteractiveObject;
    public GameObject InteractiveObject2;
	
    void OnTriggerEnter2D( Collider2D coll )
    {
        if(coll.tag == "Hazardous")
        {
			InteractiveObject.SetActive(false);
			InteractiveObject2.SetActive(true);
        }
    }
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
