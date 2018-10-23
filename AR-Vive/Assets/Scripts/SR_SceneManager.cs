using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vive.Plugin.SR;

public class SR_SceneManager : MonoBehaviour {

	private ViveSR SR;
	private bool isOn;

	// Use this for initialization
	void Start () {
		SR = GetComponent<ViveSR> ();
		StartCoroutine (DelayARStart (5f));
	}
		
	IEnumerator DelayARStart(float time){
		yield return new WaitForSecondsRealtime (time);
		TurnON ();
	}

	public void TurnON(){
		SR.StartFramework ();
		Debug.Log ("Attempting to turn AR on");
	}

	public void TurnOFF(){
		SR.StopFramework ();
		Debug.Log ("Attempting to turn AR off");
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown ("v")) {
			if (!isOn) {
				isOn = true;
				TurnON ();
			}
			else if (isOn) {
				isOn = false;
				TurnOFF ();
			}
		}*/

	}
}
