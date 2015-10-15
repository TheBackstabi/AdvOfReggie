using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour 
{
	//public GameObject Enemies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Switch(string scene)
	{
		Application.LoadLevel(scene);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && Input.GetMouseButton(0))
		{
			if(Application.loadedLevelName == "Real_Tutorial")
			{
				Application.LoadLevel("Level 7 Secret Halls");
			}
			if(Application.loadedLevelName == "Level 7 Secret Halls")
			{
				Application.LoadLevel("Boss Arctic Menace");
			}
			if(Application.loadedLevelName == "Boss Arctic Menace")
			{
				Application.LoadLevel("Main Menu");
			}
		}
	}
}
