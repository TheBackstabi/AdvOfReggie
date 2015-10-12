using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour
{
    private GameObject firedArrow;
    void Start()
    {
        firedArrow = Instantiate(GameObject.Find("Arrow"));
        firedArrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<WeaponStats>().range * 100, Input.mousePosition.y * 100));
        //firedArrow.transform.Translate(Input.mousePosition.x, Input.mousePosition.y, 00.0f);
    }
    
}
