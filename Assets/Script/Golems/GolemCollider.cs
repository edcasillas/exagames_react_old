using UnityEngine;

public class GolemCollider : MonoBehaviour {
	public Golems.GolemController Golem;

	private void OnParticleCollision(GameObject other) {
		var damager = other.GetComponent<GolemDamagerObject>();
		Debug.Log("Making damage to the golem");
		Debug.Log("[GolemCollider - OnParticleCollision] Is a damager: " + damager);
		Debug.Log("[GolemCollider - OnParticleCollision] Name of the object: " + other.name);
		Golem.TakeDamage(10);
	}
}