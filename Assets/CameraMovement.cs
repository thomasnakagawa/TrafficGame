using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	private Transform targetTransform;
	
	// Update is called once per frame
	void Update () {
		if (targetTransform != null) {
			transform.position = Vector3.Lerp (transform.position, targetTransform.position, 2 * Time.deltaTime);
			transform.rotation = Quaternion.Lerp (transform.rotation, targetTransform.rotation, 2 * Time.deltaTime);
		}
	}

	public void SetTarget(Transform target) {
		targetTransform = target;
	}
}
