﻿using UnityEngine;
using System.Collections;

public class TestCamera : MonoBehaviour {

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	// Bouger dans l'espace suivant les touches directionelles appuyées
		 if (Input.GetKeyDown(KeyCode.DownArrow)) {
            // gameObject.position.x--;
			Debug.Log("DownArrow pressed.");
		 }
	}
}
