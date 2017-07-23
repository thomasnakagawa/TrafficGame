using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public class Intersection : MonoBehaviour {
	[SerializeField] private bool initializeRandom = true;
	[SerializeField] private AudioClip[] clickSounds;

	public Direction FlowDirection { get; private set; }

	private List<Renderer> arrowRenders;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		// get the arrow renderers to be able to change their color
		arrowRenders = new List<Renderer>();
		foreach (Transform child in transform) {
			if (child.name.StartsWith ("Arrow")) {
				foreach (Renderer render in child.GetComponentsInChildren<Renderer> ()) {
					arrowRenders.Add (render);
				}
			}
		}
		Assert.IsTrue (arrowRenders.Count > 1);

		// start with a random direction
		if (initializeRandom) {
			FlowDirection = Utils.RandomDirection ();
		} else {
			FlowDirection = Utils.AngleToDirection (transform.eulerAngles.y);
		}
		StartCoroutine (spinToDirection (FlowDirection));

		audioSource = GetComponent<AudioSource> ();
	}

	// called when clicked
	public void Rotate() {
		FlowDirection = Utils.RotateClockwise (FlowDirection);
		audioSource.PlayOneShot (clickSounds [Utils.DirectionToIndex(FlowDirection)]);
		StartCoroutine (spinToDirection (FlowDirection));
	}

	private IEnumerator spinToDirection(Direction dir) {
		// turn to face new direction
		transform.eulerAngles = new Vector3 (0f, Utils.DirectionToAngle (dir), 0f);

		// update arrows color
		Material newMat = ColorSource.instance.GetDirectionMaterial (dir);
		foreach (Renderer render in arrowRenders) {
			render.material.color = newMat.color;
		}
		yield return null;
	}
}
