using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSounds : MonoBehaviour
{
	[SerializeField]
	private AudioSource aS;
	[SerializeField]
	private AudioClip[] golemSteps;
	[SerializeField]
	private AudioClip golemMeleeAttack;

	private void Start() {
		aS = GetComponent<AudioSource>();
		if(!aS) {
			Debug.LogError("GolemHasNoAudioSource");
		}
	}
	public void PlayGolemStepSound() {
		int num = Random.Range(0, golemSteps.Length);
		aS.PlayOneShot(golemSteps[num]);
	}
	public void PlayGolemMeleeAtackSound() {
		aS.PlayOneShot(golemMeleeAttack);
	}

}
