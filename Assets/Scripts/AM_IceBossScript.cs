using UnityEngine;
using System.Collections;

public class AM_IceBossScript : MonoBehaviour {
    public float fireRate = 5.0f;
    public int arrowDamageVal = 1;
    public GameObject Reticule;

    private float timeSinceLastArrow = 0;
    private AM_NPCScript baseScript;
    private bool arrowSet = false;

	// Use this for initialization
	void Start () {
        baseScript = GetComponent<AM_NPCScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (baseScript.isActive)
        {
            timeSinceLastArrow += Time.deltaTime;
            if (timeSinceLastArrow >= fireRate && arrowSet)
            {
                arrowSet = false;
                GameObject newArrow = Instantiate(baseScript.Arrow, transform.position, transform.rotation) as GameObject;
                newArrow.GetComponent<AM_ArrowScript>().SetDamageVal(arrowDamageVal);
                newArrow.tag = "EnemyArrow";
                newArrow.GetComponent<AM_ArrowScript>().target = Reticule.transform;
                timeSinceLastArrow = 0;
                Reticule.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (timeSinceLastArrow >= fireRate - 3 && !arrowSet)
            {
                arrowSet = true;
                Reticule.GetComponent<SpriteRenderer>().enabled = true;
                Reticule.transform.position = baseScript.Reggie.transform.position;
            }
        }
	}
}
