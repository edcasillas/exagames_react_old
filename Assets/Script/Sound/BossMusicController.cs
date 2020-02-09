using System;
using System.Collections;
using UnityEngine;

namespace Sound {
	[RequireComponent(typeof(AudioSource))]
	public class BossMusicController : MonoBehaviour {
		[SerializeField] private AudioClip introClip;
		[SerializeField] private AudioClip loopClip;
		[SerializeField] private AudioClip defeatedClip;
		[SerializeField] private AudioClip victoryClip;

		private AudioSource audioSource;
		private bool isStarted;
		private bool isFinished;
		private Action onIntroFinished;

		private void Awake() {
			audioSource = GetComponent<AudioSource>();
			GameManager.Instance.SetBossMusicController(this);
		}

		public void PlayIntroAndLoop(Action onIntroFinished) {
			if (isStarted) return;
			isStarted = true;
			this.onIntroFinished = onIntroFinished;
			StartCoroutine(playIntroAndLoop());
		}

		public void PlayDefeated() {
			isFinished = true;
			audioSource.clip = defeatedClip;
			audioSource.loop = false;
			audioSource.Play();
		}

		public void PlayVictory() {
			isFinished = true;
			audioSource.clip = victoryClip;
			audioSource.loop = false;
			audioSource.Play();
		}

		private IEnumerator playIntroAndLoop() {
			audioSource.clip = introClip;
			audioSource.loop = false;
			audioSource.Play();
			yield return new WaitForSeconds(introClip.length);
			if (!isFinished) {
				onIntroFinished?.Invoke();
				audioSource.clip = loopClip;
				audioSource.loop = true;
				audioSource.Play();
			}
		}
	}
}