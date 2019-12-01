using RootMotion.Demos;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;

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
	public bool isAiming;

	private GameObject equippedWeapon;
	private HealthBarController healthBarController;
	private Rigidbody rigidbody;
	[SerializeField] private GameObject gameoverCanvas;
	[SerializeField] private FreeLookCam freeLookCam;

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
			gameoverCanvas.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			freeLookCam.enabled = false;
			Destroy(PauseManager.instance.gameObject);
			enabled = false;
		} else {
			animator.SetTrigger(((AnimatorTriggers)Random.Range(0, 2)).ToString());
		}
	}

	private void HandleAiming() {
		if (Health <= 0)
			return;

		if (!AimController?.ik.solver.transform)
			return;

		var newWeight = Input.GetMouseButton(1) ? 1f : 0f;

		var newAimingLayerWeight = AimController.weight;
		newAimingLayerWeight = Mathf.Lerp(AimController.weight, newWeight, AimLerpSpeed);
		AimController.weight = newAimingLayerWeight;

		if (newWeight == 0f) {
			newAimingLayerWeight = animator.GetLayerWeight((int)AnimatorLayer.Aiming);
			if (AimController.weight <= 0.05f) {
				newAimingLayerWeight = Mathf.Lerp(newAimingLayerWeight, newWeight, AimLerpSpeed * 1.2f);
			}
		}

		if (newWeight == 1) {
			isAiming = true;
		} else {
			isAiming = false;
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

	private void OnTriggerEnter(Collider other) {
		CheckIfShouldReceiveDamage(other.gameObject);
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
