using UnityEngine;
using System.Collections;

public class Orbiting : MonoBehaviour {

	public float daysPerRevolution;

	private float speed;

	public void Start() {
		speed = 360/daysPerRevolution * 10;
		// 360° for X days, then for 1 day we have 360/X°
		// If we want to see what happends during 1 year
		// in only 1 frame, we can speed that up.
		// We need to multiply that result by 365?
	}

	public void Update() {
		transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}
}
