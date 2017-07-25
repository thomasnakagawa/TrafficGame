using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

public class CarSpawn : MonoBehaviour {
	[SerializeField] private GameObject carPrefab;
	[SerializeField] private float maxTimeBetweenSpawn = 3f;
	[SerializeField] private float minTimeBetweenSpawn = 0.1f;
	[SerializeField] private AnimationCurve difficultyCurve;
	[SerializeField] private float curveTimeLength = 180f;

	private Transform[] spawnPoints;

	private float elapsedTime;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		spawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint")
			.Select(point => point.transform).ToArray();

		Assert.IsTrue (spawnPoints.Length > 0);
		StartCoroutine (spawnAfterTime());

		elapsedTime = 0f;
	}

	void Update() {
		if (audioSource != null) {
			audioSource.pitch += Time.deltaTime * 0.00085f;
		}
	}

	private IEnumerator spawnAfterTime() {
		if (GameState.instance.getGameState == GameState.StateOfGame.ending) {
			yield break;
		}

		float timeUntilNext = timeUntilNextSpawn ();
		yield return new WaitForSeconds (timeUntilNext);
		elapsedTime += timeUntilNext;

		Transform chosenPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
		Instantiate (carPrefab, chosenPoint.position, chosenPoint.rotation);
		StartCoroutine (spawnAfterTime());
	}

	private float timeUntilNextSpawn() {
		return  minTimeBetweenSpawn + (difficultyCurve.Evaluate (elapsedTime / curveTimeLength) * maxTimeBetweenSpawn);
	}

	public void StopMusic()  {
		audioSource.Stop ();
	}
}
