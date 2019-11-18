using UnityEngine;

public class GolemCollider : MonoBehaviour {
	[SerializeField]
	private _GolemController golemController;

	private void OnParticleCollision(GameObject other) {
		var col = other.GetComponent<PlayerDamagerObject>();
		var golemDamagerObj = other.GetComponent<GolemDamagerObject>();
		// TODO: Hay que arreglar esto para que reciba daño en base a ciertas condiciones. 
		//       Ahora agregue esto para que el fuego que le hace daño al jugador no le haga daño al golem

		if(golemDamagerObj)
		{
			golemController.TakeDamage(golemDamagerObj.DamageToProvoke);
			//golemDamagerObj.DamageToProvoke();
		}

		//if (!col)
		//{
		//	Golem.GetDamage(1);
		//}
	}
}