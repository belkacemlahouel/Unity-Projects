using UnityEngine;
using System.Collections;

public class RandomPath : MonoBehaviour {

	/* ************* todo *************
	 * A pointer on the board to set the limit up properly
	 * A key to press in order to start the animation
	 * Set the timer to not change that much directions...
	 * More randomness when setting directions/speeds ?
	 * Everybody needs to keep walking forward for the moment...
	 * ********************************/

	/* ******* how does it work *******
	 * Chooses one point on the plane area 	/!\ TODO
	 * Goes left/right until the x-coordinate is reached
	 * It has to turn for a while... 		/!\ TODO
	 * Then it goes for-/backward until the z-coordinate is reached
	 *
	 * Finally, another target point is chosen
	 * ********************************/

	private readonly float EPSILON = 0.1f;
	private readonly float LIMIT = 5f;
	private readonly float TIMER = 5f;
	private float limit;
	private float x, z;
	public float direction, speed;
	private Animator animator;
	private float timerDirection, timerSpeed;

	public void Start() {
		limit = LIMIT-EPSILON;
		timerDirection = TIMER+1f;
		timerSpeed = TIMER+1f;
		animator = GetComponent<Animator>();
		newTarget();
		// computeDirectionSpeed();
	}
	
	public void Update() {
		if (areWeAlmostThere()) {
			newTarget();
		} else {
			goThere();		}
	}

	private void newTarget() {
		x = Random.Range(-limit, limit);
		z = Random.Range(-limit, limit);

		Debug.Log("newTarget : " + x + ", " + z);
	}

	private bool areWeAlmostThere() {
		bool tmp =  (transform.position.x < x+EPSILON || transform.position.x > x-EPSILON) &&
					(transform.position.z < z+EPSILON || transform.position.z > z-EPSILON);
		Debug.Log("areWeAlmostThere : " + tmp);

		return 	tmp;
	}

	/***
	 * This system uses trigonometry to determine the values for speed and direction
	 * The movement is still anarchic bacause random
	 * @bugged

	private void goThere() {
		Debug.Log("goThere");

		computeDirectionSpeed();

		if (x != transform.position.x) {
			animator.SetFloat("Speed", speed);
		}

		if (z != transform.position.z) {
			animator.SetFloat("Direction", direction);
		}
	}

	private void computeDirectionSpeed() {
		float hypothenuse, oppose, adjacent;

		adjacent = x - transform.position.x;
		oppose = z - transform.position.z;

		hypothenuse = (float) System.Math.Sqrt(adjacent*adjacent + oppose*oppose);

		speed = adjacent/hypothenuse * 2f; // Be careful, it can be very small
		direction = oppose/hypothenuse * 2f;
	}

	***/

	/***
	 * First kind of anarchic movement, using complete randomness
	 ***/

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
