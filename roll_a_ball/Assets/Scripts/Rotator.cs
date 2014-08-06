using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public Vector3 direction;
	
	void Update () {
		transform.Rotate(direction * Time.deltaTime);
	}
}
