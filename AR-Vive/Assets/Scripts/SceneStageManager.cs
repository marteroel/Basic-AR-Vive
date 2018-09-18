using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStageManager : MonoBehaviour {

	public CsvReadMultiple questionManager;
	public ConditionLoader conditionLoader;
	public GameObject questionnaireUI;
	private int currentQuestion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown("space")) {
			
			if (currentQuestion < questionManager.files.Length) {
				currentQuestion++;
				ManageUI();
			}
			/*
			else {
				currentQuestion = 0;	
				conditionLoader.OnNextButton ();
			}*/
		}
	}

	public void ManageUI (){
		if (questionnaireUI.activeSelf == true)
			questionnaireUI.SetActive (false);
		else
			questionnaireUI.SetActive (true);
	}
}
