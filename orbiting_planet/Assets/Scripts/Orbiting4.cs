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

	public float speed = 10000f;
	public int precision = 5; // n+1 number of points

	private float speedInv;
	private double r, r2; // radius and radius square
	private int iter = 0;
	private float[] zPoints, yPoints;
	private float deltaTime = 0f;
	private bool TOP = true;

	public void Start() {
		r = Math.Abs(transform.position.z);
		r2 = r*r;

		precision = Math.Max(precision, 5);
		zPoints = new float[precision];
		yPoints = new float[precision];

		computeTchebychevRoots();
		computeImages();
	}

	/***
	 * We describe a whole circular trajectory.
	 * But, we only use half of the array containing
	 * Tchebychev roots.
	 *
	 * @bug
	 * We may have one/two NaN while computing
	 * Techbychev roots. The mistake appears only
	 * later, when we make the planet move.
	 ***/
	public void Update() {
		speedInv = (float) (1/speed);

		if (deltaTime > speedInv) { // smoother image
			deltaTime = 0;

			if (iter <= 0 || iter >= precision) {
				iter = 0;
				TOP = true;
			}

			if (iter >= precision/2) {
				TOP = false;
			}

			// Updating the position of the sphere to the next
			// position.
			if (TOP) {
				Vector3 newPosition = new Vector3
					(transform.position.x, yPoints[++iter], zPoints[iter]);
				transform.position = newPosition;
			} else {
				Vector3 newPosition = new Vector3
					(transform.position.x, -yPoints[--iter], zPoints[iter]);
				transform.position = newPosition;
			}

			// Updating the count of the current iteration:
			// is now inside previous condition using
			// pre-incrementation operator properties.
		} else {
			deltaTime += Time.deltaTime;
		}
	}

	// xk = cos(2k+1/2(n+1) * PI) for k in 0..n
	// if we have n+1 points (precision)
	private void computeTchebychevRoots() {
		for (int i = 0; i < precision; ++i) {
			zPoints[i] = (float)
				(Math.Cos(2*((i+0.5)/(precision+1)) * Math.PI) * r);
		}
	}

	// y = sqrt(r^2 - x^2)
	private void computeImages() {
		for (int i = 0; i < precision; ++i) {
			yPoints[i] = (float)
				Math.Sqrt(r2 - zPoints[i]*zPoints[i]);

			/***
			 * Only because when this condition is verified,
			 * we have r2 - zPoints[i]*zPoints[i] < 0
			 * and we suppose this comes from a lack of precision
			 * that's why we decided to affect r to yPoints[i].
			 *
			 * EDIT
			 * Using double (instead of float) type for "r" makes
			 * this error disappear.
			 * If you encounter this error again, you may disable
			 * for the following lines.
			 ***/
			// if (Double.IsNaN(yPoints[i]))
			// 	yPoints[i] = (float) r;
		}
	}
}
