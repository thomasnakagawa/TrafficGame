using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

	private PlayerInteractable hoveredObject;

	// Update is called once per frame
	void Update () {
		PlayerInteractable objectUnderMouse = getObjectUnderMouse ();
		if (objectUnderMouse != null) {
			// something under the mouse. Unhover the previous hovered object (if any), and hover the new object
			if (objectUnderMouse != hoveredObject) {
				if (hoveredObject != null) {
					hoveredObject.UnHovered ();
				}
				objectUnderMouse.Hovered ();
				hoveredObject = objectUnderMouse;
			}

			// call Clicked if clicked
			if (Input.GetMouseButtonDown (0)) {
				objectUnderMouse.Clicked ();
			}
		} else {
			// nothing under mouse, unhover any hovered object
			if (hoveredObject != null) {
				hoveredObject.UnHovered ();
				hoveredObject = null;
			}
		}
	}

	private PlayerInteractable getObjectUnderMouse() {
		LayerMask layerMask = 1 << 8; // only interactive layer
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, layerMask)) {
			return hit.collider.GetComponent<PlayerInteractable> ();
		}
		return null;
	}
}
