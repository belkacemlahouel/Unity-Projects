using UnityEngine;
using System;

public class ClockAnimator : MonoBehaviour {

	private const float
		degH = 360f/12f,
		degM = 360f/60f,
		degS = 360f/60f;

	public Transform hours, minutes, seconds;

	public void Update() {
		DateTime time = DateTime.Now;

		hours.localRotation = Quaternion.Euler(0f, 0f, time.Hour * -degH);
		minutes.localRotation = Quaternion.Euler(0f, 0f, time.Minute * -degM);
		seconds.localRotation = Quaternion.Euler(0f, 0f, time.Second * -degS);
	}
}
