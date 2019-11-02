using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
	[SerializeField]
	private AudioSource aS;
	[SerializeField]
	private AudioClip[] playerRockFootsteps;
	[SerializeField]
	private AudioClip[] playerRockWalkFootsteps;
	[SerializeField]
	private AudioClip takingDamage;
	[SerializeField]
	private AudioClip jump;

	private void Start() {
		aS = GetComponent<AudioSource>();
		if (!aS) {
			Debug.LogError("PlayerHasNoAudioSource");
		}
	}

	public void PlayPlayerStepSoundRock() 
	{
		int num = Random.Range(0, playerRockFootsteps.Length);
		aS.PlayOneShot(playerRockFootsteps[num]);
	}
	public void PlayPlayerWalkStepSoundRock() {
		int num = Random.Range(0, playerRockWalkFootsteps.Length);
		aS.PlayOneShot(playerRockWalkFootsteps[num]);
	}
	public void PlayTakingDamageSound() {
		aS.PlayOneShot(takingDamage);
	}
	public void PlayJumpSound() {
		aS.PlayOneShot(jump);
	}
}
