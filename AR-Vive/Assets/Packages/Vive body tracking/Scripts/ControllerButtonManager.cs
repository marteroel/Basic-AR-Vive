using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButtonManager : MonoBehaviour {

	private SteamVR_TrackedController device;
	public static bool triggerIsPressed;
	public static bool padIsPressed;

	// Use this for initialization
	void Start () {
		device = GetComponent<SteamVR_TrackedController>();

		device.TriggerClicked += Trigger;
		device.TriggerUnclicked += Trigger;

		device.PadClicked += Pad;
		device.PadUnclicked += Pad;
	}

	void Trigger(object sender, ClickedEventArgs e) {
		
		triggerIsPressed = !triggerIsPressed;
		Debug.Log("Trigger has been pressed " +  triggerIsPressed);
	}

	void Pad(object sender, ClickedEventArgs e) {

		padIsPressed = !padIsPressed;
		Debug.Log("Pad has been pressed " +  padIsPressed);
	}
		
}
