using UnityEngine;
using System.Collections;

public class MusicTransitionScript : MonoBehaviour
{

    public GameObject Doggy = null;

    public float TransitionSpeed = 0.25f;

    public bool Flip = false;

    enum Triggered { NotTriggered, PamgaeaLoop, LightlessDawn };

    Triggered transition;

    AudioSource PamgaeaLoop = null;
    AudioSource LightlessDawn = null;

    // Use this for initialization
    void Start()
    {
        if(Doggy != null)
        {
            foreach(AudioSource audioSource in Doggy.GetComponents<AudioSource>())
            {
                if(audioSource.clip != null && audioSource.clip.name == "PamgaeaLoop")
                {
                    PamgaeaLoop = audioSource;
                }
                else if(audioSource.clip != null && audioSource.clip.name == "LightlessDawn")
                {
                    LightlessDawn = audioSource;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LightlessDawn != null && PamgaeaLoop != null)
        {
            if (transition == Triggered.LightlessDawn)
            {
                if (LightlessDawn.volume < 1)
                {
                    LightlessDawn.volume += Time.deltaTime * TransitionSpeed;
                    PamgaeaLoop.volume -= Time.deltaTime * TransitionSpeed;
                }
                if (LightlessDawn.volume >= 1)
                {
                    LightlessDawn.volume = 1;
                    PamgaeaLoop.volume = 0;
                    transition = Triggered.NotTriggered;
                }
            }
            else if (transition == Triggered.PamgaeaLoop)
            {
                if (PamgaeaLoop.volume < 1)
                {
                    PamgaeaLoop.volume += Time.deltaTime * TransitionSpeed;
                    LightlessDawn.volume -= Time.deltaTime * TransitionSpeed;
                }
                if (PamgaeaLoop.volume >= 1)
                {
                    PamgaeaLoop.volume = 1;
                    LightlessDawn.volume = 0;
                    transition = Triggered.NotTriggered;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (!Flip)
        {
            if (Doggy.transform.position.x < gameObject.transform.position.x)
                transition = Triggered.PamgaeaLoop;
            else
                transition = Triggered.LightlessDawn;
        }
        else
        {
            if (Doggy.transform.position.x > gameObject.transform.position.x)
                transition = Triggered.PamgaeaLoop;
            else
                transition = Triggered.LightlessDawn;
        }
    }
}
