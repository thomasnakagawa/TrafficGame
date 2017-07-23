using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetector : MonoBehaviour {
	void OnTriggerEnter(Collider collider) {
		Car car = collider.gameObject.GetComponent<Car>();
		if (car != null && car.numberOfIntersectionsEntered >= 3) {
			Score.instance.AddScore ();
		}
	}
}
