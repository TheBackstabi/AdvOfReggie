using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour 
{
	public void Switch(string scene)
	{
		Application.LoadLevel(scene);
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 3)
		{
			if(gameObject.name == "Exit Door")
				GetComponent<Animator>().SetBool("exit", true);
			if(Application.loadedLevelName == "Real_Tutorial" && other.gameObject.tag == "Player" && Input.GetMouseButton(0))
			{
				Application.LoadLevel("Level 7 Secret Halls");
			}
		}
		if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 3)
		{
			if(gameObject.name == "Exit Door")
				GetComponent<Animator>().SetBool("exit", true);
			if(Application.loadedLevelName == "Level 7 Secret Halls" && other.gameObject.tag == "Player" && Input.GetMouseButton(0))
			{
				Application.LoadLevel("Boss Arctic Menace");
			}
		}
		if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 1 && other.gameObject.tag == "Player" && Input.GetMouseButton(0))
		{
			if(gameObject.name == "Exit Door")
				GetComponent<Animator>().SetBool("exit", true);
			if(Application.loadedLevelName == "Boss Arctic Menace")
			{
				Application.LoadLevel("Main Menu");
			}
		}
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
