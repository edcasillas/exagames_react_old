using RootMotion.Demos;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBarController))]
public class PlayerController : CharacterThirdPerson {

	public enum AnimatorLayer {
		Base,
		Aiming,
		Hurt
	}

	public enum AnimatorTriggers {
		HeadHit,
		BodyHit,
		Death
	}

	[Header("Trejo Variables")]
	[Space]
	public float AimLerpSpeed = .1f;
	public AimController AimController;

	private GameObject equippedWeapon;
	private HealthBarController healthBarController;
	private Rigidbody rigidbody;

	public int InitialHealth;

	private int _health;
	public int Health 
	{ 
		get { return _health; }
		private set 
		{
			_health = value;
			healthBarController.UpdateBar(_health);
		}
	}

	private void Awake() {
		healthBarController = GetComponent<HealthBarController>();
		rigidbody = GetComponent<Rigidbody>();
		Health = InitialHealth;
	}

	protected virtual void Update() {
		base.Update();
		HandleAiming();
	}

	public void TakeDamage(int damage) {
		if (Health <= 0)
			return;

		Health -= damage;

		if (Health <= 0) {
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			animator.SetTrigger(AnimatorTriggers.Death.ToString());
			enabled = false;
		} else {
			animator.SetTrigger(((AnimatorTriggers)Random.Range(0, 2)).ToString());
		}
	}

	private void HandleAiming() {
		if (!AimController?.ik.solver.transform)
			return;

		var newWeight = Input.GetKey(KeyCode.Q) ? 1f : 0f;

		var newAimingLayerWeight = AimController.weight;
		newAimingLayerWeight = Mathf.Lerp(AimController.weight, newWeight, AimLerpSpeed);
		AimController.weight = newAimingLayerWeight;

		if (newWeight == 0f) {
			newAimingLayerWeight = animator.GetLayerWeight((int)AnimatorLayer.Aiming);
			if (AimController.weight <= 0.05f)
				newAimingLayerWeight = Mathf.Lerp(newAimingLayerWeight, newWeight, AimLerpSpeed * 1.2f);
		}

		animator.SetLayerWeight((int)AnimatorLayer.Aiming, newAimingLayerWeight);

	}

	public void OnWeaponPickedUp(GameObject weapon) {
		equippedWeapon = weapon;
		AimController.ik.solver.transform = weapon.transform.parent;
	}

	private void OnCollisionEnter(Collision collision) {
		CheckIfShouldReceiveDamage(collision.gameObject);
	}

	private void OnParticleCollision(GameObject other) {
		CheckIfShouldReceiveDamage(other);
	}

	private void CheckIfShouldReceiveDamage(GameObject collider) {
		var col = collider.GetComponent<PlayerDamagerObject>();
		if (col)
			TakeDamage(col.DamageToProvoke);
	}
}
