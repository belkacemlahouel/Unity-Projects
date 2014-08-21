using UnityEngine;
using System;

public class Orbiting2 : MonoBehaviour {

	// This Orbiting script makes the planet orbiting vertically.
	// Therefore, we use the plane formed by the vectors (y, z).
	// When this script works on 2 dimensions, we can add a third dimension.

	// y
	// x z

	public float speed = 1f;

	private float radius;
	private float radius_square;
	private bool PLUS; // Whether we substract or not from the first coord.

	public void Start() {
		radius = Math.Abs(transform.position.z);
		radius_square = radius*radius;
		PLUS = false;
	}

	public void Update() {
		float move = Time.deltaTime*speed;

		float x, y, z;

		x = 0;

		if (!PLUS) {
			z = transform.position.z - move;
		} else {
			z = transform.position.z + move;
		}

		if (Math.Abs(z) > radius) {
			if (z < 0) {
				z = -radius;
				PLUS = true;
			} else if (z > 0) {
				z = radius;
				PLUS = false;
			}
		}

		double tmp = Math.Sqrt((double) (radius_square - z*z));

		// if (PLUS) {
			if (!Double.IsNaN(tmp)) {
				y = (float) tmp;
			} else {
				y = (float) Math.Sqrt((double) (z*z - radius_square));
			}
		/*} else {
			if (!Double.IsNaN(tmp)) {
				y = (float) -tmp;
			} else {
				y = (float) -Math.Sqrt((double) (z*z - radius_square));
			}
		}*/

		transform.position = new Vector3(x, y, z);
	}
}
