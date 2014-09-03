using UnityEngine;
using System.Collections;

public class RandomPath : MonoBehaviour {

	// ************* todo *************
	// A pointer on the board to set the limit up properly
	// A key to press in order to start the animation
	// Set the timer to not change that much directions...
	// More randomness when setting directions/speeds ?
	// ********************************

	// ******* how does it work *******
	// Chooses one point on the plane area 	/!\ TODO
	// Goes left/right until the x-coordinate is reached
	// It has to turn for a while... 		/!\ TODO
	// Then it goes for-/backward until the z-coordinate is reached
	//
	// Finally, another target point is chosen
	// ********************************

	private readonly float EPSILON = 0.01f;
	private readonly float LIMIT = 5f;
	private readonly float TIMER = 1f;
	private float limit;
	private float x, z;
	private float direction, speed;
	private Animator animator;
	private float timerDirection = 0, timerSpeed = 0;

	public void Start() {
		limit = LIMIT-EPSILON;
		animator = GetComponent<Animator>();
		newTarget();
	}
	
	public void Update() {
		if (areWeThere()) {
			newTarget();
		} else {
			goThere();
		}
	}

	private void newTarget() {
		x = Random.Range(-limit, limit);
		z = Random.Range(-limit, limit);
	}

	private bool areWeThere() {
		return 	x == transform.position.x &&
				z == transform.position.z;
	}

	private void goThere() {
		if (x != transform.position.x && timerDirection > TIMER) {
			timerDirection = 0;
			newTarget();

			if (x > transform.position.x) {
				goRight();
			} else {
				goLeft();
			}
		} else {
			timerDirection += Time.deltaTime;
		}

		if (z != transform.position.z && timerSpeed > TIMER) {
			timerSpeed = 0;
			newTarget();

			if (z > transform.position.z) {
				goForward();
			} else {
				goBackward();
			}
		} else {
			timerSpeed += Time.deltaTime;
		}
	}

	private void goRight() {
		animator.SetFloat("Direction", 0.5f);
	}

	private void goLeft() {
		animator.SetFloat("Direction", -0.5f);
	}

	private void goForward() {
		animator.SetFloat("Speed", 0.5f);
	}

	private void goBackward() {
		animator.SetFloat("Speed", -0.5f);
	}
}
