﻿using UnityEngine;
using System.Collections;

public class TestCube : MonoBehaviour {
   	public string myName;
	public Rigidbody rigidBody;
	public Vector3 speed;

	public void Start() {
		rigidBody = gameObject.AddComponent<Rigidbody>();
		rigidBody.isKinematic = true;

		Debug.Log("Rigidbody found.");
		if (rigidBody == null)
			Debug.Log("Rigidbody found is empty");

		speed = new Vector3(2, 0, 0);
		sayMyName();
	}

	public void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            gameObject.renderer.material.color = Color.red;
			myName = "RED";
			sayMyName();
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            gameObject.renderer.material.color = Color.green;
			myName = "GREEN";
			sayMyName();
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            gameObject.renderer.material.color = Color.blue;
			myName = "BLUE";
			sayMyName();
        }

		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			gameObject.renderer.material.color = Color.gray;
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			rigidBody.MovePosition(rigidBody.position + speed);
			Debug.Log("TOP");
			// rigidBody.MovePosition(rigidBody.position);
		}

		if (Input.GetKeyDown(KeyCode.Q)) {
			rigidBody.MovePosition(rigidBody.position - speed); // *Time.deltaTime);
			Debug.Log("BOT");
			// rigidBody.MovePosition(rigidBody.position);
		}
    }

	private void sayMyName() {
		Debug.Log("My name is: " + myName);
	}
}

