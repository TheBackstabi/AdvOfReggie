using UnityEngine;
using System.Collections;

public class CameraLock : MonoBehaviour {

    [SerializeField]
    GameObject target;

    //[SerializeField]
    //GameObject GameCam;

	// Use this for initialization
	void Start () 
    {
        Camera.main.orthographicSize = 4.2f;
  	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(target.transform.localPosition.x >= transform.localPosition.x + 7.25f)
        {
            transform.localPosition = new Vector3(target.transform.localPosition.x + 7.3f, transform.localPosition.y,transform.localPosition.z);
        }
	}
}
