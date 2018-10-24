using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;
using UnityEngine.SceneManagement;
using Vive.Plugin.SR;

public class ConditionLoader : MonoBehaviour {

	public bool isStimulationNotInstructions, isPostCondition, isPreQuestionnaire;
	public static int currentCondition;
	public string conditionToLoadAfterStimulation;

	//public SR_SceneManager AR_sceneManager;

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

			currentCondition++;
			//Debug.Log ("current condition is " + currentCondition + " dictionary lenght is " + ConditionDictionary.selectedOrder.Length);

			if (currentCondition < ConditionDictionary.selectedOrder.Length)
				SceneManager.LoadScene ("Inter");
			else
				SceneManager.LoadScene ("Goodbye");
		} 

		else {
			//Debug.Log ("this is going through");
			if (isPreQuestionnaire)
				SceneManager.LoadScene ("Inter");
			else
				SceneManager.LoadScene (ConditionDictionary.selectedOrder [currentCondition]);
			}


	}
			

}
