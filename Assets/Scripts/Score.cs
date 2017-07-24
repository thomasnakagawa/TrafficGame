using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour {
	public static Score instance;
	private int score = 0;
	private Text scoreText;

	void Awake() {
		if (instance != null) {
			throw new Exception ("Only one score instance should exist");
		}
		instance = this;
	}

	// Use this for initialization
	void Start () {
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		showScore ();
	}

	public void AddScore() {
		score += 1;
		showScore ();
	}

	private void showScore() {
		scoreText.text = "Cars passed: " + score.ToString ();
	}

	public void hideScore() {
		scoreText.enabled = false;
	}

	public int getScore {
		get {
			return score;
		}
	}
}
