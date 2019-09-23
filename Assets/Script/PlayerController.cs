using RootMotion.Demos;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : CharacterThirdPerson {

	public enum AnimatorLayer {
		Base,
		Aiming
	}

	[Header("Trejo Variables")]
	[Space]
	public float AimLerpSpeed = .1f;
	public AimController AimController;

	private GameObject equippedWeapon;

	protected virtual void Update() {
		base.Update();
		HandleAiming();
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
}
