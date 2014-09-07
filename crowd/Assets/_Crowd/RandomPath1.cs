using UnityEngine;
using System.Collections;

/***
 * This script makes the Robot move towards one random position.
 * When its comes there, it just searches for a new random position
 * to go to.
 * For the moment, there is no trajectory to follow and also no
 * time constraints.
 ***/

public class RandomPath1 : MonoBehaviour {

	private readonly float LIMIT = 24f;			// for random positions
	private readonly float EPSILON = 0.01f;
	private readonly float WAITING = 2f;
	public float TIMER;

	public Vector3 target;
	public Vector3 vector;				// current 3-axis direction, vision
	public float alpha;					// rotation angle towards Y-axis, in degrees
	private float limit;
	public float timer;					// can't reach the target
	private float waiting;				// waiting after arrival
	private float distance;				// distance to target, when new target
	public float speed;
	public float direction;
	private Animator animator;

	public void Start() {
		animator = GetComponent<Animator>();

		limit = LIMIT-EPSILON;
		timer = 0f;
		TIMER = Random.Range(7f, 14f);	// just my decision behind this line
		waiting = 0f;
		speed = 0f;
		direction = 0f;
		alpha = 0f;
		computeVector();

		newTarget();

		/*
		 * Initializations used in tests
		 */
		
		// speed = 1f;
		// setAnimatorSpeed();
	}

	public void Update() {
		/*
		 * Finds a newTarget and says whether it is behind or not
		 * to check (manually) if it corresponds
		 */
		newTarget();
		computeVector();
		Debug.Log("position: " + transform.position +
					" target: " + target +
					" alpha: " + alpha +
					" vector: " + vector +
					" | isTargetBehind: " + isTargetBehind());
	}

	/***
	 * Computes the Robot's vision vector
	 * For the moment, the length of this vector is 1
	 * Improvement: add one particluar length to this vector (speed)
	 ***/

	private void computeVector() {
		alpha = Vector3.Angle(transform.position, Vector3.forward);

		Vector3 vectorX = Vector3.right * (float) System.Math.Cos(alpha);
		Vector3 vectorZ = Vector3.forward * (float) System.Math.Sin(alpha);

		Debug.Log("vectorX: " + vectorX + " vectorZ: " + vectorZ);

		vector = vectorX + vectorZ;
	}

	/***
	 * Computes whether the target position is behind or not
	 * using the directionnal vector of the Robot movement.
	 * The axis are character-centered.
	 ***/

	private bool isTargetBehind() {
		if (vector != Vector3.zero) {
			return alpha > 90f;				// What is alpha interval...
		}

		return false;
	}

	/***
	 * Finds randomly a new target position
	 * for the Robot to go to.
	 ***/

	private void newTarget() {
		target = new Vector3(Random.Range(-limit, limit),
							 0,
							 Random.Range(-limit, limit));

		distance = distanceTo(target);
	}

	/***
	 * Checks if the distance to the arrival (target position)
	 * is smaller than a certain number (e.g. ESPILON*2).
	 * This is another way to check if we are almost arrived.
	 ***/

	private bool weArrived() {
		return distanceTo(target) < 2f;
	}

	/***
	 * Checks if we are arrived to the target position
	 * We can have an certain X error on both axes.
	 ***/

	private bool weClose() {
		return  closeTo(target.x, transform.position.x) &&
				closeTo(target.z, transform.position.z);
	}

	/***
	 * Compares whether a and b are close or not
	 * with an EPSILON factor.
	 * a is the value we want to test and
	 * b is the target value.
	 ***/

	private bool closeTo(float a, float b) {
		return  (b+EPSILON) > a &&
				(b-EPSILON) < a;
	}

	/***
	 * Sets the value in parameter to the Animator parameter "Speed"
	 ***/

	private void setAnimatorSpeed() {
		animator.SetFloat("Speed", speed);
	}

	/***
	 * Sets the value in parameter to the Animator parameter "Direction"
	 ***/

	private void setAnimatorDirection() {
		animator.SetFloat("Direction", direction);
	}

	/***
	 * Computes the cartesian distance from "here" to target position
	 * uses distanceTo(Vector3).
	 * This provides another way to use distanceTo(Vector3) method.
	 ***/

	private float distanceToTarget() {
		return distanceTo(target);
	}

	/***
	 * Computes the cartesian distance from "here" to position
	 * which is in parameter.
	 ***/

	private float distanceTo(Vector3 position) {
		return (float) System.Math.Sqrt(
		(position.x-transform.position.x) * (position.x-transform.position.x) +
		(position.y-transform.position.y) * (position.y-transform.position.y) +
		(position.z-transform.position.z) * (position.z-transform.position.z));
	}
}
