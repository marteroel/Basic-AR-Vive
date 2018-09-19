using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScaler : MonoBehaviour {

	public GameObject avatar;
	public Transform originalFootPos, originalHeadPos, userFootPos, userHeadPos;

	private float originalSize, userSize;
	private bool isResizing = false;
	private Transform initialAvatarScale;

	// Use this for initialization
	void Start () {
		
		originalSize = Vector3.Distance (originalFootPos.position, originalHeadPos.position);

		//initialAvatarScale.localScale = new Vector3 (1f, 1f, 1f);

		//initialAvatarScale.localScale = avatar.transform.localScale;
		//Debug.Log ("The initial avatar scale is " + initialAvatarScale.localScale);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isResizing) {
			Resize ();
		}

		if (Input.GetKeyDown ("space"))
			isResizing = !isResizing;

	}

	public void Resize() {
		userSize = Vector3.Distance (userFootPos.position, userHeadPos.position);
		float ratio = userSize / originalSize;
		//Debug.Log (ratio);
		//avatar.transform.localScale = initialAvatarScale.localScale * ratio;
		avatar.transform.localScale = new Vector3 (1f, 1f, 1f) * ratio;

	}



}
