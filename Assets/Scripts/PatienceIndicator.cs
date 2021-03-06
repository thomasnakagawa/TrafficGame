﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceIndicator : MonoBehaviour {
	[SerializeField] private float minSize = 0.2f;
	[SerializeField] private float maxSize = 0.5f;

	public Transform Target;

	private bool growing;

	// Use this for initialization
	void Start () {
		growing = true;
		transform.localScale = Vector3.one * minSize;
		GetComponent<AudioSource> ().pitch = Random.Range (0.9f, 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameState.instance.getGameState == GameState.StateOfGame.ending) {
			Destroy (gameObject);
			return;
		}

		GetComponent<AudioSource> ().pitch += (Time.deltaTime * 0.1f);

		transform.position = Camera.main.WorldToScreenPoint (Target.position);
		if (growing) {
			transform.localScale = Vector3.one * (transform.localScale.x + (Time.deltaTime * 0.5f));
			if (transform.localScale.x >= maxSize) {
				growing = false;
			}
		} else {
			transform.localScale = Vector3.one * (transform.localScale.x - (Time.deltaTime * 0.5f));
			if (transform.localScale.x <= minSize) {
				growing = true;
			}
		}
	}
}
