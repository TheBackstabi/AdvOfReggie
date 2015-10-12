using UnityEngine;
using System.Collections;

public class WeaponStats : MonoBehaviour {

	// Use this for initialization
    public float range;
    public float basicAtt;
    public float heavyAtt;
    public float damage;
    public bool IsRanged;
    public bool IsEquipped;
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        

	}

    void FixedUpdate()
    {
        if (IsEquipped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!IsRanged)
                {
                    damage = basicAtt;
                }
                //else
                //SendMessageUpwards("Fire");
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (!IsRanged)
                    damage = heavyAtt;
                // else
                //SendMessageUpwards("Fire");

            }
        }
    }

    
}
