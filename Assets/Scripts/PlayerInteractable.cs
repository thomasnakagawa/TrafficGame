using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class PlayerInteractable : MonoBehaviour {
	[SerializeField] private UnityEvent onClick;

	private GameObject hoverIndicator;

	// Use this for initialization
	void Start () {
		hoverIndicator = transform.Find ("HoverIndicator").gameObject;
		Assert.IsNotNull (hoverIndicator);
		hoverIndicator.SetActive (false);
	
		Assert.IsNotNull (onClick);
	}

	public void Clicked() {
		onClick.Invoke ();
	}

	public void Hovered() {
		hoverIndicator.SetActive (true);
	}

	public void UnHovered() {
		hoverIndicator.SetActive (false);
	}
}
