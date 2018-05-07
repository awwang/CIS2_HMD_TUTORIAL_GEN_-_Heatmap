using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnHide ()
	{
		Debug.LogWarning("hide command");
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("scene"));
	}
}
