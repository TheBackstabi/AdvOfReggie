using UnityEngine;
using System.Collections;

public class AM_ArrowScript : MonoBehaviour {
    public float fireStrength = 100.0f;
    public Transform target = null;

    private Quaternion myRotation;
	// Use this for initialization
	void Start () {
        if (tag == "PlayerWeapon")
        {
            // Player stuff
        }
        else
        {
            if (target != null)
            {
                if (target.position.x > transform.position.x + 2)
                    transform.Rotate(0.0f, 0.0f, -90.0f);
                else if (target.position.x < transform.position.x - 2)
                    transform.Rotate(0.0f, 0.0f, 90.0f);
                else if (target.position.y < transform.position.y)
                    transform.Rotate(0.0f, 0.0f, 180.0f);
                GetComponent<Rigidbody2D>().AddForce(fireStrength * (target.position - transform.position));
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Nothing to put here
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
