using UnityEngine;
using System.Collections;

public class Spinning : MonoBehaviour {

	public float speed = 10f;

	void LateUpdate () {
		transform.Rotate(Vector3.up, speed*Time.deltaTime);
	}
}
