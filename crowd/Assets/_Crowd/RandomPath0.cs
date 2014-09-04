using UnityEngine;
using System.Collections;

public class RandomPath0 : MonoBehaviour {

	private readonly float EPSILON = 0.01f;
	private readonly float TIMER = 3f;
	private readonly float LIMIT = 5f;
	private float x, z, timer, limit;
	private Animator animator;

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

			// todo: add a parameter to allow
			// random numbers in [-1, -0.7] U [0.7, 1] (as an example)
			// with a check up of the current direction...

			animator.SetFloat("Direction", direction);
		} else {
			timer += Time.deltaTime;
		}
	}
}
