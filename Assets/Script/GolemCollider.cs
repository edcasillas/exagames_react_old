using UnityEngine;

public class GolemCollider : MonoBehaviour {
	public GolemController Golem;

	private void OnParticleCollision(GameObject other) {
		var col = other.GetComponent<PlayerDamagerObject>();
		// TODO: Hay que arreglar esto para que reciba daño en base a ciertas condiciones. 
		//       Ahora agregue esto para que el fuego que le hace daño al jugador no le haga daño al golem
		if (!col)
			Golem.GetDamage(1);
	}
}