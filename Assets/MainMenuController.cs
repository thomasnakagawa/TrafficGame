using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
	private bool gotScores = false;
	private LeaderboardData leaderboardData;

	void Start() {
		leaderboardData = GameObject.Find("LeaderboardData").GetComponent<LeaderboardData>();
		transform.Find ("LeaderPanel").Find ("LeaderText").GetComponent<Text> ().text = "Loading...";
	}

	void Update() {
		if (!gotScores) {
			string scores = leaderboardData.GetScoreString ();
			if (scores != null) {
				transform.Find ("LeaderPanel").Find("LeaderText").GetComponent<Text> ().text = scores;
				gotScores = true;
			}
		}
	}

	public void OnStart() {
		SceneManager.LoadScene (1);
	}

	public void OnQuit() {
		Application.Quit ();
	}

	public void OnToggleLeaderboard() {
		transform.Find ("LeaderPanel").gameObject.SetActive (!transform.Find ("LeaderPanel").gameObject.activeSelf);
		leaderboardData.FetchScores ();
	}
}
