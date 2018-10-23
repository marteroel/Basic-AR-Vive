using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;
using UnityEngine.SceneManagement;
using Vive.Plugin.SR;

public class ConditionLoader : MonoBehaviour {

	public bool isStimulationNotInstructions, isPostCondition, isPreQuestionnaire, isAR;
	public static int currentCondition;
	public string conditionToLoadAfterStimulation;

	public SR_SceneManager AR_sceneManager;

	// Use this for initialization
	void Start () {
		BasicDataConfigurations.isPlacebo = true;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("return")) {
			StartCoroutine (WaitToLoadScene ());
			//LoadScene ();
		}
	}

	private IEnumerator WaitToLoadScene(){
		yield return new WaitForSecondsRealtime (1f);
		LoadScene ();
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
				
				if (isAR) {
					if (AR_sceneManager != null) {
						AR_sceneManager.TurnOFF ();
						StartCoroutine (AR_stopper ());
					}
				} else {
					if (BasicDataConfigurations.isPlacebo)
						SceneManager.LoadScene (conditionToLoadAfterStimulation + " placebo");
					else
						SceneManager.LoadScene (conditionToLoadAfterStimulation);
				}
			}
		} else {
			Debug.Log ("this is going through");
			if (isPreQuestionnaire)
				SceneManager.LoadScene ("Inter");
			else
				SceneManager.LoadScene (ConditionDictionary.selectedOrder [currentCondition]);
			}


	}
			
	IEnumerator AR_stopper() {
			yield return new WaitForSecondsRealtime (2.5f);
			Debug.Log ("THE SCENE LOAD IS BEING CALLED HERE!!");
			if (BasicDataConfigurations.isPlacebo)
				SceneManager.LoadScene (conditionToLoadAfterStimulation + " placebo");
			else
				SceneManager.LoadScene (conditionToLoadAfterStimulation);
	}

}
