using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleVAS;

public class CSVconditionWriter : MonoBehaviour {

	public static string condition;
	// Use this for initialization
	void Start () {
		condition = SceneManager.GetActiveScene ().name + " pre";
	}


	public void ChangeCondition(){
		condition = SceneManager.GetActiveScene ().name + " post";
	}
}
