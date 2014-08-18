using UnityEngine;
using System;

public class ClockAnimator : MonoBehaviour {

	private const float
		degH = 360f/12f,
		degM = 360f/60f,
		degS = 360f/60f;

	public Transform hours, minutes, seconds;
	public Boolean analog;

	public void Update() {
		if (analog) {
			// Not the best solution, there is a better one using C# functions

			/*DateTime time = DateTime.Now;

			float tmp = -degH * (time.Hour + time.Minute/60 + time.Second/3600);
			hours.localRotation = Quaternion.Euler(0f, 0f, tmp);

			tmp = -degM * (time.Minute + time.Second/60);
			minutes.localRotation = Quaternion.Euler(0f, 0f, tmp);

			tmp = -degS * (time.Second);
			seconds.localRotation = Quaternion.Euler(0f, 0f, tmp);*/

            TimeSpan timespan = DateTime.Now.TimeOfDay;

            hours.localRotation =
                Quaternion.Euler(0f, 0f,(float)timespan.TotalHours * -degH);
            minutes.localRotation =
                Quaternion.Euler(0f, 0f,(float)timespan.TotalMinutes * -degM);
            seconds.localRotation =
                Quaternion.Euler(0f, 0f,(float)timespan.TotalSeconds * -degS);
		} else {
			DateTime time = DateTime.Now;

			hours.localRotation = Quaternion.Euler(0f, 0f, time.Hour * -degH);
			minutes.localRotation = Quaternion.Euler(0f, 0f, time.Minute * -degM);
			seconds.localRotation = Quaternion.Euler(0f, 0f, time.Second * -degS);
		}
	}
}
