using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset; // Décalage avec le player (sphère qui bouge)

	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	// LateUpdate is for camera updates, etc
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
