using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedIndicator : MonoBehaviour {
	[SerializeField] private float minSize = 0.2f;
	[SerializeField] private float maxSize = 0.5f;

	public Transform Target;

	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.one * minSize;
		GetComponent<AudioSource> ().pitch = Random.Range (0.9f, 1.1f);
	}

	// Update is called once per frame
	void Update () {
		if (GameState.instance.getGameState == GameState.StateOfGame.ending) {
			Destroy (gameObject);
			return;
		}
		transform.position = Camera.main.WorldToScreenPoint (Target.position);
		transform.localScale = Vector3.one * (transform.localScale.x + (Time.deltaTime * 0.5f));
		if (transform.localScale.x > maxSize) {
			Destroy (gameObject);
		}
	}
}
