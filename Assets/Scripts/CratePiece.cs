using UnityEngine;
using System.Collections;

public class CratePiece : MonoBehaviour
{
    float timer = 3f;

    void FixedUpdate()
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            timer -= Time.deltaTime;

            if (timer <= 1.5f)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
            }
        }
    }

    void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }
}
