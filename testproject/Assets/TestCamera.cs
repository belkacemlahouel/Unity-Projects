using UnityEngine;
using System.Collections;

public class TestCamera : MonoBehaviour {

	void Start() {
	
	}
	
	void Update() {
	// Bouger dans l'espace suivant les touches directionelles appuyées
		 if (Input.GetKeyDown(KeyCode.DownArrow)) {
            // transform.position = transform.position - new Vector3(1, 0, 0);
			Debug.Log("DownArrow pressed.");
		 }
	}
}
