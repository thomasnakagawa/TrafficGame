using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Utils {
	public static Direction RotateClockwise(Direction dir) {
		switch (dir) {
		case Direction.North:
			return Direction.East;
		case Direction.East:
			return Direction.South;
		case Direction.South:
			return Direction.West;
		case Direction.West:
			return Direction.North;
		default:
			throw new Exception ("Invalid direction");
		}
	}

	public static Direction RotateCounterClockwise(Direction dir) {
		switch (dir) {
		case Direction.North:
			return Direction.West;
		case Direction.East:
			return Direction.North;
		case Direction.South:
			return Direction.East;
		case Direction.West:
			return Direction.South;
		default:
			throw new Exception ("Invalid direction");
		}
	}

	public static float DirectionToAngle(Direction dir) {
		switch (dir) {
		case Direction.North:
			return 0f;
		case Direction.East:
			return 90f;
		case Direction.South:
			return 180f;
		case Direction.West:
			return 270f;
		default:
			throw new Exception ("Invalid direction");
		}
	}

	public static Direction AngleToDirection(float angle) {
		switch ((int)angle) {
		case 0:
			return Direction.North;
		case 90:
			return Direction.East;
		case 180:
			return Direction.South;
		case 270:
			return Direction.West;
		default:
			throw new Exception ("Invalid angle");
		}
	}

	public static Direction OppositeDirection(Direction dir) {
		switch (dir) {
		case Direction.North:
			return Direction.South;
		case Direction.East:
			return Direction.West;
		case Direction.South:
			return Direction.North;
		case Direction.West:
			return Direction.East;
		default:
			throw new Exception ("Invalid direction");
		}
	}

	public static Direction RandomDirection() {
		return new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West } [UnityEngine.Random.Range (0, 4)];
	}

	public static int DirectionToIndex(Direction dir) {
		return (int)dir;
	}
}
