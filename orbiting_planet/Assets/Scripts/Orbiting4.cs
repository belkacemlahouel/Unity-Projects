using UnityEngine;
using System;

/***
 * Using Tchebychev repartition of points (roots)
 * and Lagrange interpolation technique,
 * this class tries to interpolate:
 * y = sqrt(r^2 - x^2) on [0, r].
 *
 * Using symetry properties, we can replicate
 * this quarter of circle.
 *
 * For n+1 points, we have
 * Tchebychev's (Tn+1) polynom's roots:
 * xk = cos(2k+1/2(n+1) * PI) for k in 0..n
 *
 * This class makes one sphere (or another
 * GameObject) rotate around (0, 0, 0) on
 * the plane directed by the axes (z, y).
 *
 * @author Belkacem Lahouel
 * UTBM: University of Technology of Belfort-Montbeliard
 * Summer of 2014.
 ***/

public class Orbiting4 : MonoBehaviour {

	public float speed = 0.000002f;
	public int precision = 5; // n+1 number of points

	private float r, r2; // radius and radius square
	private int iter = 0;
	private float[] zPoints, yPoints;
	private float deltaTime = 0f;

	public void Start() {
		r = Math.Abs(transform.position.z);
		r2 = r*r;

		precision = Math.Max(precision, 5);
		zPoints = new float[precision];
		yPoints = new float[precision];

		Debug.Log("Bonjour");

		computeTchebychevRoots();
		computeImages();
	}

	/***
	 * @todo
	 * For the moment, this only interpolates
	 * on the first quarter of the circle.
	 * Hence, we keep going on the first quarter for now on.
	 ***/
	public void Update() {
		if (deltaTime > 0) {
			deltaTime = 0;

			if (iter == precision) {
				iter = 0;
			}

			// Updating the position of the sphere to the next
			// position.
			Vector3 newPosition = new Vector3
				(transform.position.x, zPoints[iter], yPoints[iter]);
			transform.position = newPosition;

			// Updating the count of the current iteration.
			++iter;
		} else {
			deltaTime += Time.deltaTime;
		}
	}

	// xk = cos(2k+1/2(n+1) * PI) for k in 0..n
	// if we have n+1 points (precision)
	private void computeTchebychevRoots() {
		for (int i = 0; i < precision; ++i) {
			zPoints[i] = (float)
				Math.Cos(2*((i+0.5)/(precision+1)) * Math.PI) * r;
			Debug.Log("zPoints[" + i + "]: " + zPoints[i]);
		}
	}

	// y = sqrt(r^2 - x^2)
	private void computeImages() {
		for (int i = 0; i < precision; ++i) {
			yPoints[i] = (float)
				Math.Sqrt(r*r - zPoints[i]*zPoints[i]);
			Debug.Log("yPoints[" + i + "]: " + yPoints[i]);
		}
	}
}
