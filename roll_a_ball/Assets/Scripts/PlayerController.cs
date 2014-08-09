using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

	public float speed = 1.0f;
	public GUIText countText;
	public GUIText winText;
	public GUIText timeText;
	private int count;

	public void Start() {
		count = 0;
		UpdateTexts();

		winText.text = "YOU WIN!";
		winText.guiText.enabled = false;
	}

	// FixedUpdate is called just before physical calculation
	public void FixedUpdate() {
		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
		Vector3 tmp = movement * speed * Time.deltaTime;

		rigidbody.AddForce(tmp);
	}

	public void LateUpdate() {
		UpdateTexts();
	}

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive(false);
			// Destroy(other.gameObject);

			++count;
		}
	}

	private void UpdateTexts() {
		countText.text = "Count: " + count;

		if (count < 20)
			timeText.text = "Time: " + Math.Round(Time.unscaledTime, 3) + " seconds";
		
		if (count >= 20)
			winText.guiText.enabled = true;
	}
}
