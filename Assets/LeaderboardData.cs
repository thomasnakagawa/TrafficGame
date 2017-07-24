using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class LeaderboardData : MonoBehaviour {
	public static LeaderboardData instance;

	void Awake() {
		if (instance != null) {
			throw new Exception ("Only one LeaderboardData instance should exist");
		}
		instance = this;
	}

	// Use this for initialization
	void Start () {
		// get the private code from file
		/*
		StreamReader streamReader = new StreamReader(Application.dataPath + "/privateCode.txt");
		string privateCode = streamReader.ReadToEnd ();
		streamReader.Close ();
		*/
		TextAsset textAsset = Resources.Load ("privateCode") as TextAsset;
		string privateCode = textAsset.text;

		GetComponent<dreamloLeaderBoard> ().privateCode = privateCode;
	}

	public void UploadScore(string name, int score) {
		GetComponent<dreamloLeaderBoard> ().AddScore (name, score);
	}

	public string GetScoreString() {
		List<dreamloLeaderBoard.Score> scores = GetComponent<dreamloLeaderBoard> ().ToListHighToLow ();
		if (scores == null || scores.Count < 1) {
			return null;
		}

		// there are scores, format them into a string
		int index = 1;
		return string.Join("\n",
			scores.Take(10).Select(score => 
				((index > 9 ? "" : " ") // posible space before rank
				+ (index++).ToString()    // number of rank. ++ to increment for next in list
				+ ". "					  // . to seperate rank from score
				+ score.score.ToString()  // actual score
				+ " car" + (score.score == 1 ? "" : "s") // unit of score
				+ " | "					  // seperator between score and name
				+ score.playerName		  // name
				+ (PlayerPrefs.HasKey("Name") && score.playerName == PlayerPrefs.GetString("Name") ? "<-" : "") // arrow to show if score belongs to current player
				)
			).ToArray()
		);
	}

	public void FetchScores() {
		GetComponent<dreamloLeaderBoard> ().LoadScores ();
	}
}
