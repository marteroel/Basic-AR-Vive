using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleVAS;

public class InstructionsManager : MonoBehaviour {

	public GameObject VRInstructions, ARInstructions, PRInstructions;

	// Use this for initialization
	void Start () {
		if (ConditionDictionary.selectedOrder [ConditionLoader.currentCondition] == "VR")
			VRInstructions.SetActive (true);
		else if (ConditionDictionary.selectedOrder [ConditionLoader.currentCondition] == "AR")
			ARInstructions.SetActive (true);
		else if (ConditionDictionary.selectedOrder [ConditionLoader.currentCondition] == "PR")
			PRInstructions.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
