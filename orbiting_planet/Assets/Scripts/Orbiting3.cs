using UnityEngine;
using System;

public class Orbiting3 : MonoBehaviour {

	// Using Bresenham's circle tracing algorigthm

	public float speed = 10f;

	private float radius;
	private float radius2; // radius square
	private float x, y, m;

	public void Start() {
		radius = Math.Abs(transform.position.z);
		radius2 = radius*radius;

		x = 0;
		y = radius;
		m = 5 - 4*radius;
	}

	public void Update() {
		if (x <= y) {
			// ...
		}
	}

	private void affectPosition() {
		// ...
	}
}
