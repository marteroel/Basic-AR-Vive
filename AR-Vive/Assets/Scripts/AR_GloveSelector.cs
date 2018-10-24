using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_GloveSelector : MonoBehaviour {

	public GameObject glove;

	public void AddGlove(){
		glove.SetActive (true);
	}
}
