using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSounds : MonoBehaviour
{
	[SerializeField] AudioSource aS;
	[SerializeField] AudioClip shoot;
	[SerializeField] AudioClip reloading;
	public bool isPaused;
    
	public AudioSource GetAudioSource() {
		return aS;
	}
	public void Pause() {
		aS.Pause();
		isPaused = true;
	}
	public void Resume() {
		aS.UnPause();
		isPaused = false;
	}
	public void PlayShootSound() {
		aS.PlayOneShot(shoot);
	}
	public void PlayReloadingSound() {
		aS.PlayOneShot(reloading);
		
	}
}
