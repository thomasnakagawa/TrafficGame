using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

public class Car : MonoBehaviour {
	[SerializeField] private float stoppingDistance = 3f;
	public Direction driveDirection { get; private set;}
	private HashSet<Intersection> intersectionsEntered;
	private List<Transform> eyes;

	// Use this for initialization
	void Start () {
		// determine which way this car will move
		driveDirection = Utils.AngleToDirection (transform.localEulerAngles.y);
		GetComponent<Renderer> ().material = ColorSource.instance.GetDirectionMaterial (driveDirection);

		// get the position and rotation of the eyes to raycast from
		eyes = new List<Transform> ();
		foreach (Transform child in transform) {
			if (child.name.StartsWith("Eyes")) {
				eyes.Add (child);
			}
		}
		Assert.IsTrue (eyes.Count > 0);

		intersectionsEntered = new HashSet<Intersection> ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject objSeen = lookInFront ();
		if (objSeen == null) {
			moveForward ();
		} else {
			if (objSeen.GetComponent<Intersection> ()) {
				intersectionsEntered.Add (objSeen.GetComponent<Intersection> ());
				// intersection is in front. enter the intersection or wait impatiently
				if (canEnterIntersection (objSeen.GetComponent<Intersection> ())) {
					moveForward ();
				} else {
					waitImpatiently ();
				}
			} else if (objSeen.GetComponent<Car> ()) {
				// car is in front. Only wait impatiently if other is going in same direction
				if (objSeen.GetComponent<Car> ().driveDirection == driveDirection) {
					waitImpatiently ();
				}
			}
		}
	}

	public int numberOfIntersectionsEntered {
		get {
			return intersectionsEntered.Count;
		}
	}

	/// <summary>
	/// Looks in front of the car with raycats. Returns the first object it sees
	/// </summary>
	/// <returns>The in front.</returns>
	private GameObject lookInFront() {
		LayerMask layerMask = (1 << 8) | (1 << 9); // interactive or car layers
		foreach (Transform eye in eyes) {
			RaycastHit hit;
			if (Physics.Raycast (eye.position, eye.forward, out hit, stoppingDistance, layerMask)) {
				return hit.collider.gameObject;
			}
		}
		return null;
	}

	private bool canEnterIntersection(Intersection intersection) {
		return driveDirection == intersection.FlowDirection || driveDirection == Utils.OppositeDirection (intersection.FlowDirection);
	}

	private void moveForward() {
		transform.position += transform.forward * Time.deltaTime * 8f; // magic speed sry
		Patience.instance.hidePatienceIndicator (this);
	}

	private void waitImpatiently() {
		Patience.instance.losePatience (Time.deltaTime);
		Patience.instance.showPatienceIndicator (this);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		// show the vision rays
		foreach (Transform child in transform) {
			if (child.name.StartsWith("Eyes")) {
				Gizmos.DrawLine (child.position, child.position + child.forward * stoppingDistance);
			}
		}
	}
}
