using UnityEngine;
using System.Collections;

public class StraightMove : MonoBehaviour {

	public int speed = 10;

	private Vector3 direction;

	public void Start() {
		direction = new Vector3(0, 0, 1);
	}
	
	/***
	 * Unity C# script to make an object move alongside one direction
	 * at a given speed, constantly.
	 *
	 * *** @todo ***
	 * 
	 * First amelioration: using an human instead of a sphere/cube
	 * The human moves in the same direction, at the same speed.
	 *
	 * Second kind of ameliorations to do:
	 *  - changing the speed using two keys (in/de-creasing)
	 *  - changing the direction using ddirectionnal pad
	 ***/
	public void Update() {
		transform.position += direction * Time.deltaTime * speed;
	}
}
