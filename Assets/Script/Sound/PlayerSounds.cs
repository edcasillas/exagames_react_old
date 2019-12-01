using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
	[SerializeField]
	private AudioClip[] playerRockFootsteps;
	[SerializeField]
	private AudioClip[] playerRockWalkFootsteps;
	[SerializeField]
	private AudioClip takingDamage;
	[SerializeField]
	private AudioClip jump;
	[SerializeField]
	private AudioClip death;
	[SerializeField]
	private AudioClip pickUpWeapon;

	private AudioSource audioSource;

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
		if (!audioSource) {
			Debug.LogError("PlayerHasNoAudioSource");
		}
	}

	public void PlayPlayerStepSoundRock() 
	{
		int num = Random.Range(0, playerRockFootsteps.Length);
		audioSource.PlayOneShot(playerRockFootsteps[num]);
	}
	public void PlayPlayerWalkStepSoundRock() {
		int num = Random.Range(0, playerRockWalkFootsteps.Length);
		audioSource.PlayOneShot(playerRockWalkFootsteps[num]);
	}
	public void PlayTakingDamageSound() {
		audioSource.PlayOneShot(takingDamage);
	}

	/// <summary>
	/// Evento que se manda llamar desde HumanoidIdleJumpUp.fbx/HumanoidJumpUp y desde HumanoidJumpAndFall.fbx/HumanoidJumpForwardLeft y HumanoidJumpAndFall.fbx/HumanoidJumpForwardRight
	/// </summary>
	public void PlayJumpSound() {
		audioSource.PlayOneShot(jump);
	}
	public void PlayDeathSound() {
		audioSource.PlayOneShot(death);
	}
	public void PlayPickUpWeaponSound() {
		audioSource.PlayOneShot(pickUpWeapon);
	}
}