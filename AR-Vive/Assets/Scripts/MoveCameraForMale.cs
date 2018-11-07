using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class MoveCameraForMale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (BasicDataConfigurations.gender == "Male") { //|| BasicDataConfigurations.gender == null
			float bias = -0.1f;
			this.transform.Translate (0f, 0f, -0.02f);
		}
	}
		
}
