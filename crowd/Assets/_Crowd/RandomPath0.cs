using UnityEngine;
using System.Collections;

/***
 * This script makes the Robot move always forward
 * after one time frame "TIMER" (approx.),
 * it will change its direction.
 * This script is done (for the moment)
 * in order to make the Robot move naturally,
 * so there is not any possibility for an odd
 * move like turning around one point...
 * And the choice of direction is completely random.
 ***/

public class RandomPath0 : MonoBehaviour {

	private readonly float EPSILON = 0.01f;
	private readonly float TIMER = 3f;
	private readonly float LIMIT = 5f;
	private float x, z, timer, limit;
	private Animator animator;

	public float speed;

	public void Start() {
		timer = TIMER-EPSILON;
		limit = LIMIT-EPSILON;

		animator = GetComponent<Animator>();
		animator.SetFloat("Speed", 1);
	}
	
	public void Update() {
		if (timer > TIMER) {
			timer = 0;

			float direction;
			direction = Random.Range(-0.5f, 0.5f);

			// improvement: add a parameter to allow
			// e.g. random numbers in [-1, -0.7] U [0.7, 1]
			// with a check up of the current direction...

			animator.SetFloat("Direction", direction);
		} else {
			timer += Time.deltaTime;
		}
	}
}
