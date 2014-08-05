using UnityEngine;
using System.Collections;

public class TestSphere : MonoBehaviour {
	public Vector3 speed;

	public void Start() {
		speed = new Vector3(0, 0, 1);
	}

	public void Update() {
		transform.Translate(speed);
	}
}

