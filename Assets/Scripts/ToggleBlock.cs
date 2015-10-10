using UnityEngine;
using System.Collections;

public class ToggleBlock : MonoBehaviour
{
    public bool toggle;
    public Sprite solid;
    public Sprite trans;

    public void Toggle(bool _toggle)
    {
        toggle = _toggle;

        if ( toggle ) 
        {
            GetComponent<SpriteRenderer>().sprite = solid;
            GetComponent<BoxCollider2D>().enabled = true;
        } 
        else 
        {
            GetComponent<SpriteRenderer>().sprite = trans;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
