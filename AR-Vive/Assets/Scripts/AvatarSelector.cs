using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class AvatarSelector : MonoBehaviour {

	public GameObject malePre, femalePre, malePost, femalePost;

	// Use this for initialization
	void Start () {

		if (BasicDataConfigurations.gender != "") {

			if (BasicDataConfigurations.gender == "Male")
				malePre.SetActive (true);
			else
				femalePre.SetActive (true);

		} else
			femalePre.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeToPost(){
		if (BasicDataConfigurations.isPlaceboDummy) {
			if (BasicDataConfigurations.gender == "Male") {
				malePre.SetActive (false);
				malePost.SetActive (true);
			} 

			else {
				Debug.Log ("should load the avatar with glove");
				femalePre.SetActive (false);
				femalePost.SetActive (true);
			}
		}
	}
		
}
