using UnityEngine;
using System.Collections;

public class EquippedWeapon : MonoBehaviour {

	// Use this for initialization
    public GameObject wielder;
    public GameObject Weapon;

	void Start () 
    {
        transform.position = new Vector3(wielder.transform.position.x, wielder.transform.position.y, wielder.transform.position.z);	    
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!wielder.GetComponent<PlayerStats>().Facingleft)
        {
            if (wielder.GetComponent<PlayerStats>().isCrouched)
            {
                transform.position = new Vector3(wielder.transform.position.x + 0.8f, wielder.transform.position.y + 1.0f, wielder.transform.position.z);
            }
            else
                transform.position = new Vector3(wielder.transform.position.x + 0.8f, wielder.transform.position.y + 1.2f, wielder.transform.position.z);
        }
        else
        {
            if (wielder.GetComponent<PlayerStats>().isCrouched)
            {
                transform.position = new Vector3(wielder.transform.position.x - 0.8f, wielder.transform.position.y + 1.0f, wielder.transform.position.z);
            }
            else
                transform.position = new Vector3(wielder.transform.position.x - 0.8f, wielder.transform.position.y + 1.2f, wielder.transform.position.z);
        }

	}

    void FixedUpdate()
    {

    }
}
