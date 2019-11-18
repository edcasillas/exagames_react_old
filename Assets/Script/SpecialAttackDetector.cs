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
		var isPlayer = other.gameObject.GetComponent<PlayerController>();
		if(isPlayer) 
		{
			//Debug.Log("Special Attacks");
			golemController.SetSpecialAttackTriggered(true);
		}
		//For debug only
		//if (other.gameObject.tag == PLAYER_TAG) {
		//	//Debug.Log("Entro jeje");
		//	golemController.SetSpecialAttackTriggered(true);
		//}
	}
}