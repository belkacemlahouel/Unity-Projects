using UnityEngine;
using System.Collections;

// Prefabs script
public class Rotator : MonoBehaviour {

	public Vector3 direction;
	private int number;
	private static int MAX_NUMBER = 0;

	public void Start() {
		++MAX_NUMBER;
		number = MAX_NUMBER;
		
		Debug.Log("NUMBER: " + number);
	}

	public void Update() {
		transform.Rotate(direction * Time.deltaTime);
	}
}
