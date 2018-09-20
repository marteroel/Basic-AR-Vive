using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VASLabeler : MonoBehaviour {

	//public Text lowLabel, highLabel;
	public string[] lowLabelNames, highLabelNames;
	public Text lowLabel, highLabel;

	public void ChangeLabels(int qIndex){
		lowLabel.text = lowLabelNames[qIndex];
		highLabel.text = highLabelNames [qIndex];
	}
}
