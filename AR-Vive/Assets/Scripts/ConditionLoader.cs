using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;
using UnityEngine.SceneManagement;

public class ConditionLoader : MonoBehaviour {

	public bool isStimulationNotInstructions;
	public static int currentCondition;

	// Use this for initialization
	void Start () {
		BasicDataConfigurations.isPlacebo = true;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			OnNextButton ();
		}
	}

	public void OnNextButton() {

		if (isStimulationNotInstructions) {
			currentCondition++;
			SceneManager.LoadScene ("VAS");
		}
		else {
			if (BasicDataConfigurations.isPlacebo)
				SceneManager.LoadScene (ConditionDictionary.selectedOrder [currentCondition] + " glove");
			else
				SceneManager.LoadScene (ConditionDictionary.selectedOrder [currentCondition] + " no glove");
		}

			
	}
}
