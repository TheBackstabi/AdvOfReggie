using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatUIControls : MonoBehaviour {

    [SerializeField]
    Slider stamina;

    [SerializeField]
    Image health;

    [SerializeField]
    PlayerStats player;

    private int healthwidth = 230;
    private int healthcount;
    private int staminacount;

	// Use this for initialization
	void Start () 
    {
        healthcount = player.hitcount;
        stamina.value = stamina.maxValue;
        //Need to add hit count from player or some interaction from that.
        health.rectTransform.sizeDelta = new Vector2(healthwidth * healthcount,health.rectTransform.sizeDelta.y);
	}
	
	// Update is called once per frame
	void Update () 
    {
        stamina.value = player.availstamina;
        if(healthcount != player.hitcount)
        health.rectTransform.sizeDelta = new Vector2(healthwidth, health.rectTransform.sizeDelta.y);
	    
	}
}
