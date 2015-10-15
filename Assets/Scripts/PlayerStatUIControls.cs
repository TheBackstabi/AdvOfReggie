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

    private int healthwidth = 235;
    public int healthcount;
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
        healthcount = player.hitcount;
        stamina.value = player.availstamina;
       // if(stamina.value == 0)
           // stamina.
        //if(healthcount != player.hitcount)
        //health.
        health.rectTransform.sizeDelta = new Vector2(healthwidth * healthcount, health.rectTransform.sizeDelta.y);
        
	    
	}
}
