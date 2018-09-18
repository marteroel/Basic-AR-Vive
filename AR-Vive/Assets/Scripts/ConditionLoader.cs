using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;
using UnityEngine.SceneManagement;

public class ConditionLoader : MonoBehaviour {

	public bool isStimulationNotInstructions, isPostCondition;
	public static int currentCondition;
	public string conditionToLoadAfterStimulation;

	// Use this for initialization
	void Start () {
		BasicDataConfigurations.isPlacebo = true;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("return")) {
			LoadScene ();
		}
	}

	public void LoadScene() {

		if (isStimulationNotInstructions) {
			
			currentCondition++;

			if (isPostCondition) SceneManager.LoadScene ("Inter");
			 
			else {
				if (BasicDataConfigurations.isPlacebo)	SceneManager.LoadScene (conditionToLoadAfterStimulation + " placebo");
				else	SceneManager.LoadScene (conditionToLoadAfterStimulation);
			}
		}

		else {
				SceneManager.LoadScene (ConditionDictionary.selectedOrder [currentCondition]);
		}

			
	}
}
