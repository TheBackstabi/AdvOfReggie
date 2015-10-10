using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject GameCam;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(target.transform.localPosition.x >= GameCam.transform.localPosition.x + 3)
        {
            GameCam.transform.localPosition = new Vector3(target.transform.localPosition.x + 3.0f, GameCam.transform.localPosition.y,GameCam.transform.localPosition.z);
        }
	}
}
