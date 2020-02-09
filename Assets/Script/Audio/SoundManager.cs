using System;
using System.Collections;
using Audio.Configuration;
using UnityEngine;

namespace Audio {
	public class SoundManager : MonoBehaviour {
		public static SoundManager Instance { get; private set; }

		[SerializeField] private AudioSource _musicSource;
		[SerializeField] private MusicConfiguration _configuration;

		private IEnumerator loopCoroutine;

		private void Start() {
			if (Instance == null) {
				Instance = this;
			} else if (Instance != this) {
				Destroy(gameObject);
			}
		}

		private void OnDestroy() {
			if (Instance == this) Instance = null;
		}

		public void Play(MusicClipName clipName, bool loop = false, Action onFinished = null) {
			var clip = _configuration.Get(clipName);
			_musicSource.clip = clip;
			_musicSource.loop = loop;
			_musicSource.Play();
			if(onFinished != null) StartCoroutine(invokeDelayed(onFinished, clip.length));
		}

		private IEnumerator invokeDelayed(Action action, float delay) {
			yield return new WaitForSeconds(delay);
			action.Invoke();
		}

		public void PlayLoopClip(MusicClipName clipName, bool stopCurrentClip) {
			if (stopCurrentClip) {
				Play(clipName);
				_musicSource.loop = true;
				return;
			}

			stopCurrentLoopCoroutine();

			loopCoroutine = playDelayedLoop(clipName, _musicSource.clip.length - _musicSource.time);
			StartCoroutine(loopCoroutine);
		}

		private void stopCurrentLoopCoroutine() {
			if (loopCoroutine != null) {
				StopCoroutine(loopCoroutine);
				loopCoroutine = null;
			}
		}

		private IEnumerator playDelayedLoop(MusicClipName clipName, float delayedTime) {
			yield return new WaitForSeconds(delayedTime);
			Play(clipName);
			_musicSource.loop = true;
		}
	}
}