using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandTrackers : MonoBehaviour {

	public SteamVR_ControllerManager controllerManager;
	public List<GameObject> referencePositions;
	private List<GameObject> trackedPositions = new List<GameObject>();

	private int minimumValueIndex;
	private bool hasCallibrated = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (hasCallibrated) {
			for (int i = 0; i < trackedPositions.Count; i++) {
				referencePositions [i].transform.SetPositionAndRotation (trackedPositions [i].transform.position, trackedPositions [i].transform.rotation);
			}
		}
		if (Input.GetKeyDown ("c"))
			SetControllerToLimb ();
	}

	private void SetControllerToLimb() {

		foreach (var item in referencePositions) { 
			trackedPositions.Add (null); // to fill list and then replace with matched items.
		}

		foreach (var item in controllerManager.objects) {
			if (referencePositions.Count != 0) {
				if (item.transform.position == Vector3.zero) {
					continue;
				}

				Vector3 currentItemPosition = item.transform.position;
				float minimumDistance = float.MaxValue;



				for (int i = 0; i < referencePositions.Count; i++) {

					float currentDistance = Vector3.Distance (currentItemPosition, referencePositions [i].transform.position);

					if (currentDistance < minimumDistance) {
						minimumDistance = currentDistance;
						minimumValueIndex = i;
					}

					trackedPositions [minimumValueIndex] = item;
					Debug.Log ("added item " + trackedPositions [minimumValueIndex].name + " corresponding to IK " + referencePositions[minimumValueIndex].name);

					List<Vector3> tempPositionList = new List<Vector3>();
					List<Quaternion> tempRotationList = new List<Quaternion>();

				}
			}
		}

		hasCallibrated = true;
	}


}
