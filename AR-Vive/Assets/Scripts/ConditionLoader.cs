using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;
using UnityEngine.SceneManagement;

public class ConditionLoader : MonoBehaviour {

	public bool isStimulationNotInstructions, isPostCondition, isPreQuestionnaire;
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

			if (isPostCondition) {
				currentCondition++;
				Debug.Log ("current condition is " + currentCondition + " dictionary lenght is " + ConditionDictionary.selectedOrder.Length);

				if (currentCondition < ConditionDictionary.selectedOrder.Length)
					SceneManager.LoadScene ("Inter");
				else
					SceneManager.LoadScene ("Goodbye");
			} else {
				if (BasicDataConfigurations.isPlacebo)
					SceneManager.LoadScene (conditionToLoadAfterStimulation + " placebo");
				else
					SceneManager.LoadScene (conditionToLoadAfterStimulation);
			}
		} else {
			if (isPreQuestionnaire)
				SceneManager.LoadScene ("Inter");
			else
				SceneManager.LoadScene (ConditionDictionary.selectedOrder [currentCondition]);
		}
			
	}
}
