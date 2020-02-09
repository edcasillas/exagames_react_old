using Golems;
using UnityEngine;

public class GolemCollider : MonoBehaviour {
	public GolemController Golem;

	private void OnParticleCollision(GameObject other) {
		//var col = other.GetComponent<PlayerDamagerObject>();
		//var golemDamagerObject = other.GetComponent<GolemDamagerObject>();
		// TODO: Hay que arreglar esto para que reciba daño en base a ciertas condiciones. 
		//       Ahora agregue esto para que el fuego que le hace daño al jugador no le haga daño al golem
		//if (golemDamagerObject) 
		//{
			Debug.LogError("Making damage to the golem");
			//Golem.TakeDamage(golemDamagerObject.DamageToProvoke);
			Golem.TakeDamage(10);
		//}
		//if (!col)
		//	Golem.TakeDamage(1);
	}

	//private void OnParticleTrigger(GameObject other) 
	//{
	//	var golemDamagerObject = other.GetComponent<GolemDamagerObject>();
	//	if (golemDamagerObject) Golem.TakeDamage(golemDamagerObject.DamageToProvoke);
	//}
}