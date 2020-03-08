using Golems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Collider))]
public class PlayerDamagerObject : MonoBehaviour
{
	[SerializeField] private GolemController golemController;
	public int DamageToProvoke;

	private void Awake() 
	{
		if(golemController)	
		{
			if(golemController.IsInnoffensiveGolem()) {
				DamageToProvoke = 0;
			}
		}
	}

	public GolemController GetGolemController() {
		if(golemController) {
			return golemController;
		}
		return null;
	}
}
