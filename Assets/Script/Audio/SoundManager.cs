using System.Collections;
using Audio.Configuration;
using UnityEngine;

namespace Audio {
	public class SoundManager : MonoBehaviour {
		[SerializeField] private AudioSource _musicSource;
		[SerializeField] private MusicConfiguration _configuration;

		private IEnumerator _loopCoroutine;

		public static SoundManager Instance { get; private set; }

		private void Start() {
			if (Instance == null) {
				Instance = this;
			} else if (Instance != this) {
				Destroy(gameObject);
			}

			DontDestroyOnLoad(gameObject);
		}

		public void PlaySingleClip(MusicClipName clipName) {
			var clip = _configuration.GetAudioClip(clipName);
			_musicSource.clip = clip;
			_musicSource.loop = false;
			_musicSource.Play();
		}

		public void PlayLoopClip(MusicClipName clipName, bool stopCurrentClip) {
			if (stopCurrentClip) {
				PlaySingleClip(clipName);
				_musicSource.loop = true;
				return;
			}

			StopCurrentLoopCoroutine();

			_loopCoroutine = PlayDelayedLoop(clipName, _musicSource.clip.length - _musicSource.time);
			StartCoroutine(_loopCoroutine);
		}

		private void StopCurrentLoopCoroutine() {
			if (_loopCoroutine != null) {
				StopCoroutine(_loopCoroutine);
				_loopCoroutine = null;
			}
		}

		private IEnumerator PlayDelayedLoop(MusicClipName clipName, float delayedTime) {
			yield return new WaitForSeconds(delayedTime);
			PlaySingleClip(clipName);
			_musicSource.loop = true;
		}
	}
}