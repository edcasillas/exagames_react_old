using UnityEngine;

public class GolemCollider : MonoBehaviour {
	public GolemController Golem;

	private void OnParticleCollision(GameObject other)
	{
		Debug.Log("Making damage to the golem");
		Golem.TakeDamage(10);
	}

	public void MakeDamage()
	{
		Golem.TakeDamage(10);
	}
}