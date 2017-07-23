using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;

public class Patience : MonoBehaviour {
	[SerializeField] private float initialPatience = 5f;
	[SerializeField] private GameObject indicatorPrefab;

	public static Patience instance;

	private RectTransform timePanel;
	private float initialWidth;
	private float patienceRemaining;
	private Dictionary<Car, PatienceIndicator> carIndicators;

	void Awake() {
		if (instance != null) {
			throw new Exception ("Only one patience instance should exist");
		}
		instance = this;
	}

	// Use this for initialization
	void Start () {
		timePanel = GameObject.Find ("TimePanel").GetComponent<RectTransform> ();
		Assert.IsNotNull (timePanel);
		initialWidth = timePanel.sizeDelta.x;
		patienceRemaining = initialPatience;
		carIndicators = new Dictionary<Car, PatienceIndicator> ();
	}

	public void losePatience(float time) {
		patienceRemaining -= time;
		timePanel.sizeDelta = new Vector2 (
			Mathf.Lerp (0, initialWidth, patienceRemaining / initialPatience),
			timePanel.sizeDelta.y
		);

		if (patienceRemaining <= 0f) {
			// end game
		}
	}

	public void showPatienceIndicator(Car car) {
		if (carIndicators.ContainsKey(car) == false) {
			PatienceIndicator newIndicator = Instantiate (indicatorPrefab, Camera.main.WorldToScreenPoint (car.transform.position), Quaternion.identity).GetComponent<PatienceIndicator>();
			newIndicator.transform.SetParent (GameObject.Find ("Canvas").transform);
			newIndicator.Target = car.transform;
			carIndicators.Add (car, newIndicator);
		}
	}

	public void hidePatienceIndicator(Car car) {
		if (carIndicators.ContainsKey(car)) {
			Destroy (carIndicators [car].gameObject);
			carIndicators.Remove (car);
		}
	}
}
