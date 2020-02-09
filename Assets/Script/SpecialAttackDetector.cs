using Golems;
using UnityEngine;

public class SpecialAttackDetector : MonoBehaviour
{
	[SerializeField]
	private GolemController golemController;

	private void OnTriggerEnter(Collider other) 
	{
		var isPlayer = other.gameObject.GetComponent<PlayerController>();
		if(isPlayer) 
		{
			golemController.TriggerSpecialAttack();
		}
	}
}