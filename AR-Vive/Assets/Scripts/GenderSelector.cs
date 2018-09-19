using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class GenderSelector : MonoBehaviour {

	public GameObject male, female;

	// Use this for initialization
	void Start () {
		if (BasicDataConfigurations.gender == "Male")
			female.SetActive (false);
		else
			male.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
