using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackDetector : MonoBehaviour
{
	[SerializeField]
	private GolemController golemController;
	private readonly string PLAYER_TAG = "Player";

	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == PLAYER_TAG) {
			Debug.Log("Entro jeje");
			golemController.SpecialAttack();
		}
	}
}