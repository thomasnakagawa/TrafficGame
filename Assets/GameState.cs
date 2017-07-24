using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameState : MonoBehaviour {
	[SerializeField] GameObject endingCanvas;

	public static GameState instance;

	[SerializeField] private StateOfGame stateOfGame;
	public enum StateOfGame {
		play,
		ending,
		menu
	}

	void Awake() {
		if (instance != null) {
			throw new Exception ("Only one GameState instance should exist");
		}
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Q) && Input.GetKeyDown (KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha9)) {
			SetGameState(StateOfGame.ending);
		}
		if (Input.GetKeyDown (KeyCode.Escape) && stateOfGame != StateOfGame.menu) {
			SceneManager.LoadScene (0);
		}
	}

	public void SetGameState(StateOfGame state) {
		stateOfGame = state;
		switch (stateOfGame) {
		case StateOfGame.ending:
			// set camera to move to ending position
			Camera.main.GetComponent<CameraMovement> ().SetTarget (GameObject.Find ("EndingCamera").transform);

			// stop the music
			GetComponent<CarSpawn> ().StopMusic ();

			// hide the old ui
			GetComponent<Score>().hideScore();

			// start the ending canvas
			endingCanvas.SetActive(true);

			break;
		}
	}

	public StateOfGame getGameState {
		get {
			return stateOfGame;
		}
	}
}
