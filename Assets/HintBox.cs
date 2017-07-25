using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintBox : MonoBehaviour {

	public void onHover() {
		transform.Find ("Hint").GetComponent<Text> ().enabled = true;
		transform.Find ("HowTo").GetComponent<Text> ().enabled = false;
	}

	public void onUnhover() {
		transform.Find ("Hint").GetComponent<Text> ().enabled = false;
		transform.Find ("HowTo").GetComponent<Text> ().enabled = true;
	}
}
