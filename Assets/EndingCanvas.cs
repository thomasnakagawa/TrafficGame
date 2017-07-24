using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;

public class EndingCanvas : MonoBehaviour {
	private RectTransform leaderboardView;
	private RectTransform gameoverView;

	private bool gotScores = false;
	private LeaderboardData leaderboardData;

	// Use this for initialization
	void Start () {
		// get the two panes
		gameoverView = (RectTransform)transform.Find("GameoverView");
		leaderboardView = (RectTransform)transform.Find("LeaderboardView");
		gameoverView.gameObject.SetActive (true);
		leaderboardView.gameObject.SetActive (true);

		// update text fields
		int score = Score.instance.getScore;
		gameoverView.transform.Find("YourScore").GetComponent<Text>().text = "You directed " + score.ToString() + " car" + (score == 1 ? "" : "s") + " through your city!";

		if (PlayerPrefs.HasKey ("Name")) {
			GameObject.Find ("InputField").GetComponent<InputField> ().text = PlayerPrefs.GetString ("Name");
		}

		leaderboardView.transform.Find("LeaderboardText").GetComponent<Text>().text = "Loading...";
		leaderboardView.transform.Find ("YourScore").GetComponent<Text> ().text = "Your score: " + score.ToString ();

		leaderboardView.gameObject.SetActive (false);

		// set focus on the input field
		EventSystem.current.SetSelectedGameObject(GameObject.Find ("InputField"));

		// get leaderboard data to get scores from
		leaderboardData = GameObject.Find("LeaderboardData").GetComponent<LeaderboardData>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!gotScores) {
			string scores = leaderboardData.GetScoreString ();
			if (scores != null) {
				leaderboardView.transform.Find ("LeaderboardText").GetComponent<Text> ().text = scores;
				gotScores = true;
			}
		}
	}

	public void OnNameConfirm() {
		string input = GameObject.Find ("InputField").GetComponent<InputField> ().text;
		if (input.Length > 0) {
			// save name for next time
			PlayerPrefs.SetString("Name", input);

			// submit name to leaderboard and retrieve data
			leaderboardData.UploadScore(input, Score.instance.getScore); 

			// now show leaderboard
			switchToLeaderboardView ();
		}
	}

	public void OnNameSkip() {
		switchToLeaderboardView ();
		leaderboardData.FetchScores ();
	}

	private void switchToLeaderboardView() {
		gameoverView.gameObject.SetActive (false);
		leaderboardView.gameObject.SetActive (true);
	}

	public void OnReplayClicked() {
		SceneManager.LoadScene (1);
	}

	public void OnGotoMenu() {
		SceneManager.LoadScene (0);
	}
}
