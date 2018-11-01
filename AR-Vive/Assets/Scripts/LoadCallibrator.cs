using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCallibrator : MonoBehaviour {

	void Update() {
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("Intro");
		}
	}

	public void LoadCallibratorScene(){
		SceneManager.LoadScene ("VR_Setup");
	}
}
