using UnityEngine;
using System.Collections;

public class MusicTransitionScript : MonoBehaviour
{

    public GameObject Player = null;
	//public AudioClip FirstSound;
	//public AudioClip SecondSound;
    public float TransitionSpeed = 0.25f;
    public bool Flip = false;

    enum Triggered { NotTriggered, FirstSound, SecondSound };

    Triggered transition;

    AudioSource FirstSound = null;
    AudioSource SecondSound = null;

    // Use this for initialization
    void Start()
    {
        if(Player != null)
        {
            foreach(AudioSource audioSource in Player.GetComponents<AudioSource>())
            {
                if(audioSource.clip != null && audioSource.clip.name == "Theme1")
                {
                    FirstSound = audioSource;
                }
                else if(audioSource.clip != null && audioSource.clip.name == "Theme2")
                {
                    SecondSound = audioSource;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SecondSound != null && FirstSound != null)
        {
            if (transition == Triggered.SecondSound)
            {
                if (SecondSound.volume < 1)
                {
                    SecondSound.volume += Time.deltaTime * TransitionSpeed;
                    FirstSound.volume -= Time.deltaTime * TransitionSpeed;
                }
                if (SecondSound.volume >= 1)
                {
                    SecondSound.volume = 1;
                    FirstSound.volume = 0;
                    transition = Triggered.NotTriggered;
                }
            }
            else if (transition == Triggered.FirstSound)
            {
                if (FirstSound.volume < 1)
                {
                    FirstSound.volume += Time.deltaTime * TransitionSpeed;
                    SecondSound.volume -= Time.deltaTime * TransitionSpeed;
                }
                if (FirstSound.volume >= 1)
                {
                    FirstSound.volume = 1;
                    SecondSound.volume = 0;
                    transition = Triggered.NotTriggered;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (!Flip)
        {
            if (Player.transform.position.x < gameObject.transform.position.x)
                transition = Triggered.FirstSound;
            else
                transition = Triggered.SecondSound;
        }
        else
        {
            if (Player.transform.position.x > gameObject.transform.position.x)
                transition = Triggered.FirstSound;
            else
                transition = Triggered.SecondSound;
        }
    }
}
