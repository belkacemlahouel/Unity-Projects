using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 1.0f;

	// FixedUpdate is called just before physical calculation
	public void FixedUpdate() {
		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
		Vector3 tmp = movement * speed * Time.deltaTime;

		// Debug.Log("Tmp: (" + tmp.x + ", " + tmp.y + ", " + tmp.z + ")");

		rigidbody.AddForce(tmp);
	}

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive(false);
			// Destroy(other.gameObject);
		}
	}
}
