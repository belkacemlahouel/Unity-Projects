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

	private readonly float LIMIT = 5f;			// for random positions
	private readonly float EPSILON = 0.01f;
	private readonly float WAITING = 2f;
	public float TIMER;

	public Vector3 target;
	public Vector3 previous;			// previous position
	public Vector3 vector;				// current 3-axis direction
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
		TIMER = Random.Range(7f, 14f);
		waiting = 0f;
		speed = 0f;
		direction = 0f;
		previous = transform.position;
		vector = Vector3.zero;

		newTarget();
	}

	public void Update() {
		vector = transform.position-previous;
		previous = transform.position;

		if (timer > TIMER) {
			timer = 0f;
			TIMER = Random.Range(7f, 14f);
			speed = 0f; // !!! No transition?
			setAnimatorSpeed();
			newTarget();
		} else if (weArrived()) {
			// Improvement: boolean variable to keep this evaluation
			// then, we just have to test the variable if true.

			// Small transition to Idle state and wait a small time frame
			// Then search one new target position to go to

			waiting += Time.deltaTime;

			if (waiting == 0f) {
				speed = 0f;
				setAnimatorSpeed();
			}

			if (waiting > WAITING) {
				// We can look for another target to go to
				newTarget();
				waiting = 0f;
			}

		} else {
			timer += Time.deltaTime;

			// We keep trying to go there (to the target position)
			// We keep the speed rising until we come

			// Improvement: correct computation of the speed.
			// Exponential model > regular acceleration model.

			if (distanceTo(target) < (distance/2) && speed > 0f) {
				speed -= 0.01f;
			} else if (speed < 10f) {
				speed += 0.01f;
			}
			setAnimatorSpeed();

			// ****************************************************************
			// ****************************************************************
			// ****************************************************************


			// Determining the direction
			// We always consider the case where the target is ahead
			if (isBehind()) {
				/*if (target.x > transform.position.x) {
					// || System.Math.Abs(transform.position.z) > limit ||
					// System.Math.Abs(transform.position.x) > limit) {
					// The target is behind and on the right.
					// Or we reached one wall
					direction = 1f;
				} else {
					// The target is behind and on the left.
					direction = -1f;
				}*/

				direction = (float) (Vector3.Angle(target, vector)/180f);
				if (target.x > transform.position.x) {
					direction *= -1;
				}

				// if (System.Math.Abs(direction) < 0.01f) {
				// 	Debug.Log("ANGLE " + direction);
				// }

				speed = 1;
			} else {
				// We use trigonometry rules here
				// cos(T) = Adj/Hyp and Hyp ~ distanceToTarget
				// (because we work in 2D, and the third is useless: y-axis)
				direction = (float) System.Math.Cos(
								(target.x - transform.position.x)
								/ distanceToTarget());

				// if (System.Math.Abs(direction) < 0.01f) {
				// 	Debug.Log("TRIGO " + direction);
				// }
			}

			/*direction = System.Math.Min(0.3f, (float) System.Math.Cos(
								(target.x - transform.position.x)
								/ distanceToTarget()));
			direction = System.Math.Min(direction,
								(float) (Vector3.Angle(target, vector)));*/

			// ****************************************************************
			// ****************************************************************
			// ****************************************************************



			setAnimatorDirection();
		}
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
	 * is smaller than EPSILON (times X).
	 * This is another way to check if we are almost arrived.
	 ***/

	private bool weArrived() {
		return distanceTo(target) < 2f;
	}

	/***
	 * Checks if we are arrived to the target position
	 * We can have an EPSILON error on both axes.
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

	/***
	 * Computes whether the target position is behind or not
	 * The axis are character-centered
	 ***/

	private bool isBehind() {
		if (vector != Vector3.zero) {
			float angle = Vector3.Angle(target, vector);
			return angle > 90f;
		}

		return false;
	}
}
