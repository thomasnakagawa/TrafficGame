using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorSource : MonoBehaviour {
	[SerializeField] private Material NorthSouthMat;
	[SerializeField] private Material EastWestMat;

	public static ColorSource instance;

	void Awake() {
		if (instance != null) {
			throw new Exception ("Only one color source instance should exist");
		}
		instance = this;
	}

	public Material NorthSouthMaterial {
		get {
			return NorthSouthMat;
		}
	}

	public Material EastWestMaterial {
		get {
			return EastWestMat;
		}
	}

	public Material GetDirectionMaterial(Direction dir) {
		switch (dir) {
		case Direction.North:
		case Direction.South:
			return NorthSouthMat;
		case Direction.East:
		case Direction.West:
			return EastWestMat;
		default:
			throw new Exception ("Invalid direction");
		}
	}
}
