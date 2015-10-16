using UnityEngine;
using System.Collections;

public class CameraLock : MonoBehaviour {

    [SerializeField]
    GameObject target;

    //[SerializeField]
    //GameObject GameCam;

    private BoxCollider2D bossbox;
    private Plane[] planes;

	// Use this for initialization
	void Start () 
    {
        Camera.main.orthographicSize = 4.2f;
        bossbox = null;
  	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(target.transform.localPosition.x >= transform.localPosition.x + 7.25f)
        {
            transform.localPosition = new Vector3(target.transform.localPosition.x + 7.3f, transform.localPosition.y,transform.localPosition.z);
        }

        if (Application.loadedLevelName == "Boss Arctic Menace")
        {
            GameObject boss = GameObject.Find("IceBoss");
            planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            bossbox = boss.GetComponent<BoxCollider2D>();

            if (GeometryUtility.TestPlanesAABB(planes, bossbox.bounds))
            {
                Debug.Log("Saw Boss");
                target.SendMessage("LockArea");
            }
        }
	}
}
