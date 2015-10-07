using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	// Use this for initialization

    public int hitcount = 3;
    public int availstamina = 100;
    public GameObject player;
    private BoxCollider2D collision;
	void Start () 
    {
        collision = player.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
