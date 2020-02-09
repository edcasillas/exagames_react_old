using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDamagerObject : MonoBehaviour
{
	public int DamageToProvoke;

	private void OnParticleCollision(GameObject other) {
		var golemHurtBox = other.GetComponent<GolemCollider>();
		if(golemHurtBox)
		{
			golemHurtBox.MakeDamage();
		}
	}
}
