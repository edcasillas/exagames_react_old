using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour {
	public Vector3 RotationAmount;

	void Update() {
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + (RotationAmount * Time.deltaTime));
	}
}
