using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetector : MonoBehaviour {
	[SerializeField] private GameObject indicatorPrefab;

	void OnTriggerEnter(Collider collider) {
		Car car = collider.gameObject.GetComponent<Car>();
		if (GameState.instance.getGameState == GameState.StateOfGame.play) {
			if (car != null && car.numberOfIntersectionsEntered >= 3) {
				// make the indicator
				PassedIndicator newIndicator = Instantiate (indicatorPrefab, Camera.main.WorldToScreenPoint (car.transform.position), Quaternion.identity).GetComponent<PassedIndicator> ();
				newIndicator.transform.SetParent (GameObject.Find ("Canvas").transform);
				newIndicator.Target = car.transform;

				// add point
				Score.instance.AddScore ();
			}
		}
	}
}
