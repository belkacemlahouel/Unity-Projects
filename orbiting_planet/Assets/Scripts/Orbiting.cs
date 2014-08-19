using UnityEngine;
using System.Collections;

public class Orbiting : MonoBehaviour {

	public float speed = 50f;

	void Update() {
		transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}
}
