using UnityEngine;

public class GolemDamagerObject : MonoBehaviour {
	public int DamageToProvoke;

	private void OnParticleCollision(GameObject other) {
		Debug.Log($"Particles collided with {other.name}");
	}
}